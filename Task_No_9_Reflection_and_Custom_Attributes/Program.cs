using System;
using System.Linq;
using System.Reflection;

// Define a custom attribute
[AttributeUsage(AttributeTargets.Method)]
public class RunnableAttribute : Attribute
{
}

// Create some sample classes with [Runnable] methods
public class MathOperations
{
    [Runnable]
    public void Add()
    {
        Console.WriteLine("Add: 2 + 3 = " + (2 + 3));
    }

    public void NotRunnable()
    {
        Console.WriteLine("This won't run.");
    }

    [Runnable]
    public void Multiply()
    {
        Console.WriteLine("Multiply: 4 * 5 = " + (4 * 5));
    }
}

public class StringOperations
{
    [Runnable]
    public void Print()
    {
        Console.WriteLine("From StringOperations!");
    }

    [Runnable]
    public void UpperCase()
    {
        Console.WriteLine("Test -> " + "test".ToUpper());
    }
}

// Reflection runner
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Discovering and executing methods marked with [Runnable]...\n");

        // Get all types in the current assembly
        var types = Assembly.GetExecutingAssembly().GetTypes();

        foreach (var type in types)
        {
            // Get all public instance methods
            var methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);

            foreach (var method in methods)
            {
                // Check if method has [Runnable] attribute
                if (method.GetCustomAttribute<RunnableAttribute>() != null)
                {
                    try
                    {
                        // Create an instance of the class
                        var instance = Activator.CreateInstance(type);

                        // Invoke the method
                        Console.WriteLine($"Executing {type.Name}.{method.Name}()");
                        method.Invoke(instance, null);
                        Console.WriteLine();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error executing {type.Name}.{method.Name}: {ex.Message}");
                    }
                }
            }
        }

        Console.WriteLine("Execution complete.");
    }
}
