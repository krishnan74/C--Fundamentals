using System;

namespace CSharpFundamentals
{
    public class Person
    {
        // Properties
        public string Name { get; set; }
        public int Age { get; set; }

        // Constructor
        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        // Method to introduce the person
        public void Introduce()
        {
            Console.WriteLine($"Hello! My name is {Name} and I am {Age} years old.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create multiple Person objects
            Person person1 = new Person("Saul Goodman", 25);
            Person person2 = new Person("Walter White", 30);
            Person person3 = new Person("Jesse Pinkman", 45);

            // Call Introduce() method on each person
            Console.WriteLine("Introducing our team:");
            Console.WriteLine("------------------------");
            person1.Introduce();
            person2.Introduce();
            person3.Introduce();

            
        }
    }
} 