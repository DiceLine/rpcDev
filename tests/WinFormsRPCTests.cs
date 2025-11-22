using Npgsql;
using System.Diagnostics;
using System.Xml;
using WinFormsRPC;

namespace WinFormsRPCTest
{
    [TestClass]
    public class WinFormsRPCTests
    {
        private DatabaseConfig dbConfig;
        private Random random = new Random();
        private string configFilePath = "config.xml";

        [TestInitialize]
        public void TestInitialize()
        {
            // Загружаем конфигурацию как в основном проекте
            dbConfig = LoadDatabaseConfig();
            EnsureDatabaseAndTable();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            // Очищаем базу после КАЖДОГО теста
            ClearDatabase();
        }

        // === ГРУППА A: ТЕСТЫ ДОБАВЛЕНИЯ ===

        [TestMethod]
        [TestCategory("InsertTests")]
        public void TestA_Insert100Arrays()
        {
            RunInsertTest("A", 100);
        }

        [TestMethod]
        [TestCategory("InsertTests")]
        public void TestB_Insert1000Arrays()
        {
            RunInsertTest("B", 1000);
        }

        [TestMethod]
        [TestCategory("InsertTests")]
        public void TestC_Insert10000Arrays()
        {
            RunInsertTest("C", 10000);
        }

        // === ГРУППА D: ТЕСТЫ ВЫГРУЗКИ И СОРТИРОВКИ ===

        [TestMethod]
        [TestCategory("LoadSortTests")]
        public void TestD1_LoadSort100Arrays_From100()
        {
            FillDatabase(100);
            RunLoadAndSortTest("D1", 100);
        }

        [TestMethod]
        [TestCategory("LoadSortTests")]
        public void TestD2_LoadSort100Arrays_From1000()
        {
            FillDatabase(1000);
            RunLoadAndSortTest("D2", 1000);
        }

        [TestMethod]
        [TestCategory("LoadSortTests")]
        public void TestD3_LoadSort100Arrays_From10000()
        {
            FillDatabase(10000);
            RunLoadAndSortTest("D3", 10000);
        }

        // === ГРУППА E: ТЕСТЫ ОЧИСТКИ ===

        [TestMethod]
        [TestCategory("ClearTests")]
        public void TestE1_ClearDatabase_100()
        {
            FillDatabase(100);
            RunClearTest("E1", 100);
        }

        [TestMethod]
        [TestCategory("ClearTests")]
        public void TestE2_ClearDatabase_1000()
        {
            FillDatabase(1000);
            RunClearTest("E2", 1000);
        }

        [TestMethod]
        [TestCategory("ClearTests")]
        public void TestE3_ClearDatabase_10000()
        {
            FillDatabase(10000);
            RunClearTest("E3", 10000);
        }

        // === ОСНОВНЫЕ МЕТОДЫ ТЕСТОВ ===

