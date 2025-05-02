using System;
using System.Collections.Generic;

namespace CSharpFundamentals
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> items = new List<string>();
            bool running = true;

            Console.WriteLine("Welcome to String List Manager!");

            while (running)
            {
                Console.WriteLine("\nPlease choose an option:");
                Console.WriteLine("1. Add an item");
                Console.WriteLine("2. Remove an item");
                Console.WriteLine("3. Display all items");
                Console.WriteLine("4. Exit");
                Console.Write("\nEnter your choice (1-4): ");

                string choice = Console.ReadLine()?.Trim();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter the item to add: ");
                        string newItem = Console.ReadLine()?.Trim();
                        if (!string.IsNullOrEmpty(newItem))
                        {
                            items.Add(newItem.ToUpper());
                            Console.WriteLine($"Added: {newItem.ToUpper()}");
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Item not added.");
                        }
                        break;

                    case "2":
                        if (items.Count == 0)
                        {
                            Console.WriteLine("The list is empty!");
                            break;
                        }

                        Console.WriteLine("\nCurrent items:");
                        for (int i = 0; i < items.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {items[i]}");
                        }

                        Console.Write("\nEnter the number of the item to remove: ");
                        if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= items.Count)
                        {
                            string removedItem = items[index - 1];
                            items.RemoveAt(index - 1);
                            Console.WriteLine($"Removed: {removedItem}");
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. No item was removed.");
                        }
                        break;

                    case "3":
                        if (items.Count == 0)
                        {
                            Console.WriteLine("The list is empty!");
                        }
                        else
                        {
                            Console.WriteLine("\nCurrent items:");
                            for (int i = 0; i < items.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}. {items[i]}");
                            }
                        }
                        break;

                    case "4":
                        running = false;
                        Console.WriteLine("User Exited the program");
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }

        }
    }
} 