using System.Xml;

namespace WinFormsRPC
{
    public class DatabaseConfig
    {
        public string Host { get; set; } = "localhost";
        public string Port { get; set; } = "5432";
        public string Username { get; set; } = "postgres";
        public string Password { get; set; } = "qweqwer";
        public string Database { get; set; } = "arrays_db";

        public string GetConnectionString()
        {
            return $"Host={Host};Port={Port};Username={Username};Password={Password};Database={Database}";
        }
    }
}