using System;

namespace Brainfuck.Logic
{
    /// <summary>
    /// Interprets the code
    /// </summary>
    public class Interpreter
    {
        /// <summary>
        /// The Brainfuck code to interpret
        /// </summary>
        Code code;

        /// <summary>
        /// Represents the method that takes
        /// and returns input from the user
        /// </summary>
        Func<char> UserInput;

        /// <summary>
        /// Represents the method that the 
        /// <see cref="Interpreter"/> uses to display charaters
        /// </summary>
        Action<char> Display;

        #region Constructor

        /// <summary>
        /// Default constructor for the <see cref="Interpreter"/> class
        /// </summary>
        public Interpreter(string code, Func<char> UserInput, Action<char> Display)
        {
            this.code = new Code(code);

            if (UserInput == null) throw new ArgumentNullException();
            this.UserInput = UserInput;

            if (Display == null) throw new ArgumentNullException();
            this.Display = Display;
        }

        #endregion

        /// <summary>
        /// Interpret the code we have
        /// </summary>
        public void Interpret()
        {
            char current;

            // While we have a character to interpret
            while ((current = code.Next()) != 0)
            {
                switch (current)
                {
                    case '+':
                        code.Increment();
                        break;
                    case '-':
                        code.Decrement();
                        break;

                    case '<':
                        code.ShiftLeft();
                        break;
                    case '>':
                        code.ShiftRight();
                        break;

                    case ',':
                        code.SetValue(UserInput());
                        break;
                    case '.':
                        Display((char)code.GetValue());
                        break;
                }
            }
        }
    }
}
