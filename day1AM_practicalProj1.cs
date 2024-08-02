// Practical Project 1: Utilizing GitHub Copilot for Console C# Development in Visual Studio Community 2022
// Objective:
// By the end of this lab exercise, you will be able to:
// - Integrate and use GitHub Copilot to assist with C# code development.
// - Leverage Copilot’s code completion, refactoring, and documentation features.
// - Apply best practices in coding, testing, and error handling in a console C# application.

using System;
using System.Linq;

namespace CopilotCSharpApp
{
    class Program
    {
        /// <summary>
        /// Main entry point of the application
        /// </summary>
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Enter two numbers:");
                int num1 = int.Parse(Console.ReadLine());
                int num2 = int.Parse(Console.ReadLine());

                int sum = AddNumbers(num1, num2);
                Console.WriteLine($"Sum: {sum}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Adds two non-negative integers.
        /// </summary>
        /// <param name="a">The first integer.</param>
        /// <param name="b">The second integer.</param>
        /// <returns>The sum of the two integers.</returns>
        public static int AddNumbers(int a, int b)
        {
            if (a < 0 || b < 0)
            {
                throw new ArgumentException("Inputs must be non-negative.");
            }
            return a + b;
        }

        // Example of optimized code using LINQ
        public static int SumRange(int start, int end)
        {
            return Enumerable.Range(start, end).Sum();
        }
    }
}
