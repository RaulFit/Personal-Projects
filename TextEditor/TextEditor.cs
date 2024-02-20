using System;
using System.Reflection;

namespace TextEditor
{
    class TextEditor
    {
        const string ESC = "\x1b[";
        static string[] text = { };
        static int row = 0;
        static int offsetRow = 0;
        static int col = 0;
        static int offsetCol = 0;

        static void Main(string[] args)
        {
            OpenFile(args);

            while (true)
            {
                Scroll();
                RefreshScreen();
                HandleInput();
            }
        }

        private static void RefreshScreen()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            DrawContent();
            DrawCursor();
        }

        private static void DrawContent()
        {
            int len = Math.Min(Console.WindowHeight - 1, text.Length - 1);

            for (int i = 0; i < len; i++)
            {
                int rowIndex = i + offsetRow;
                DrawRow(rowIndex);
            }

            int lastIndex = len + offsetRow;
            int lenToDraw = text[lastIndex].Length - offsetCol;

            if (lenToDraw > 0)
            {
                Console.Write(text[lastIndex][offsetCol..(lenToDraw + offsetCol)]);
            }
        }

        private static void DrawRow(int index)
        {
            int lenToDraw = text[index].Length - offsetCol;

            if (lenToDraw <= 0)
            {
                Console.WriteLine();
            }

            if (lenToDraw > Console.WindowWidth)
            {
                lenToDraw = Console.WindowWidth;
            }

            if (lenToDraw > 0)
            {
                Console.WriteLine(text[index][offsetCol..(lenToDraw + offsetCol)]);
            }
        }

        private static void DrawCursor() => Console.SetCursorPosition(col - offsetCol, row - offsetRow);

        private static void Scroll()
        {
            if (row >= Console.WindowHeight + offsetRow)
            {
                offsetRow = row - Console.WindowHeight + 1;
            }

            else if (row < offsetRow)
            {
                offsetRow = row;
            }

            if (col >= Console.WindowWidth + offsetCol)
            {
                offsetCol = col - Console.WindowWidth + 1;
            }

            else if (col < offsetCol)
            {
                offsetCol = col;
            }
        }

        private static void HandleInput()
        {
            var ch = Console.ReadKey(true);

            if (ch.Key == ConsoleKey.UpArrow && row > 0)
            {
                if (col > text[row - 1].Length)
                {
                    col = text[row - 1].Length;
                }

                row--;
            }

            else if (ch.Key == ConsoleKey.DownArrow && row < text.Length - 1)
            {
                if (col > text[row + 1].Length)
                {
                    col = text[row + 1].Length;
                }

                row++;
            }

            else if (ch.Key == ConsoleKey.RightArrow && col < text[row].Length)
            {
                col++;
            }

            else if (ch.Key == ConsoleKey.LeftArrow && col > 0)
            {
                col--;
            }
        }

        static void OpenFile(string[] args)
        {
            if (args.Length == 0 || !Path.Exists(args[0]))
            {
                Console.WriteLine("Enter the name of the text file in the format name.txt: ");
                string? fileName = Console.ReadLine();

                while (!Path.Exists(fileName))
                {
                    Console.WriteLine($"{ESC}31mThe specified file does not exist. Please enter a different file name: {ESC}0m");
                    fileName = Console.ReadLine();
                }

                text = File.ReadAllLines(Path.GetFullPath(fileName));
                return;
            }

            text = File.ReadAllLines(args[0]);
        }
    }
}