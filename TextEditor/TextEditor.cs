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
        static int prevCol = 0;
        static string rowIndex = "";
        static bool shouldRefresh = false;
        static int windowWidth = Console.WindowWidth;

        static void Main(string[] args)
        {
            OpenFile(args);
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            DrawContent();

            while (true)
            {
                Scroll();
                RefreshScreen();
                HandleInput();
            }
        }

        private static void RefreshScreen()
        {
            if (shouldRefresh)
            {
                Console.Write($"{ESC}?25l");
                Console.SetCursorPosition(0, 0);
                ClearScreen();
                Console.SetCursorPosition(0, 0);
                DrawContent();
                Console.Write($"{ESC}?25h");
                shouldRefresh = false;
            }

            DrawCursor();
        }

        private static void Scroll()
        {
            if (row >= Console.WindowHeight + offsetRow)
            {
                offsetRow = row - Console.WindowHeight + 1;
                shouldRefresh = true;
            }

            else if (row < offsetRow)
            {
                offsetRow = row;
                shouldRefresh = true;
            }

            if (col >= Console.WindowWidth + offsetCol)
            {
                offsetCol = col - Console.WindowWidth + 1;
                shouldRefresh = true;
            }

            else if (col < offsetCol)
            {
                offsetCol = col;
                shouldRefresh = true;
            }

            if (windowWidth != Console.WindowWidth)
            {
                windowWidth = Console.WindowWidth;
                shouldRefresh = true;
            }
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
            rowIndex = (lastIndex + 1) + " ";
            int lenToDraw = text[lastIndex].Length - offsetCol;
            DrawRowIndex();

            if (lenToDraw >= Console.WindowWidth - rowIndex.Length)
            {
                lenToDraw = Console.WindowWidth - rowIndex.Length;
            }

            if (lenToDraw > 0)
            {
                Console.Write(text[lastIndex][offsetCol..(lenToDraw + offsetCol)]);
            }
        }

        private static void ClearScreen()
        {
            for (int i = 0; i < Console.WindowHeight - 1; i++)
            {
                Console.WriteLine(new string(' ', Console.WindowWidth));
            }

            Console.Write(new string(' ', Console.WindowWidth));
        }

        private static void DrawRow(int index)
        {
            rowIndex = (index + 1) + " ";
            int lenToDraw = text[index].Length - offsetCol;
            DrawRowIndex();

            if (lenToDraw <= 0)
            {
                Console.WriteLine();
            }

            if (lenToDraw >= Console.WindowWidth - rowIndex.Length)
            {
                lenToDraw = Console.WindowWidth - rowIndex.Length;

                if (index < 10)
                {
                    lenToDraw--;
                }
            }

            if (lenToDraw > 0)
            {
                Console.WriteLine(text[index][offsetCol..(lenToDraw + offsetCol)]);
            }
        }

        private static void DrawRowIndex()
        {
            if (Console.WindowHeight + offsetRow < 100)
            {
                Console.Write($"{ESC}32m{rowIndex,3}{ESC}0m");
            }

            else if (Console.WindowHeight + offsetRow < 1000)
            {
                Console.Write($"{ESC}32m{rowIndex,4}{ESC}0m");
                Console.CursorLeft = rowIndex.Length;
            }

            else if (Console.WindowHeight + offsetRow < 10000)
            {
                Console.Write($"{ESC}32m{rowIndex,5}{ESC}0m");
                Console.CursorLeft = rowIndex.Length;
            }
        }

        private static void DrawCursor() => Console.SetCursorPosition(Math.Min(col - offsetCol + rowIndex.Length, Console.WindowWidth - 1), row - offsetRow);

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
                col = text[row].Length > 0 ? text[row].IndexOf(text[row].First(c => !char.IsWhiteSpace(c))) : 0;
            }

            else if (ch == ConsoleKey.End)
            {
                if (text[row].Length > Console.WindowWidth)
                {
                    col = text[row].Length + rowIndex.Length;
                }

                else
                {
                    col = text[row].Length;
                }
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
                if (col > prevCol)
                {
                    prevCol = col;
                }

                if (col >= text[row - 1].Length)
                {
                    col = Math.Min(Console.WindowWidth + offsetCol - 1, text[row - 1].Length);
                }

                else if (text[row - 1].Length > text[row].Length)
                {
                    col = prevCol > text[row - 1].Length ? text[row - 1].Length : prevCol;
                }

                row--;
            }

            else if (ch == ConsoleKey.DownArrow && row < text.Length - 1)
            {
                if (col > prevCol)
                {
                    prevCol = col;
                }

                if (col >= text[row + 1].Length)
                {
                    col = Math.Min(Console.WindowWidth + offsetCol - 1, text[row + 1].Length);
                }

                else if (text[row + 1].Length > text[row].Length)
                {
                    col = prevCol > text[row + 1].Length ? text[row + 1].Length : prevCol;
                }

                row++;
            }

            else if (ch == ConsoleKey.RightArrow && (col < text[row].Length || (text[row].Length > Console.WindowWidth && col < text[row].Length + rowIndex.Length - 1)))
            {
                col++;
                prevCol = col;
            }

            else if (ch == ConsoleKey.LeftArrow && col > 0)
            {
                col--;
                prevCol = col;
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