using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.IO;
using System.Threading.Tasks;

namespace UBB_SE_2025_EUROTRUCKERS.Data
{
    public class DatabaseInitializer
    {
        private readonly TransportDbContext _context;
        private readonly string _schemaScriptPath;
        private readonly string _mockDataScriptPath;

        public DatabaseInitializer(TransportDbContext context,
                                string schemaScriptPath = null,
                                string mockDataScriptPath = null)
        {
            _context = context;
            _schemaScriptPath = schemaScriptPath ?? GetDefaultScriptPath("schema.sql");
            _mockDataScriptPath = mockDataScriptPath ?? GetDefaultScriptPath("mock_data.sql");
        }

        private string GetDefaultScriptPath(string fileName)
        {
            return Path.Combine(AppContext.BaseDirectory, "Database", fileName);
        }

        public async Task InitializeDatabaseAsync()
        {
            // Verifying if database exists
            bool databaseExists = await _context.Database.CanConnectAsync();

            if (!databaseExists)
            {
                await CreateDatabaseFromScriptsAsync();
            }
            else
            {
                // Verify if tables exist
                var tablesExist = await _context.Deliveries.AnyAsync();
                if (!tablesExist)
                {
                    await CreateDatabaseFromScriptsAsync();
                }
            }
        }

        private async Task CreateDatabaseFromScriptsAsync()
        {
            var connectionString = "Host=localhost;Username=postgres;Password=admin;";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();

                // Execute schema.sql
                var schemaScript = await File.ReadAllTextAsync(_schemaScriptPath);
                await ExecuteScriptAsync(connection, schemaScript);

                // Execute mock_data.sql
                var mockDataScript = await File.ReadAllTextAsync(_mockDataScriptPath);
                await ExecuteScriptAsync(connection, mockDataScript);
            }
        }

        private async Task ExecuteScriptAsync(NpgsqlConnection connection, string script)
        {
            // Divide script into individual commands
            var commands = script.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var commandText in commands)
            {
                if (string.IsNullOrWhiteSpace(commandText.Trim()))
                    continue;

                using (var command = new NpgsqlCommand(commandText, connection))
                {
                    try
                    {
                        await command.ExecuteNonQueryAsync();
                    }
                    catch (Exception ex)
                    {
                        // Log error but continue
                        Console.WriteLine($"Error executing command: {commandText}");
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
    }
}
