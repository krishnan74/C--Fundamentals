using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpFundamentals
{
    public class Student
    {
        public string Name { get; set; }
        public int Grade { get; set; }
        public int Age { get; set; }

        public Student(string name, int grade, int age)
        {
            Name = name;
            Grade = grade;
            Age = age;
        }

        public override string ToString()
        {
            return $"Name: {Name}, Grade: {Grade}, Age: {Age}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create a list of students with sample data
            List<Student> students = new List<Student>
            {
                new Student("John Smith", 85, 18),
                new Student("Emma Johnson", 92, 17),
                new Student("Michael Brown", 78, 19),
                new Student("Sarah Davis", 95, 18),
                new Student("David Wilson", 88, 17),
                new Student("Lisa Anderson", 82, 19),
                new Student("James Taylor", 90, 18),
                new Student("Emily White", 87, 17)
            };

            Console.WriteLine("Student Management System");
            Console.WriteLine("------------------------");

            while (true)
            {
                Console.WriteLine("\nPlease choose an option:");
                Console.WriteLine("1. Display all students");
                Console.WriteLine("2. Filter students by grade threshold");
                Console.WriteLine("3. Sort students by name");
                Console.WriteLine("4. Sort students by grade");
                Console.WriteLine("5. Exit");
                Console.Write("\nEnter your choice (1-5): ");

                string choice = Console.ReadLine()?.Trim();

                switch (choice)
                {
                    case "1":
                        DisplayStudents(students);
                        break;

                    case "2":
                        Console.Write("Enter minimum grade threshold: ");
                        if (int.TryParse(Console.ReadLine(), out int threshold))
                        {
                            var filteredStudents = students.Where(s => s.Grade >= threshold)
                                                         .OrderByDescending(s => s.Grade)
                                                         .ThenBy(s => s.Name);
                            Console.WriteLine($"\nStudents with grade >= {threshold}:");
                            DisplayStudents(filteredStudents);
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter a valid number.");
                        }
                        break;

                    case "3":
                        var sortedByName = students.OrderBy(s => s.Name);
                        Console.WriteLine("\nStudents sorted by name:");
                        DisplayStudents(sortedByName);
                        break;

                    case "4":
                        var sortedByGrade = students.OrderByDescending(s => s.Grade);
                        Console.WriteLine("\nStudents sorted by grade (highest to lowest):");
                        DisplayStudents(sortedByGrade);
                        break;

                    case "5":
                        Console.WriteLine("Thank you for using Student Management System!");
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void DisplayStudents(IEnumerable<Student> students)
        {
            Console.WriteLine("\nStudent List:");
            Console.WriteLine("-------------");
            foreach (var student in students)
            {
                Console.WriteLine(student);
            }
        }
    }
} 