        private void RunInsertTest(string testId, int arrayCount)
        {
            var stopwatch = Stopwatch.StartNew();
            bool success = true;
            string errorMessage = "";

            try
            {
                using (var conn = new NpgsqlConnection(dbConfig.GetConnectionString()))
                {
                    conn.Open();

                    for (int i = 0; i < arrayCount; i++)
                    {
                        var array = GenerateRandomArray();
                        SaveArrayToDatabase(conn, $"test_array_{i}", array, false);

                        if (arrayCount >= 1000 && i % 100 == 0)
                        {
                            Debug.WriteLine($"  Прогресс: {i}/{arrayCount}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                success = false;
                errorMessage = ex.Message;
            }

            stopwatch.Stop();

            Assert.IsTrue(success, errorMessage);
            Debug.WriteLine($"Тест {testId}: Добавление {arrayCount} массивов - УСПЕХ | Время: {stopwatch.Elapsed.TotalMilliseconds:F2} мс");
        }

        private void RunLoadAndSortTest(string testId, int databaseSize)
        {
            var stopwatch = Stopwatch.StartNew();
            bool success = true;
            string errorMessage = "";
            double averageTimePerArray = 0;

            try
            {
                var arrayIds = GetRandomArrayIds(100);
                var sortTimes = new List<TimeSpan>();

                using (var conn = new NpgsqlConnection(dbConfig.GetConnectionString()))
                {
                    conn.Open();

                    foreach (var arrayId in arrayIds)
                    {
                        var arrayStopwatch = Stopwatch.StartNew();

                        var arrayData = LoadArrayFromDatabase(conn, arrayId);
                        QuickSort(arrayData);

                        arrayStopwatch.Stop();
                        sortTimes.Add(arrayStopwatch.Elapsed);
                    }
                }

                averageTimePerArray = sortTimes.Average(ts => ts.TotalMilliseconds);
            }
            catch (Exception ex)
            {
                success = false;
                errorMessage = ex.Message;
            }

            stopwatch.Stop();

            Assert.IsTrue(success, errorMessage);
            Debug.WriteLine($"Тест {testId}: Выгрузка и сортировка 100 массивов (база: {databaseSize}) - УСПЕХ | " +
                          $"Общее время: {stopwatch.Elapsed.TotalMilliseconds:F2} мс | " +
                          $"Среднее на массив: {averageTimePerArray:F2} мс");
        }

        private void RunClearTest(string testId, int databaseSize)
        {
            var stopwatch = Stopwatch.StartNew();
            bool success = true;
            string errorMessage = "";

            try
            {
                using (var conn = new NpgsqlConnection(dbConfig.GetConnectionString()))
                {
                    conn.Open();

                    // Проверяем начальное количество
                    var countBefore = GetArrayCount(conn);
                    Assert.AreEqual(databaseSize, countBefore, $"Ожидалось {databaseSize} записей перед очисткой");

                    // Очищаем базу
                    ClearDatabase(conn);

                    // Проверяем конечное количество
                    var countAfter = GetArrayCount(conn);
                    Assert.AreEqual(0, countAfter, "База должна быть пустой после очистки");
                }
            }
            catch (Exception ex)
            {
                success = false;
                errorMessage = ex.Message;
            }

            stopwatch.Stop();

            Assert.IsTrue(success, errorMessage);
            Debug.WriteLine($"Тест {testId}: Очистка базы данных (база: {databaseSize}) - УСПЕХ | Время: {stopwatch.Elapsed.TotalMilliseconds:F2} мс");
        }

        // === КОНФИГУРАЦИЯ БАЗЫ ДАННЫХ ===

        private DatabaseConfig LoadDatabaseConfig()
        {
            var config = new DatabaseConfig();

            try
            {
                if (File.Exists(configFilePath))
                {
                    var xmlDoc = new XmlDocument();
                    xmlDoc.Load(configFilePath);

                    var root = xmlDoc.DocumentElement;
                    config.Host = root.SelectSingleNode("Host")?.InnerText ?? "localhost";
                    config.Port = root.SelectSingleNode("Port")?.InnerText ?? "5432";
                    config.Username = root.SelectSingleNode("Username")?.InnerText ?? "postgres";
                    config.Password = root.SelectSingleNode("Password")?.InnerText ?? "";
                    config.Database = root.SelectSingleNode("Database")?.InnerText ?? "arrays_db";
                }
                else
                {
                    // Создаем конфиг по умолчанию
                    CreateDefaultConfig(config);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка загрузки конфигурации: {ex.Message}");
            }

            return config;
        }

        private void CreateDefaultConfig(DatabaseConfig config)
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

                AddConfigElement("Host", config.Host);
                AddConfigElement("Port", config.Port);
                AddConfigElement("Username", config.Username);
                AddConfigElement("Password", config.Password);
                AddConfigElement("Database", config.Database);

                xmlDoc.Save(configFilePath);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка создания конфигурации: {ex.Message}");
            }
        }

        // === ВСПОМОГАТЕЛЬНЫЕ МЕТОДЫ БАЗЫ ДАННЫХ ===

        private void EnsureDatabaseAndTable()
        {
            try
            {
                using (var conn = new NpgsqlConnection(dbConfig.GetConnectionString()))
                {
                    try
                    {
                        conn.Open();
                        EnsureTableExists(conn);
                    }
                    catch (PostgresException ex) when (ex.SqlState == "3D000")
                    {
                        CreateDatabase();
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
                Assert.Fail($"Ошибка инициализации БД: {ex.Message}");
            }
        }

        private void CreateDatabase()
        {
            var defaultConfig = new DatabaseConfig
            {
                Host = dbConfig.Host,
                Port = dbConfig.Port,
                Username = dbConfig.Username,
                Password = dbConfig.Password,
                Database = "postgres"
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
            var checkTableCmd = new NpgsqlCommand(
                "SELECT EXISTS (SELECT FROM information_schema.tables WHERE table_name = 'arrays')", conn);
            bool tableExists = (bool)checkTableCmd.ExecuteScalar();

            if (!tableExists)
            {
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

        private List<int> GenerateRandomArray()
        {
            int size = random.Next(10, 1000);
            var array = new List<int>();

            for (int i = 0; i < size; i++)
            {
                array.Add(random.Next(-1000, 1000));
            }

            return array;
        }

        private void SaveArrayToDatabase(NpgsqlConnection conn, string name, List<int> array, bool isSorted)
        {
            using (var cmd = new NpgsqlCommand(
                "INSERT INTO arrays (array_name, array_data, is_sorted) VALUES (@name, @data, @sorted)", conn))
            {
                cmd.Parameters.AddWithValue("name", name);
                cmd.Parameters.AddWithValue("data", array.ToArray());
                cmd.Parameters.AddWithValue("sorted", isSorted);
                cmd.ExecuteNonQuery();
            }
        }

        private int[] LoadArrayFromDatabase(NpgsqlConnection conn, int arrayId)
        {
            using (var cmd = new NpgsqlCommand("SELECT array_data FROM arrays WHERE id = @id", conn))
            {
                cmd.Parameters.AddWithValue("id", arrayId);
                var result = cmd.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    return (int[])result;
                }
            }

            throw new Exception($"Массив с ID {arrayId} не найден");
        }

        private List<int> GetRandomArrayIds(int count)
        {
            var ids = new List<int>();
            using (var conn = new NpgsqlConnection(dbConfig.GetConnectionString()))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand(
                    $"SELECT id FROM arrays ORDER BY RANDOM() LIMIT {count}", conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ids.Add(reader.GetInt32(0));
                        }
                    }
                }
            }

            return ids;
        }

        private int GetArrayCount(NpgsqlConnection conn)
        {
            using (var cmd = new NpgsqlCommand("SELECT COUNT(*) FROM arrays", conn))
            {
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private void ClearDatabase(NpgsqlConnection conn)
        {
            using (var cmd = new NpgsqlCommand("DELETE FROM arrays", conn))
            {
                cmd.ExecuteNonQuery();
            }
        }

        private void ClearDatabase()
        {
            using (var conn = new NpgsqlConnection(dbConfig.GetConnectionString()))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("DELETE FROM arrays", conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void FillDatabase(int count)
        {
            using (var conn = new NpgsqlConnection(dbConfig.GetConnectionString()))
            {
                conn.Open();

                for (int i = 0; i < count; i++)
                {
                    var array = GenerateRandomArray();
                    SaveArrayToDatabase(conn, $"fill_array_{i}", array, false);
                }
            }
        }

        // === АЛГОРИТМ БЫСТРОЙ СОРТИРОВКИ ИЗ MAINFORM ===

        private static void QuickSort(int[] array)
        {
            QuickSort(array, 0, array.Length - 1);
        }

        private static void QuickSort(int[] array, int left, int right)
        {
            if (left >= right) return;
            int pivotIndex = Partition(array, left, right);
            QuickSort(array, left, pivotIndex - 1);
            QuickSort(array, pivotIndex, right);
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

        private static void Swap(int[] array, int i, int j)
        {
            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }
}
