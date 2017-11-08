using System;
using System.Collections.Generic;
using System.Linq;

namespace Brainfuck.Logic
{
    /// <summary>
    /// Represents the Brainfuck code
    /// </summary>
    internal class Code
    {
        /// <summary>
        /// The raw code string
        /// </summary>
        string rawText;

        /// <summary>
        /// The code string without unwanted characters
        /// that can't be interpreted
        /// </summary>
        string pureText;

        /// <summary>
        /// The current character being interpreted
        /// </summary>
        int charPointer;

        /// <summary>
        /// The stack that keeps track of the [] loop starts
        /// </summary>
        Stack<int> loops;

        /// <summary>
        /// The pointers and their respective values
        /// </summary>
        Dictionary<int, uint> pointers;

        /// <summary>
        /// The current pointer
        /// </summary>
        int currentPointer;

        #region Constructor

        /// <summary>
        /// Creates a new instance of <see cref="Code"/>
        /// </summary>
        public Code(string codeText)
        {
            rawText = codeText;
            pureText = PurifyText(codeText);

            charPointer = -1;
            loops = new Stack<int>();
            pointers = new Dictionary<int, uint>();
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Returns the specified string but with no unwanted characters,
        /// that means only +, -, &lt;, &gt;, ,, ., [ and ]
        /// </summary>
        /// <param name="text">The <see cref="string"/> to purify</param>
        /// <returns></returns>
        private string PurifyText(string text)
        {
            // TODO: It probably isn't efficient to create this array again and again
            char[] wanted = new char[] { '+', '-', '<', '>', '[', ']', ',', '.' };

            return new string(text.Where(c => wanted.Contains(c)).ToArray());
        }

        #endregion

        /// <summary>
        /// Get the next yet uninterpreted character
        /// </summary>
        /// <returns></returns>
        public char Next()
        {
            charPointer++;

            if (charPointer >= pureText.Length)
                return (char)0;

            char next = pureText[charPointer];

            // What if next is a [ or ]?
            if (next == '[')
            {
                // Begining of a loop
                
                if (GetValue() != 0)
                {
                    loops.Push(charPointer);
                    return Next();
                }

                // We want to skip to the end of the loop because the value is zero
                int inLoopPointer = 0;
                int loopLevel = 1;

                while (loopLevel != 0)
                {
                    switch (pureText[currentPointer + ++inLoopPointer])
                    {
                        case '[':
                            loopLevel++;
                            break;
                        case ']':
                            loopLevel--;
                            break;

                        default:
                            break;
                    }

                }
                charPointer += inLoopPointer;

                return Next();

            }
            else if (next == ']')
            {
                // End of a loop (or a jump to the begining of it)
                if (!pointers.ContainsKey(currentPointer) || pointers[currentPointer] == 0)
                {
                    // Our current pointer value is zero

                    // Remove the loop
                    loops.Pop();

                    // Return the next character
                    return Next();
                }

                // Go back to the begining of the loop, but DON'T remove it
                charPointer = loops.Peek();
                return Next();
            }

            return next;
        }

        /// <summary>
        /// Sets the value of the <see cref="currentPointer"/> to the specified value
        /// </summary>
        /// <param name="value"></param>
        public void SetValue(char value)
        {
            pointers[currentPointer] = value;
        }

        /// <summary>
        /// Get the value of the <see cref="currentPointer"/>
        /// </summary>
        /// <returns></returns>
        public uint GetValue()
        {
            if (!pointers.ContainsKey(currentPointer))
                return 0;
            return pointers[currentPointer];
        }

        /// <summary>
        /// Adds one to the value at <see cref="currentPointer"/>,
        /// corresponds to '+'
        /// </summary>
        public void Increment()
        {
            if (!pointers.ContainsKey(currentPointer))
            {
                pointers[currentPointer] = 1;
                return;
            }
            pointers[currentPointer]++;
        }

        /// <summary>
        /// Subtracts one from the value at <see cref="currentPointer"/>,
        /// corresponds to '-'
        /// </summary>
        public void Decrement()
        {
            if (!pointers.ContainsKey(currentPointer) || pointers[currentPointer] == 0)
                throw new InvalidOperationException("Pointers cannot hold values less than zero.");

            pointers[currentPointer]--;
        }

        /// <summary>
        /// Shifts the <see cref="currentPointer"/> to the left
        /// (decreases by one), corresponds to '&lt;'
        /// </summary>
        public void ShiftLeft()
        {
            currentPointer--;
        }

        /// <summary>
        /// Shifts the <see cref="currentPointer"/> to the right
        /// (increases by one), corresponds to '&gt;'
        /// </summary>
        public void ShiftRight()
        {
            currentPointer++;
        }

        /// <summary>
        /// The string representation of the code object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return pureText;
        }
    }
}
