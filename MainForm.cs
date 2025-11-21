using Npgsql;
using System.Data.SqlTypes;
using System.Xml;

namespace WinFormsRPC
{
    public partial class MainForm : Form
    {
        private static void Swap(int[] array, int i, int j)
        {
            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }

        private static int Partition(int[] array, int left, int right)
        {
            int pivot = array[(left + right) / 2];

            while (left <= right)
            {
                while (array[left] < pivot) ++left;
                while (array[right] > pivot) --right;
                if (left <= right)
                {
                    Swap(array, left, right);
                    ++left;
                    --right;
                }
            }

            return left;
        }

        private static void QuickSort(int[] array, int left, int right)
        {
            if (left >= right) return;
            int pivotIndex = Partition(array, left, right);
            QuickSort(array, left, pivotIndex - 1);
            QuickSort(array, pivotIndex, right);
        }

        public static void QuickSort(int[] array)
        {
            QuickSort(array, 0, array.Length - 1);
        }

        private void ClearErrors()
        {
            errorProvider.Clear();
            errorProvider.SetError(groupBoxRandom, "");
            errorProvider.SetError(buttonSorting, "");
            errorProvider.SetError(buttonSave, "");
            errorProvider.SetError(buttonCreate, "");
            errorProvider.SetError(comboBoxArrayList, "");
            errorProvider.SetError(buttonSqlDelete, "");
            errorProvider.SetError(dataGridViewArrayA, "");
            errorProvider.SetError(dataGridViewArrayB, "");
        }

