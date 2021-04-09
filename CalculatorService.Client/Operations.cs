using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CalculatorService.Client
{
    public static class Operations
    {
        private const string ERROR_MESSAGE = "Only integer numbers are valid.";
        private const string JOURNAL_MESSAGE = "Type a tracking Id value to view your operations later. Is optional.";

        public static async Task<bool> Sum()
        {
            Console.WriteLine("\n\n Type the numbers that you want to sum, separated with spaces.");
            string inputNumbers = Console.ReadLine();
            
            if (!Regex.IsMatch(inputNumbers, @"[0-9-]"))
            {
                Console.WriteLine(ERROR_MESSAGE);
                return true;
            }
            
            string[] split = inputNumbers.Split(" ");
            List<int> numbers = split.Select(x => int.Parse(x)).ToList();
            
            if (numbers.Count < 2)
            {
                Console.WriteLine("\n\nEnter at least two numbers to sum.");
                return true;
            }
            
            Console.WriteLine(JOURNAL_MESSAGE);
            
            string trackingId = Console.ReadLine();
            
            object content = new
            {
                Addends = numbers,
            };

            string operation = "add";
            var result = await ExecuteService.Request<object, JObject>(content, operation, trackingId.Trim().ToLower());
            var sumresult = result.Property("sum");

            if (sumresult == null)
            {
                Console.WriteLine(result.ToString());
                return true;
            }

            Console.WriteLine("\n\nThe result is: " + sumresult.Value);

            return true;
        }

        public static async Task<bool> Sub()
        {
            Console.WriteLine("\n\n Enter a minuend.");
            string minuend = Console.ReadLine();
            
            if (!Regex.IsMatch(minuend, @"[0-9-]"))
            {
                Console.WriteLine(ERROR_MESSAGE);
                return true;
            }
            
            Console.WriteLine("\n\n Enter a subtrahend.");
            string subtrahend = Console.ReadLine();
            
            if (!Regex.IsMatch(subtrahend, @"[0-9-]"))
            {
                Console.WriteLine(ERROR_MESSAGE);
                return true;
            }
            
            object content = new
            {
                Minuend = minuend,
                Subtrahend = subtrahend
            };
            
            Console.WriteLine(JOURNAL_MESSAGE);
            
            string trackingId = Console.ReadLine();

            string operation = "sub";

            var result = await ExecuteService.Request<object, JObject>(content, operation, trackingId.Trim().ToLower());
            var subresult = result.Property("difference");
            
            if (subresult == null)
            {
                Console.WriteLine(result.ToString());
                return true;
            }
            
            Console.WriteLine("\n\n The result is: " + subresult.Value);
            
            return true;
        }

        public static async Task<bool> Div()
        {
            Console.WriteLine("\n\n Enter a dividend.");
            string dividend = Console.ReadLine();

            if (!Regex.IsMatch(dividend, @"[0-9-]"))
            {
                Console.WriteLine(ERROR_MESSAGE);
                return true;
            }

            Console.WriteLine("\n\n Enter a divisor. ");
            string divisor = Console.ReadLine();

            if (!Regex.IsMatch(divisor, @"[0-9-]"))
            {
                Console.WriteLine(ERROR_MESSAGE);
                return true;
            }

            Console.WriteLine(JOURNAL_MESSAGE);
            string trackingId = Console.ReadLine();

            if (divisor.Trim() == "0")
            {
                Console.WriteLine("Invalid operation: divided by zero.");
                return true;
            }

            object content = new
            {
                Dividend = dividend,
                Divisor = divisor
            };

            string operation = "div";
            var result = await ExecuteService.Request<object, JObject>(content, operation, trackingId.Trim().ToLower());
            var quotient = result.Property("quotient");
            var remainder = result.Property("remainder");

            if (quotient == null)
            {
                Console.WriteLine(result.ToString());
                return true;
            }

            Console.WriteLine("\n\n The result of the division is " + quotient.Value + " and the remainder " + remainder.Value);

            return true;
        }

        public static async Task<bool> Mult()
        {
            Console.WriteLine("\n\n Enter the numbers that you want to multiply, separated by spaces.");
            string inputNumbers = Console.ReadLine();

            if (!Regex.IsMatch(inputNumbers, @"[0-9-]"))
            {
                Console.WriteLine(ERROR_MESSAGE);
                return true;
            }

            string[] split = inputNumbers.Split(" ");
            List<int> numbers = split.Select(x => int.Parse(x)).ToList();

            if (numbers.Count < 2)
            {
                Console.WriteLine("\n\n Enter at least two numbers to multiply");
                return true;
            }

            Console.WriteLine(JOURNAL_MESSAGE);
            string trackingId = Console.ReadLine();

            object content = new
            {
                Factors = numbers
            };

            string operation = "mult";
            var result = await ExecuteService.Request<object, JObject>(content, operation, trackingId.Trim().ToLower());
            var mulresult = result.Property("product");

            if (mulresult == null)
            {
                Console.WriteLine(result.ToString());
                return true;
            }

            Console.WriteLine("\n\n The result of the MULTIPLICATION is: " + mulresult.Value);

            return true;
        }

        public static async Task<bool> Sqrt()
        {
            Console.WriteLine("\n\n Enter a number to calculate square root.");
            string inputNumber = Console.ReadLine();

            if (!Regex.IsMatch(inputNumber, @"[0-9]"))
            {
                Console.WriteLine(ERROR_MESSAGE);
                return true;
            }

            Console.WriteLine(JOURNAL_MESSAGE);
            string trackingId = Console.ReadLine();

            object content = new
            {
                Number = inputNumber
            };

            string operation = "sqrt";
            var result = await ExecuteService.Request<object, JObject>(content, operation, trackingId.Trim().ToLower());
            var sqrtresult = result.Property("square");

            if (sqrtresult == null)
            {
                Console.WriteLine(result.ToString());
                return true;
            }

            Console.WriteLine("\n\n The square root is: " + sqrtresult.Value);
            
            return true;
        }

        public static async Task<bool> Journal()
        {
            Console.WriteLine("\n\n Enter your tracking Id.");
            string trackingId = Console.ReadLine();

            object content = new
            {
                Id = trackingId.Trim().ToLower()
            };
            
            string operation = "query";
            var result = await ExecuteService.Request<object, JObject>(content, operation, null);
            var operations = result.Property("operations");
            
            if (operations == null)
            {
                Console.WriteLine("There is no operations to the tracking Id indicated");
                return true;
            }
            
            Console.WriteLine("\n\nOperations: " + operations.Value.ToString());
            
            return true;
        }
    }
}
