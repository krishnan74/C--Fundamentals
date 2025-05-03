using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSharpFundamentals
{
    // Class to represent data source
    public class DataSource
    {
        public string Name { get; }
        public int DelayMilliseconds { get; }

        public DataSource(string name, int delayMilliseconds)
        {
            Name = name;
            DelayMilliseconds = delayMilliseconds;
        }
    }

    // Class to represent data result
    public class DataResult
    {
        public string SourceName { get; }
        public string Data { get; }
        public bool IsSuccess { get; }
        public string ErrorMessage { get; }

        public DataResult(string sourceName, string data, bool isSuccess, string errorMessage = null)
        {
            SourceName = sourceName;
            Data = data;
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
        }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Async Data Fetching Demo");
            Console.WriteLine("------------------------");

            try
            {
                // Create a list of data sources
                var sources = new List<DataSource>
                {
                    new DataSource("Database", 2000),
                    new DataSource("API", 3000),
                    new DataSource("File System", 1500),
                    new DataSource("Cache", 500)
                };

                var tasks = new List<Task<DataResult>>();

                // Start all fetch operations concurrently
                foreach (var source in sources)
                {
                    tasks.Add(FetchDataAsync(source));
                }

                // Wait for all tasks to complete
                var results =  new List<DataResult>(await Task.WhenAll(tasks));

                // Display results
                Console.WriteLine("\nFetching Results:");
                Console.WriteLine("-----------------");
                foreach (var result in results)
                {
                    if (result.IsSuccess)
                    {
                        Console.WriteLine($"✓ {result.SourceName}: {result.Data}");
                    }
                    else
                    {
                        Console.WriteLine($"✗ {result.SourceName}: Failed - {result.ErrorMessage}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nAn unexpected error occurred: {ex.Message}");
            }

        }

        // Method to fetch data from a single source
        static async Task<DataResult> FetchDataAsync(DataSource source)
        {
            try
            {
                Console.WriteLine($"Starting fetch from {source.Name}...");
                
                // Simulate network delay
                await Task.Delay(source.DelayMilliseconds);

                Console.WriteLine($"Finished fetch from {source.Name}.");

                // Simulate random failures (20% chance)
                if (new Random().Next(100) < 20)
                {
                    throw new Exception($"Simulated failure in {source.Name}");
                }

                // Simulate successful data fetch
                return new DataResult(
                    source.Name,
                    $"Data from {source.Name} fetched successfully",
                    true
                );
            }
            catch (Exception ex)
            {
                
                // return failure result
                return new DataResult(
                    source.Name,
                    null,
                    false,
                    ex.Message
                );
            }
        }
    }
}