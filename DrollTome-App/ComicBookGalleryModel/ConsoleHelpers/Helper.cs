using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicBookGalleryModel.ConsoleHelpers
{
    public static class Helper
    {
        public static string ReadInput(string prompt, bool forceToLowerCase)
        {
            Console.WriteLine();
            Console.WriteLine(prompt);
            string input = Console.ReadLine();
            return forceToLowerCase ? input.ToLower() : input;
        }

        public static void ClearOutput()
        {
            Console.Clear();
        }

        public static void Output(string message)
        {
            Console.WriteLine(message);
        }

        public static void Output(string format, params object[] args)
        {
            Console.WriteLine(format, args);
            Console.WriteLine();
        }

        public static void OutputLine(string message, bool outputBlankLine)
        {
            if (outputBlankLine)
            {
                Console.WriteLine(message);
            }
            Console.WriteLine();
        }

        public static void OutputLine(string format, params object[] args)
        {
            Console.WriteLine(format, args);
            Console.WriteLine();
        }

        public static void OutputBlankLine()
        {
            Console.WriteLine();
        }
    }
}
