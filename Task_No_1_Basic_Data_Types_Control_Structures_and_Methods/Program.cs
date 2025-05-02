using System;

namespace CSharpFundamentals
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Factorial Calculator");
            Console.WriteLine("-------------------");

            while (true)
            {
                Console.Write("\nEnter a positive integer to calculate factorial: ");
                string input = Console.ReadLine()?.Trim().ToLower();

                if (int.TryParse(input, out int number))
                {
                    if (number < 0)
                    {
                        Console.WriteLine("Error: Please enter a positive integer.");
                        continue;
                    }

                    // Calculate factorial using recursion
                    long recursiveResult = CalculateFactorial(number);

                    Console.WriteLine($"\nFactorial of {number}:");
                    Console.WriteLine($"Using recursion: {recursiveResult}");
                }
                else
                {
                    Console.WriteLine("Error: Please enter a valid integer.");
                }
            }

        }

        // Method to calculate factorial using recursion
        static long CalculateFactorial(int n)
        {
            if (n == 0 || n == 1)
                return 1;

            return n * CalculateFactorial(n - 1);
        }
    }
} 