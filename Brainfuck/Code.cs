using System.Linq;

namespace Brainfuck
{
    /// <summary>
    /// Represents the Brainfuck code
    /// </summary>
    public class Code
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

        #region Constructor

        /// <summary>
        /// Creates a new instance of <see cref="Code"/>
        /// </summary>
        public Code(string codeText)
        {
            rawText = codeText;
            pureText = PurifyText(codeText);
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Returns the specified string but with no unwanted characters,
        /// that means only +, -, &lt;, &gt;, [ and ]
        /// </summary>
        /// <param name="text">The <see cref="string"/> to purify</param>
        /// <returns></returns>
        private string PurifyText(string text)
        {
            // TODO: It probably isn't efficient to create this array again and again
            char[] wanted = new char[] { '+', '-', '<', '>', '[', ']' };

            return new string(text.Where(c => wanted.Contains(c)).ToArray());
        }

        #endregion
    }
}
