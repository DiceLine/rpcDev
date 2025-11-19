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

        public MainForm()
        {
            InitializeComponent();

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

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (!haveSqlMode)
            {
                MessageBox.Show("Включите режим работы с БД!");
                return;
            }

            try
            {
                EnsureDatabaseStructure();

                using (var conn = new NpgsqlConnection(dbConfig.GetConnectionString()))
                {
                    conn.Open();

                    // Сохраняем исходный массив
                    if (originalArray.Count > 0)
                    {
                        SaveArrayToDatabase(conn,
                            string.IsNullOrWhiteSpace(textBoxArrayA.Text) ? null : textBoxArrayA.Text,
                            originalArray, false);
                    }

                    // Сохраняем отсортированный массив
                    if (sortedArray.Count > 0)
                    {
                        SaveArrayToDatabase(conn,
                            string.IsNullOrWhiteSpace(textBoxArrayB.Text) ? null : textBoxArrayB.Text,
                            sortedArray, true);
                    }
                }

                MessageBox.Show("Массивы сохранены в БД!");
                LoadArraysList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}");
            }

        }

        private void EnsureDatabaseStructure()
        {
            try
            {
                using (var conn = new NpgsqlConnection(dbConfig.GetConnectionString()))
                {
                    conn.Open();

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

                        MessageBox.Show("Таблица 'arrays' создана в базе данных.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка создания структуры БД: {ex.Message}");
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
            comboBoxArrayList.Items.Clear();

            if (!haveSqlMode) return;

            try
            {
                using (var conn = new NpgsqlConnection(dbConfig.GetConnectionString()))
                {
                    conn.Open();

                    var whereConditions = new List<string>();
                    var parameters = new List<NpgsqlParameter>();

                    // Фильтрация по статусу сортировки
                    if (checkBoxSorted.Checked && !checkBoxNotSorted.Checked)
                    {
                        whereConditions.Add("is_sorted = true");
                    }
                    else if (checkBoxNotSorted.Checked && !checkBoxSorted.Checked)
                    {
                        whereConditions.Add("is_sorted = false");
                    }
                    // Если оба checked или оба unchecked - показываем все

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
                MessageBox.Show($"Ошибка загрузки списка: {ex.Message}");
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

        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {

        }

        private void textBoxArrayA_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonSqlMode_Click(object sender, EventArgs e)
        {
            if (!haveSqlMode)
            {
                comboBoxArrayList.Visible = true;
                textBoxArrayA.Visible = true;
                textBoxArrayB.Visible = true;
                buttonSave.Enabled = true;
                groupBoxArrayList.Visible = true;
                haveSqlMode = true;
                return;
            }

            comboBoxArrayList.Visible = false;
            textBoxArrayA.Visible = false;
            textBoxArrayB.Visible = false;
            buttonSave.Enabled = false;
            groupBoxArrayList.Visible = false;
            haveSqlMode = false;
        }

        private void buttonRandom_Click(object sender, EventArgs e)
        {
            if (!haveRandomMode)
            {

                haveRandomMode = true;
                return;
            }
            haveRandomMode = false;
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
            if (!haveTransferMode)
            {
                showControl(groupBoxTransfer, out haveTransferMode);
                hideControl(groupBoxFilter, out isFilterMenuOpen);
                return;
            }
            hideControl(groupBoxTransfer, out haveTransferMode);
        }

        private void buttonFilter_Click(object sender, EventArgs e)
        {
            if (!isFilterMenuOpen)
            {
                showControl(groupBoxFilter, out isFilterMenuOpen);
                hideControl(groupBoxTransfer, out haveTransferMode);
                return;
            }
            hideControl(groupBoxFilter, out isFilterMenuOpen);
        }
    }
}