        private void LoadDatabaseConfig()
        {
            dbConfig = new DatabaseConfig();

            try
            {
                if (File.Exists(configFilePath))
                {
                    var xmlDoc = new XmlDocument();
                    xmlDoc.Load(configFilePath);

                    var root = xmlDoc.DocumentElement;
                    dbConfig.Host = root.SelectSingleNode("Host")?.InnerText ?? "localhost";
                    dbConfig.Port = root.SelectSingleNode("Port")?.InnerText ?? "5432";
                    dbConfig.Username = root.SelectSingleNode("Username")?.InnerText ?? "postgres";
                    dbConfig.Password = root.SelectSingleNode("Password")?.InnerText ?? "";
                    dbConfig.Database = root.SelectSingleNode("Database")?.InnerText ?? "arrays_db";
                }
                else
                {
                    // Создаем файл конфигурации по умолчанию
                    CreateDefaultConfig();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки конфигурации: {ex.Message}\nБудет использована конфигурация по умолчанию.");
            }
        }

        private void CreateDefaultConfig()
        {
            try
            {
                var xmlDoc = new XmlDocument();
                var declaration = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                xmlDoc.AppendChild(declaration);

                var root = xmlDoc.CreateElement("DatabaseConfiguration");
                xmlDoc.AppendChild(root);

                void AddConfigElement(string name, string value)
                {
                    var element = xmlDoc.CreateElement(name);
                    element.InnerText = value;
                    root.AppendChild(element);
                }

                AddConfigElement("Host", dbConfig.Host);
                AddConfigElement("Port", dbConfig.Port);
                AddConfigElement("Username", dbConfig.Username);
                AddConfigElement("Password", dbConfig.Password);
                AddConfigElement("Database", dbConfig.Database);

                xmlDoc.Save(configFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка создания конфигурации: {ex.Message}");
            }
        }

        private void UpdateGridView()
        {
            dataGridViewArrayA.Rows.Clear();
            dataGridViewArrayB.Rows.Clear();

            // Для больших массивов ограничиваем отображение в DataGridView
            int displayLimit = 1000;
            bool limitedA = originalArray.Count > displayLimit;
            bool limitedB = sortedArray.Count > displayLimit;

            try
            {
                // Исправление: создаем массивы для отображения безопасным способом
                var displayArrayA = limitedA
                    ? originalArray.Take(displayLimit).ToArray()
                    : originalArray.ToArray();

                var displayArrayB = limitedB
                    ? sortedArray.Take(displayLimit).ToArray()
                    : sortedArray.ToArray();

                if(displayArrayA.Length > 0)
                {
                    dataGridViewArrayA.Rows.Add(displayArrayA.Length);
                }
                if(displayArrayB.Length > 0)
                {
                    dataGridViewArrayB.Rows.Add(displayArrayB.Length);
                }


                for (int i = 0; i < displayArrayA.Length; i++)
                {
                    dataGridViewArrayA.Rows[i].Cells[0].Value = i;
                    dataGridViewArrayA.Rows[i].Cells[1].Value = displayArrayA[i];
                }
                    

                for (int i = 0; i < displayArrayB.Length; i++)
                {
                    dataGridViewArrayB.Rows[i].Cells[0].Value = i;
                    dataGridViewArrayB.Rows[i].Cells[1].Value = displayArrayB[i];
                }
                    

                // Показываем предупреждение, если массив слишком большой для полного отображения
                if (limitedA)
                {
                    dataGridViewArrayA.Rows.Add($"... (показано {displayLimit} из {originalArray.Count} элементов)");
                }

                if (limitedB)
                {
                    dataGridViewArrayB.Rows.Add($"... (показано {displayLimit} из {sortedArray.Count} элементов)");
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибок отображения
                Console.WriteLine($"Ошибка в UpdateGridView: {ex.Message}");
            }
        }

        public MainForm()
        {
            InitializeComponent();
            InitializeCustomComponents();
            LoadDatabaseConfig();

            // Создаем БД и таблицу при запуске
            EnsureDatabaseAndTable();

            UpdateGridView();

            isFilterMenuOpen = false;
            haveSqlMode = false;
            haveTransferMode = false;
            haveRandomMode = false;

            comboBoxArrayList.Visible = false;
            textBoxArrayA.Visible = false;
            textBoxArrayB.Visible = false;
            buttonSave.Enabled = false;
            groupBoxArrayList.Visible = false;
            groupBoxFilter.Visible = false;
            groupBoxTransfer.Visible = false;
        }

        private void EnsureDatabaseAndTable()
        {
            try
            {
                // Сначала пытаемся подключиться к существующей БД
                using (var conn = new NpgsqlConnection(dbConfig.GetConnectionString()))
                {
                    try
                    {
                        conn.Open();
                        // Если подключились, проверяем существование таблицы
                        EnsureTableExists(conn);
                    }
                    catch (PostgresException ex) when (ex.SqlState == "3D000") // База данных не существует
                    {
                        // Создаем базу данных
                        CreateDatabase();
                        // После создания БД создаем таблицу
                        using (var conn2 = new NpgsqlConnection(dbConfig.GetConnectionString()))
                        {
                            conn2.Open();
                            EnsureTableExists(conn2);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorProvider.SetError(buttonSqlMode, $"Ошибка инициализации БД: {ex.Message}");
            }
        }

        private void CreateDatabase()
        {
            // Подключаемся к базе данных postgres по умолчанию для создания новой БД
            var defaultConfig = new DatabaseConfig
            {
                Host = dbConfig.Host,
                Port = dbConfig.Port,
                Username = dbConfig.Username,
                Password = dbConfig.Password,
                Database = "postgres" // Подключаемся к стандартной БД
            };

            using (var conn = new NpgsqlConnection(defaultConfig.GetConnectionString()))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand($"CREATE DATABASE {dbConfig.Database}", conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void EnsureTableExists(NpgsqlConnection conn)
        {
            try
            {
                // Проверяем существование таблицы
                var checkTableCmd = new NpgsqlCommand(
                    "SELECT EXISTS (SELECT FROM information_schema.tables WHERE table_name = 'arrays')", conn);

                bool tableExists = (bool)checkTableCmd.ExecuteScalar();

                if (!tableExists)
                {
                    // Создаем таблицу
                    var createTableCmd = new NpgsqlCommand(@"
                CREATE TABLE arrays (
                    id SERIAL PRIMARY KEY,
                    array_name TEXT,
                    array_data INTEGER[],
                    is_sorted BOOLEAN NOT NULL,
                    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
                )", conn);
                    createTableCmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка создания таблицы: {ex.Message}");
            }
        }

        private void InitializeCustomComponents()
        {
            // Инициализация DataGridView
            InitializeDataGridView(dataGridViewArrayA, "Исходный массив");
            InitializeDataGridView(dataGridViewArrayB, "Отсортированный массив");

            // Установка значений по умолчанию
            maskedTextBoxArraySize.Text = "10";
            maskedTextBoxRandBegin.Text = "-50";
            maskedTextBoxRandEnd.Text = "50";

            // Настройка ToolTip
            InitializeToolTips();

            // Настройка стилей для кнопок с логикой CheckBox
            SetCheckBoxButtonStyle(buttonRandom, haveRandomMode);
            SetCheckBoxButtonStyle(buttonSqlMode, haveSqlMode);
        }

        private void InitializeDataGridView(DataGridView dgv, string columnName)
        {
            dgv.Columns.Clear();
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "id", HeaderText = "Индекс" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "Value", HeaderText = columnName });
            dgv.AllowUserToAddRows = false;
            dgv.RowHeadersVisible = false;
            dgv.AllowUserToDeleteRows = false;
        }

        private void InitializeToolTips()
        {
            toolTip.SetToolTip(buttonRandom, "Включить/выключить режим случайной генерации");
            toolTip.SetToolTip(buttonCreate, "Создать новый массив указанного размера");
            toolTip.SetToolTip(buttonSorting, "Отсортировать исходный массив");
            toolTip.SetToolTip(buttonDeleteA, "Очистить исходный массив");
            toolTip.SetToolTip(buttonDeleteB, "Очистить отсортированный массив");
            toolTip.SetToolTip(buttonSqlMode, "Включить/выключить режим работы с БД");
            toolTip.SetToolTip(buttonSave, "Сохранить массивы в базу данных");
            toolTip.SetToolTip(buttonSqlDelete, "Удалить выбранный массив из БД");
            toolTip.SetToolTip(buttonFilter, "Показать/скрыть фильтры");
            toolTip.SetToolTip(buttonTransfer, "Показать/скрыть панель переноса");
            toolTip.SetToolTip(buttonA, "Загрузить выбранный массив в исходный");
            toolTip.SetToolTip(buttonB, "Загрузить выбранный массив в отсортированный");
            toolTip.SetToolTip(maskedTextBoxArraySize, "Размер создаваемого массива");
            toolTip.SetToolTip(maskedTextBoxRandBegin, "Начало диапазона случайных чисел");
            toolTip.SetToolTip(maskedTextBoxRandEnd, "Конец диапазона случайных чисел");
        }

        private void SetCheckBoxButtonStyle(Button button, bool isActive)
        {
            button.BackColor = isActive ? Color.LightBlue : SystemColors.Control;
            button.FlatAppearance.BorderColor = isActive ? Color.Blue : SystemColors.ControlDark;
            button.FlatAppearance.BorderSize = isActive ? 2 : 1;
        }



        private void buttonSave_Click(object sender, EventArgs e)
        {
            ClearErrors();

            if (originalArray.Count == 0 && sortedArray.Count == 0)
            {
                errorProvider.SetError(buttonSave, "Нет данных для сохранения");
                return;
            }

            try
            {
                // Убеждаемся, что БД и таблица существуют перед сохранением
                EnsureDatabaseAndTable();

                using (var conn = new NpgsqlConnection(dbConfig.GetConnectionString()))
                {
                    conn.Open();

                    // Сохраняем исходный массив
                    if (originalArray.Count > 0)
                    {
                        SaveArrayToDatabase(conn,
                            string.IsNullOrWhiteSpace(textBoxArrayA.Text) ? null : textBoxArrayA.Text.Trim(),
                            originalArray, false);
                    }

                    // Сохраняем отсортированный массив
                    if (sortedArray.Count > 0)
                    {
                        SaveArrayToDatabase(conn,
                            string.IsNullOrWhiteSpace(textBoxArrayB.Text) ? null : textBoxArrayB.Text.Trim(),
                            sortedArray, true);
                    }
                }

                LoadArraysList();
            }
            catch (Exception ex)
            {
                errorProvider.SetError(buttonSave, $"Ошибка сохранения: {ex.Message}");
            }
        }

        private void SaveArrayToDatabase(NpgsqlConnection conn, string name, List<int> array, bool isSorted)
        {
            using (var cmd = new NpgsqlCommand(
                "INSERT INTO arrays (array_name, array_data, is_sorted) VALUES (@name, @data, @sorted)", conn))
            {
                cmd.Parameters.AddWithValue("name", name ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("data", array.ToArray());
                cmd.Parameters.AddWithValue("sorted", isSorted);
                cmd.ExecuteNonQuery();
            }
        }

        private void LoadArraysList()
        {
            ClearErrors();
            comboBoxArrayList.Items.Clear();

            if (!haveSqlMode) return;

            try
            {
                // Убеждаемся, что БД и таблица существуют перед загрузкой
                EnsureDatabaseAndTable();

                using (var conn = new NpgsqlConnection(dbConfig.GetConnectionString()))
                {
                    conn.Open();

                    var whereConditions = new List<string>();

                    if (checkBoxSorted.Checked && !checkBoxNotSorted.Checked)
                    {
                        whereConditions.Add("is_sorted = true");
                    }
                    else if (checkBoxNotSorted.Checked && !checkBoxSorted.Checked)
                    {
                        whereConditions.Add("is_sorted = false");
                    }

                    string query = "SELECT id, array_name, is_sorted FROM arrays";
                    if (whereConditions.Any())
                    {
                        query += " WHERE " + string.Join(" OR ", whereConditions);
                    }
                    query += " ORDER BY created_at DESC";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string name = reader.IsDBNull(1) ? null : reader.GetString(1);
                            bool isSorted = reader.GetBoolean(2);

                            string displayText = FormatArrayDisplay(id, name, isSorted);
                            comboBoxArrayList.Items.Add(new ArrayItem(id, name, isSorted, displayText));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorProvider.SetError(comboBoxArrayList, $"Ошибка загрузки списка: {ex.Message}");
            }
        }

        private string FormatArrayDisplay(int id, string name, bool isSorted)
        {
            string status = isSorted ? "сортирован" : "не сортирован";
            string identifier = string.IsNullOrEmpty(name) ? id.ToString() : name;

            return $"{identifier}_{status}";
        }

        private void buttonSorting_Click(object sender, EventArgs e)
        {
            ClearErrors();

            if (originalArray.Count == 0)
            {
                errorProvider.SetError(buttonSorting, "Исходный массив пуст");
                return;
            }

            if (originalArray.Count > 100000)
            {
                errorProvider.SetError(buttonSorting, "Слишком большой массив для сортировки");
                return;
            }

            try
            {
                // Показываем курсор ожидания для больших массивов
                if (originalArray.Count > 1000)
                {
                    Cursor = Cursors.WaitCursor;
                    Application.DoEvents();
                }

                var arrayToSort = originalArray.ToArray();
                QuickSort(arrayToSort);
                sortedArray = new List<int>(arrayToSort);
                UpdateGridView();

                if (originalArray.Count > 1000)
                {
                    Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                errorProvider.SetError(buttonSorting, $"Ошибка сортировки: {ex.Message}");
                Cursor = Cursors.Default;
            }
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            ClearErrors();

            if (!int.TryParse(maskedTextBoxArraySize.Text, out int size) || size <= 0)
            {
                errorProvider.SetError(maskedTextBoxArraySize, "Размер массива должен быть положительным числом");
                return;
            }

            if (size > 100000)
            {
                errorProvider.SetError(maskedTextBoxArraySize, "Слишком большой размер массива (макс. 100000)");
                return;
            }

            try
            {
                if (haveRandomMode)
                {
                    if (!ValidateRandomRange()) return;

                    int begin = int.Parse(maskedTextBoxRandBegin.Text);
                    int end = int.Parse(maskedTextBoxRandEnd.Text);
                    var rnd = new Random();

                    originalArray = new List<int>(size);
                    for (int i = 0; i < size; i++)
                    {
                        originalArray.Add(rnd.Next(begin, end + 1));
                    }
                }
                else
                {
                    // Создаем массив с нулями
                    originalArray = new List<int>(Enumerable.Repeat(0, size));
                }

                sortedArray.Clear();
                textBoxArrayB.Clear();
                UpdateGridView();
            }
            catch (Exception ex)
            {
                errorProvider.SetError(buttonCreate, $"Ошибка создания массива: {ex.Message}");
            }
        }

        private void textBoxArrayA_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void buttonSqlMode_Click(object sender, EventArgs e)
        {
            ClearErrors();

            haveSqlMode = !haveSqlMode;
            SetCheckBoxButtonStyle(buttonSqlMode, haveSqlMode);

            if (haveSqlMode)
            {
                comboBoxArrayList.Visible = true;
                textBoxArrayA.Visible = true;
                textBoxArrayB.Visible = true;
                buttonSave.Enabled = true;
                groupBoxArrayList.Visible = true;
                buttonSqlDelete.Enabled = true;
                LoadArraysList();
                return;
            }

            comboBoxArrayList.Visible = false;
            textBoxArrayA.Visible = false;
            textBoxArrayB.Visible = false;
            buttonSave.Enabled = false;
            groupBoxArrayList.Visible = false;
            buttonSqlDelete.Enabled = false;
        }

        private void buttonRandom_Click(object sender, EventArgs e)
        {
            ClearErrors();

            haveRandomMode = !haveRandomMode;
            SetCheckBoxButtonStyle(buttonRandom, haveRandomMode);

            if (haveRandomMode && !ValidateRandomRange())
            {
                haveRandomMode = false;
                SetCheckBoxButtonStyle(buttonRandom, false);
            }
        }

        private bool ValidateRandomRange()
        {
            if (!int.TryParse(maskedTextBoxRandBegin.Text, out int begin) ||
            !int.TryParse(maskedTextBoxRandEnd.Text, out int end))
            {
                errorProvider.SetError(groupBoxRandom, "Некорректные значения диапазона");
                return false;
            }

            if (begin >= end)
            {
                errorProvider.SetError(groupBoxRandom, "Начало диапазона должно быть меньше конца");
                return false;
            }

            errorProvider.SetError(groupBoxRandom, "");
            return true;
        }



        private void hideControl(Control obj, out bool trigger)
        {
            obj.Visible = false;
            trigger = false;
        }

        private void hideControls(Control[] objs, out bool trigger)
        {
            foreach (var obj in objs)
            {
                obj.Visible = false;
            }
            trigger = false;
        }

        private void showControl(Control obj, out bool trigger)
        {
            obj.Visible = true;
            trigger = true;
        }

        private void showControls(Control[] objs, out bool trigger)
        {
            foreach (var obj in objs)
            {
                obj.Visible = true;
            }
            trigger = true;
        }

        private void buttonTransfer_Click(object sender, EventArgs e)
        {
            ClearErrors();

            if (!haveTransferMode)
            {
                showControl(groupBoxTransfer, out haveTransferMode);
                hideControl(groupBoxFilter, out isFilterMenuOpen);
                SetCheckBoxButtonStyle(buttonFilter, false);
                return;
            }
            hideControl(groupBoxTransfer, out haveTransferMode);
        }

        private void buttonFilter_Click(object sender, EventArgs e)
        {
            ClearErrors();

            if (!isFilterMenuOpen)
            {
                showControl(groupBoxFilter, out isFilterMenuOpen);
                hideControl(groupBoxTransfer, out haveTransferMode);
                SetCheckBoxButtonStyle(buttonTransfer, false);
                return;
            }
            hideControl(groupBoxFilter, out isFilterMenuOpen);
        }

        private void buttonDeleteA_Click(object sender, EventArgs e)
        {
            ClearErrors();
            originalArray.Clear();
            UpdateGridView();
        }

        private void buttonDeleteB_Click(object sender, EventArgs e)
        {
            ClearErrors();
            sortedArray.Clear();
            UpdateGridView();
        }

        private void buttonSqlDelete_Click(object sender, EventArgs e)
        {
            ClearErrors();

            if (comboBoxArrayList.SelectedItem == null)
            {
                errorProvider.SetError(comboBoxArrayList, "Выберите массив для удаления");
                return;
            }

            var selectedItem = (ArrayItem)comboBoxArrayList.SelectedItem;

            try
            {
                using (var conn = new NpgsqlConnection(dbConfig.GetConnectionString()))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand("DELETE FROM arrays WHERE id = @id", conn))
                    {
                        cmd.Parameters.AddWithValue("id", selectedItem.Id);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            LoadArraysList();
                        }
                        else
                        {
                            errorProvider.SetError(buttonSqlDelete, "Не удалось удалить массив");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorProvider.SetError(buttonSqlDelete, $"Ошибка удаления: {ex.Message}");
            }
        }

        private void buttonA_Click(object sender, EventArgs e)
        {
            ClearErrors();
            LoadSelectedArrayToGrid(false);
        }

        private void buttonB_Click(object sender, EventArgs e)
        {
            ClearErrors();
            LoadSelectedArrayToGrid(true);
        }

        private void LoadSelectedArrayToGrid(bool toSortedGrid)
        {
            if (comboBoxArrayList.SelectedItem == null)
            {
                errorProvider.SetError(comboBoxArrayList, "Выберите массив из списка");
                return;
            }

            var selectedItem = (ArrayItem)comboBoxArrayList.SelectedItem;

            try
            {
                using (var conn = new NpgsqlConnection(dbConfig.GetConnectionString()))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand("SELECT array_data FROM arrays WHERE id = @id", conn))
                    {
                        cmd.Parameters.AddWithValue("id", selectedItem.Id);
                        var result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            var arrayData = (int[])result;
                            var listData = new List<int>(arrayData);

                            if (toSortedGrid)
                            {
                                if (!selectedItem.IsSorted)
                                {
                                    errorProvider.SetError(comboBoxArrayList, "Выбранный массив не отсортирован");
                                    return;
                                }
                                sortedArray = listData;
                                textBoxArrayB.Text = selectedItem.Name ?? "";
                            }
                            else
                            {
                                if (selectedItem.IsSorted)
                                {
                                    errorProvider.SetError(comboBoxArrayList, "Выбранный массив уже отсортирован");
                                    return;
                                }
                                originalArray = listData;
                                textBoxArrayA.Text = selectedItem.Name ?? "";
                            }

                            UpdateGridView();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorProvider.SetError(comboBoxArrayList, $"Ошибка загрузки массива: {ex.Message}");
            }
        }

        private void checkBoxNotSorted_CheckedChanged(object sender, EventArgs e)
        {
            ClearErrors();
            LoadArraysList();
        }

        private void checkBoxSorted_CheckedChanged(object sender, EventArgs e)
        {
            ClearErrors();
            LoadArraysList();
        }

        private void maskedTextBoxArraySize_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void maskedTextBoxRandBegin_KeyPress(object sender, KeyPressEventArgs e)
        {
            var textBox = (MaskedTextBox)sender;
            if (e.KeyChar == '-' && textBox.Text.Length == 0)
            {
                return; // Разрешаем минус только в начале
            }
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void maskedTextBoxRandEnd_KeyPress(object sender, KeyPressEventArgs e)
        {
            var textBox = (MaskedTextBox)sender;
            if (e.KeyChar == '-' && textBox.Text.Length == 0)
            {
                return;
            }
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void dataGridViewArrayA_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    var cellValue = dataGridViewArrayA.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    if (cellValue != null && int.TryParse(cellValue.ToString(), out int value))
                    {
                        if (e.RowIndex < originalArray.Count)
                        {
                            originalArray[e.RowIndex] = value;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка редактирования: {ex.Message}");
            }
        }
    }
}
