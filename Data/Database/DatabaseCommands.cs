namespace Data.Database.Commands;
using Data.Database.Configs;
using Microsoft.Data.SqlClient;

public class DatabaseCommands
{
    public static async Task CreateDatabaseIfNotExist()
    {
        try
        {
            await using var connection = new SqlConnection(DatabaseConfigs.ServerConnectionString);
            await connection.OpenAsync();

            var useMasterCommand = new SqlCommand(DatabaseConfigs.UseMaster, connection);
            await useMasterCommand.ExecuteNonQueryAsync();

            var command = new SqlCommand($"IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = '{DatabaseConfigs.DatabaseName}') " +
                                         $"CREATE DATABASE {DatabaseConfigs.DatabaseName}", connection);
            await command.ExecuteNonQueryAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while creating the database: {e.Message}");
            throw new Exception($"Failed to create database '{DatabaseConfigs.DatabaseName}'. ", e);
        }
    }

    public static async Task DropDatabaseIfExist()
    {
        try
        {
            await using var connection = new SqlConnection(DatabaseConfigs.ServerConnectionString);
            await connection.OpenAsync();

            var useMasterCommand = new SqlCommand(DatabaseConfigs.UseMaster, connection);
            await useMasterCommand.ExecuteNonQueryAsync();

            var command = new SqlCommand($"IF EXISTS (SELECT * FROM sys.databases WHERE name = '{DatabaseConfigs.DatabaseName}') " +
                                         $"DROP DATABASE {DatabaseConfigs.DatabaseName}", connection);
            await command.ExecuteScalarAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while dropping the database: {e.Message}");
            throw new Exception($"Failed to drop database '{DatabaseConfigs.DatabaseName}'. ", e);
        }
    }

    public static async Task CreateTableIfNotExist(string tableName)
    {
        try
        {
            await using var connection = new SqlConnection(DatabaseConfigs.DatabaseConnectionString);
            await connection.OpenAsync();

            var useDatabaseCommand = new SqlCommand(DatabaseConfigs.UseDatabase, connection);
            await useDatabaseCommand.ExecuteNonQueryAsync();

            string createTableQuery = $"CREATE TABLE {tableName} (Id INT PRIMARY KEY, Name NVARCHAR(100))";

            var command = new SqlCommand(createTableQuery, connection);
            await command.ExecuteNonQueryAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while creating the table: {e.Message}");
            throw new Exception($"Failed to create table '{tableName}'. ", e);
        }
    }

    public static async Task DropTableIfExist(string tableName)
    {
        try
        {
            await using var connection = new SqlConnection(DatabaseConfigs.DatabaseConnectionString);
            await connection.OpenAsync();

            var useDatabaseCommand = new SqlCommand(DatabaseConfigs.UseDatabase, connection);
            await useDatabaseCommand.ExecuteNonQueryAsync();

            var command = new SqlCommand($"IF EXISTS (SELECT * FROM sys.tables WHERE name = '{tableName}') " +
                                         $"DROP TABLE {tableName}", connection);
            await command.ExecuteNonQueryAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while dropping the table: {e.Message}");
            throw new Exception($"Failed to drop table '{tableName}'. ", e);
        }
    }

    public static async Task ListTables()
    {
        try
        {
            await using var connection = new SqlConnection(DatabaseConfigs.DatabaseConnectionString);
            await connection.OpenAsync();

            var useDatabaseCommand = new SqlCommand(DatabaseConfigs.UseDatabase, connection);
            await useDatabaseCommand.ExecuteNonQueryAsync();

            var command = new SqlCommand("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'", connection);
            await using var reader = await command.ExecuteReaderAsync();

            Console.WriteLine("Tables in the database:");
            while (await reader.ReadAsync()) { Console.WriteLine(reader.GetString(0)); }
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while listing the tables: {e.Message}");
            throw new Exception("Failed to list tables.", e);
        }
    }
}