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
            string code;

            // Request the path to the text file of the code
            Console.WriteLine("Specify the path to the code text file:");
            string path = Console.ReadLine();
            code = File.ReadAllText(path);

            Interpreter i = new Interpreter(code, getUserInput, display);
            i.Interpret();

            Console.ReadKey(true);
        }
    }
}
