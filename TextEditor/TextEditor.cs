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
        static string rowIndex = "";

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
            Console.Write($"{ESC}32m");
            rowIndex = (lastIndex + 1) + " ";
            Console.Write(rowIndex);
            Console.Write($"{ESC}0m");

            if (lenToDraw > 0)
            {
                Console.Write(text[lastIndex][offsetCol..(lenToDraw + offsetCol)]);
            }
        }

        private static void DrawRow(int index)
        {
            int lenToDraw = text[index].Length - offsetCol;
            Console.Write($"{ESC}32m");
            rowIndex = (index + 1) + " ";
            Console.Write(rowIndex);
            Console.Write($"{ESC}0m");

            if (lenToDraw <= 0)
            {
                Console.WriteLine();
            }

            if (lenToDraw > Console.WindowWidth)
            {
                lenToDraw = Console.WindowWidth - rowIndex.Length;
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
            HandleArrows(ch.Key);
            HandleKeys(ch.Key);
        }

        private static void HandleKeys(ConsoleKey ch)
        {
            if (ch == ConsoleKey.Home)
            {
                if (offsetCol == 0)
                {
                    col = text[row].Length > 0 ? text[row].IndexOf(text[row].First(c => !char.IsWhiteSpace(c))) + rowIndex.Length - 1: rowIndex.Length - 1;
                }
                else
                {
                    col = text[row].Length > 0 ? text[row].IndexOf(text[row].First(c => !char.IsWhiteSpace(c))) : 0;
                }
                
            }

            else if (ch == ConsoleKey.End)
            {
                col = text[row].Length + rowIndex.Length - 1;
            }

            else if (ch == ConsoleKey.PageDown)
            {
                row = Console.WindowHeight + offsetRow - 1;
                int end = row + Console.WindowHeight;
                for (int i = row; i < end && i < text.Length - 1; i++)
                {
                    HandleArrows(ConsoleKey.DownArrow);
                }
            }

            else if (ch == ConsoleKey.PageUp)
            {
                row = offsetRow;
                int end = row - Console.WindowHeight;
                for (int i = row; i > end && i > 0; i--)
                {
                    HandleArrows(ConsoleKey.UpArrow);
                }
            }
        }

        private static void HandleArrows(ConsoleKey ch)
        {
            if (ch == ConsoleKey.UpArrow && row > 0)
            {
                if (col > text[row - 1].Length || col == text[row].Length + rowIndex.Length - 1)
                {
                    col = Math.Min(Console.WindowWidth + offsetCol - 1, text[row - 1].Length+ rowIndex.Length - 1);
                }

                row--;
            }

            else if (ch == ConsoleKey.DownArrow && row < text.Length - 1)
            {
                if (col > text[row + 1].Length || col == text[row].Length + rowIndex.Length - 1)
                {
                    col = Math.Min(Console.WindowWidth + offsetCol - 1, text[row + 1].Length + rowIndex.Length -1);
                }

                row++;
            }

            else if (ch == ConsoleKey.RightArrow && col < text[row].Length + row.ToString().Length + 1)
            {
                col++;
            }

            else if (ch == ConsoleKey.LeftArrow && col > 0)
            {
                if (offsetCol == 0 && col < rowIndex.Length)
                {
                    col = rowIndex.Length;
                }

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