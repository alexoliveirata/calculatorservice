using System;
using System.Threading.Tasks;

namespace CalculatorService.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            StartMessage();
            MainAsync().Wait();
        }

        static async Task MainAsync()
        {
            bool on = true;
            while (on == true)
            {
                on = await Calculator();
            }
        }

        private static void StartMessage()
        {
            Console.WriteLine(
                "\n\n === CALCULATOR =============================" +
                "\n\n Addition => '+' || Substraction => '-' || Multiplication => '*' || Division => '/' || Square Root => 's' " +
                "\n Or type 'exit' to close the application");
        }

        private static async Task<bool> Calculator()
        {
            Console.WriteLine("\n Please select an operation.");

            string Operator = Console.ReadLine();

            if (string.IsNullOrEmpty(Operator))
            {
                Console.WriteLine("Please select an operation.");
                return true;
            }
            else if (Operator.ToLower() == "exit")
            {
                return false;
            }
            else
            {
                switch (Operator)
                {
                    case "+":
                        {
                            await Operations.Sum();
                            break;
                        }
                    case "-":
                        {
                            await Operations.Sub();
                            break;
                        }
                    case "*":
                        {
                            await Operations.Mult();
                            break;
                        }
                    case "/":
                        {
                            await Operations.Div();
                            break;
                        }
                    case "s":
                        {
                            await Operations.Sqrt();
                            break;
                        }
                    case "j":
                        {
                            await Operations.Journal();
                            break;
                        }
                    default:
                        break;
                }

                Console.WriteLine("\n\n  Press Enter to continue with the calculator" +
                                  "\n\n  or " +
                                  "\n\n  Press j to view the journal records");

                string action = Console.ReadLine();
                
                if (action.Trim() == "j")
                {
                    await Operations.Journal();
                }

                return true;
            }
        }
    }
}
