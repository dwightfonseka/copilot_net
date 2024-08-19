// Lab Exercise 0: Console C# Application with GitHub Copilot in Visual Studio Community 2022
// Objective:
// By the end of this lab exercise, you will be able to:
// - Integrate GitHub Copilot into Visual Studio Community 2022.
// - Utilize Copilot’s code suggestions to enhance your C# development workflow.
// - Configure and customize Copilot settings to suit your project needs.
// - Develop a simple C# console application using Copilot’s code completion features.

using System;

namespace CopilotCSharpApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}

// Lab Exercise 4: Exploring GitHub Copilot Basic Features and Applications in Visual Studio Community 2022
// Objective:
// By the end of this lab exercise, you will be able to:
// - Understand and utilize GitHub Copilot’s basic features for code completion and suggestions.
// - Apply Copilot in real-world coding scenarios to enhance productivity.
// - Write and test sample code in C# using Copilot.

using System;

namespace CopilotBasicsCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter two numbers:");
            int num1 = int.Parse(Console.ReadLine());
            int num2 = int.Parse(Console.ReadLine());

            int sum = CalculateSum(num1, num2);
            Console.WriteLine($"Sum: {sum}");

            int max = Max(num1, num2);
            Console.WriteLine($"Max: {max}");
        }

        public static int CalculateSum(int a, int b)
        {
            return a + b; // Copilot Suggestion
        }

        // Function to return the maximum of two numbers
        public static int Max(int x, int y)
        {
            return x > y ? x : y; // Copilot Suggestion
        }
    }
}

// Lab Exercise 6: Input Validation and Error Handling Using GitHub Copilot in Visual Studio Community 2022
// Objective:
// By the end of this lab exercise, you will be able to:
// - Implement input validation and error handling techniques in your C# code using GitHub Copilot.
// - Utilize Copilot to assist in generating and improving validation logic and error handling mechanisms.
// - Apply these techniques to ensure robust and reliable C# console applications.

using System;

namespace CopilotValidationCSharp
{
    class Program
    {
        /// <summary>
        /// Main method to accept and validate user input and handle potential errors.
        /// </summary>
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a number:");
            string input = Console.ReadLine();
            int number;

            try
            {
                if (int.TryParse(input, out number))
                {
                    if (number < 0)
                    {
                        Console.WriteLine("Please enter a positive number.");
                    }
                    else
                    {
                        Console.WriteLine($"You entered: {number}");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}

// Lab Exercise 7: Optimizing Code for Speed and Performance Using GitHub Copilot in Visual Studio Community 2022
// Objective:
// By the end of this lab exercise, you will be able to:
// - Identify performance bottlenecks in C# code and optimize them using GitHub Copilot.
// - Refactor and improve code efficiency by leveraging Copilot’s suggestions.
// - Apply optimization techniques to enhance the performance of a C# console application.

using System;
using System.Collections.Generic;

namespace CopilotOptimizationCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            int limit = 10000;
            Console.WriteLine($"Finding prime numbers up to {limit}...");
            var primes = FindPrimes(limit);
            Console.WriteLine($"Number of primes found: {primes.Length}");
        }

        /// <summary>
        /// Finds all prime numbers up to the specified limit using the Sieve of Eratosthenes algorithm.
        /// </summary>
        /// <param name="limit">The upper bound for finding prime numbers.</param>
        /// <returns>An array of prime numbers.</returns>
        public static int[] FindPrimes(int limit)
        {
            bool[] isPrime = new bool[limit + 1];
            for (int i = 2; i <= limit; i++)
            {
                isPrime[i] = true;
            }

            for (int i = 2; i <= Math.Sqrt(limit); i++)
            {
                if (isPrime[i])
                {
                    for (int j = i * i; j <= limit; j += i)
                    {
                        isPrime[j] = false;
                    }
                }
            }

            var primes = new List<int>();
            for (int i = 2; i <= limit; i++)
            {
                if (isPrime[i])
                {
                    primes.Add(i);
                }
            }
            return primes.ToArray();
        }
    }
}
```

You can now place this code into a `.cs` file, and it will be organized and commented as per the lab exercises outlined in the document.