using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicBookGalleryModel.ConsoleHelpers
{
    public static class Helper
    {
        public static string ReadInput(string prompt, bool forceToLowerCase = false)
        {
            Console.WriteLine();
            Console.Write(prompt);
            string input = Console.ReadLine();
            return forceToLowerCase ? input.ToLower() : input;
        }

        public static void ClearOutput()
        {
            Console.Clear();
        }

        public static void Output(string message)
        {
            Console.Write(message);
        }

        public static void Output(string format, params object[] args)
        {
            Console.Write(format, args);
        }

        public static void OutputLine(string message, bool outputBlankLine)
        {
            if (outputBlankLine)
            {
                Console.WriteLine();
            }
            Console.WriteLine(message);
        }

        public static void OutputLine(string format, params object[] args)
        {
            Console.WriteLine(format, args);
        }

        public static void OutputBlankLine()
        {
            Console.WriteLine();
        }
    }
}
