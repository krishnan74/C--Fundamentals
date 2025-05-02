using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace CSharpFundamentals
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("File Processing Application");
            Console.WriteLine("-------------------------");

            while (true)
            {
                Console.WriteLine("\nPlease choose an option:");
                Console.WriteLine("1. Process a text file");
                Console.WriteLine("2. Exit");
                Console.Write("\nEnter your choice (1-2): ");

                string choice = Console.ReadLine()?.Trim();

                switch (choice)
                {
                    case "1":
                        ProcessFile();
                        break;

                    case "2":
                        Console.WriteLine("Thank you for using File Processing Application!");
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void ProcessFile()
        {
            try
            {
                // Get input file path
                Console.Write("\nEnter the path to the input file: ");
                string inputPath = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(inputPath))
                {
                    Console.WriteLine("Error: File path cannot be empty.");
                    return;
                }

                // Read and process the file
                string[] lines = File.ReadAllLines(inputPath);
                var statistics = AnalyzeFile(lines);

                // Generate output file path
                string outputPath = Path.Combine(
                    Path.GetDirectoryName(inputPath),
                    Path.GetFileNameWithoutExtension(inputPath) + "_analysis.txt"
                );

                // Write results to output file
                WriteResults(outputPath, statistics);

                Console.WriteLine($"\nFile processed successfully!");
                Console.WriteLine($"Results written to: {outputPath}");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Error: The specified file was not found.");
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Error: The specified directory was not found.");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Error: You do not have permission to access this file.");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error: An I/O error occurred: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: An unexpected error occurred: {ex.Message}");
            }
        }

        static Dictionary<string, int> AnalyzeFile(string[] lines)
        {
            var statistics = new Dictionary<string, int>
            {
                ["Total Lines"] = lines.Length,
                ["Total Words"] = 0,
                ["Empty Lines"] = 0,
                ["Lines with Numbers"] = 0
            };

            foreach (string line in lines)
            {
                // Count words
                string[] words = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                statistics["Total Words"] += words.Length;

                // Count empty lines
                if (string.IsNullOrWhiteSpace(line))
                {
                    statistics["Empty Lines"]++;
                }

                // Count lines with numbers
                if (line.Any(char.IsDigit))
                {
                    statistics["Lines with Numbers"]++;
                }
            }

            return statistics;
        }

        static void WriteResults(string outputPath, Dictionary<string, int> statistics)
        {
            using (StreamWriter writer = new StreamWriter(outputPath))
            {
                writer.WriteLine("File Analysis Results");
                writer.WriteLine("--------------------");
                writer.WriteLine($"Analysis Date: {DateTime.Now}");
                writer.WriteLine();

                foreach (var stat in statistics)
                {
                    writer.WriteLine($"{stat.Key}: {stat.Value}");
                }
            }
        }
    }
} 