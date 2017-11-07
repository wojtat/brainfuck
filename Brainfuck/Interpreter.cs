﻿namespace Brainfuck.Logic
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

        #region Constructor

        /// <summary>
        /// Default constructor for the <see cref="Interpreter"/> class
        /// </summary>
        public Interpreter(string code)
        {
            this.code = new Code(code);
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
                        break;
                    case '.':
                        break;
                }
            }
        }
    }
}
