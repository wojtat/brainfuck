using System;
using System.IO;
using Brainfuck.Logic;

namespace Brainfuck.UI
{
    class Program
    {
        /// <summary>
        /// The entry point of the application
        /// </summary>
        /// <param name="args">Command prompt arguments</param>
        static void Main(string[] args)
        {
            // The get user input method
            Func<char> getUserInput = () =>
            {
                return (char)Console.Read();
            };

            // The display char method
            Action<char> display = c =>
            {
                Console.Write(c);
            };

            // The actual code string to be interpreted
            string code = null;

            if (args == null || args.Length < 1)
            {
                // Request the path to the text file of the code
                Console.WriteLine("Specify the path to the code text file:");
                string path = Console.ReadLine();
                
                while (!TryReadText(path, ref code))
                {
                    Console.WriteLine("Invalid path! Specify the path to the code text file:");
                    path = Console.ReadLine();
                }
            }
            else
            {
                // Use the path provided as a command line argument
                bool success = TryReadText(args[0], ref code);

                // Terminate the application if the file isn't valid
                if (!success) return;
            }

            Console.Clear();

            // Interpret the code
            Interpreter i = new Interpreter(code, getUserInput, display);
            i.Interpret();
            
            // Wait for the user to terminate
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
        }
        
        /// <summary>
        /// Determine whether the path leads to a valid .txt file,
        /// if so, read from it
        /// </summary>
        static bool TryReadText(string path, ref string text)
        {
            if (File.Exists(path) && path.EndsWith(".txt"))
            {
                text = File.ReadAllText(path);
                return true;
            }
            return false;
        }
    }
}
