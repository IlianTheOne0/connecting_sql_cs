namespace Data.Database.Configs;

public class DatabaseConfigs
{
    internal const string ServerConnectionString = @"Server=localhost,1433;Database=master;User Id=sa;Password=Password123;TrustServerCertificate=True;";
    internal const string DatabaseName = "TestDB";
    internal const string DatabaseConnectionString = @"Server=localhost,1433;Database=master;User Id=sa;Password=Password123;TrustServerCertificate=True;";
    internal const string UseMaster = "USE master;";
    internal const string UseDatabase = "USE TestDB;";
}