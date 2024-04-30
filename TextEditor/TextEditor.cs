﻿namespace TextEditor
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
        static bool lineNumbers = false;
        static bool relativeLines = false;

        static void Main(string[] args)
        {
            OpenFile(args);
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            DrawFinder(args[0]);
            Console.SetCursorPosition(1, Console.WindowHeight - 2);

            while (true)
            {
                Scroll();
                RefreshScreen();
                var ch = Console.ReadKey(true);
                HandleInput(ch);
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

        private static void DrawFinder(string name)
        {
            Console.SetCursorPosition(0, 0);
            PrintVerticalBorders(Console.WindowHeight - 5);
            Console.SetCursorPosition(0, 0);
            PrintHorizontalBorder(Console.WindowWidth / 2 - 4);
            Console.Write("Results");
            PrintHorizontalBorder(Console.WindowWidth / 2 - 3);
            Console.SetCursorPosition(0, Console.WindowHeight - 5);
            PrintHorizontalBorder(Console.WindowWidth);
            Console.SetCursorPosition(0, Console.WindowHeight - 3);
            PrintHorizontalBorder(Console.WindowWidth / 2 - 5);
            Console.Write("Find Files");
            PrintHorizontalBorder(Console.WindowWidth / 2 - 5);
            Console.SetCursorPosition(0, Console.CursorTop + 1);
            PrintVerticalBorders(1);
            PrintHorizontalBorder(Console.WindowWidth);
            Console.SetCursorPosition(1, Console.WindowHeight - 6);
            string[] files = GetAllFiles(name);
            PrintFiles(files);
            SearchFile();
        }

        private static void SearchFile()
        {
            Console.SetCursorPosition(1, Console.WindowHeight - 2);
            string word = "";
            while (true)
            {
                var ch = Console.ReadKey(true);
                word += ch.KeyChar.ToString();
                Console.SetCursorPosition(1, 3);
                Console.Write(word);
                Console.SetCursorPosition(1, Console.WindowHeight - 2);
            }
        }

        private static void PrintFiles(string[] files)
        {
            for (int i = 0; i < files.Length; i++)
            {
                Console.Write($"{files[i].Substring(files[i].LastIndexOf('\\') + 1)}");
                Console.CursorTop--;
                Console.CursorLeft = 1;
            }
        }

        private static string[] GetAllFiles(string name)
        {
            string path = Path.GetFullPath(name);
            return Directory.GetFiles(path.Substring(0, path.LastIndexOf('\\')));
        }

        private static void PrintVerticalBorders(int length)
        {
            for (int i = 0; i < length; i++)
            {
                PrintBorder(true);
                Console.CursorLeft = Console.WindowWidth - 1;
                PrintBorder(true);
                Console.CursorTop++;
                Console.CursorLeft = 0;
            }
        }

        private static void PrintHorizontalBorder(int length)
        {
            for (int i = 0; i < length; i++)
            {
                PrintBorder(false);
            }
        }

        private static void PrintBorder(bool vertical)
        {
            Console.Write("\x1b" + "(0");
            Console.Write(ESC + "31m");
            if (vertical)
            {
                Console.Write("x");
            }
            else
            {
                Console.Write("q");
            }
            Console.Write(ESC + "0m");
            Console.Write("\x1b" + "(B");
        }

        private static void DrawContent()
        {
            int len = Math.Min(Console.WindowHeight, text.Length);

            for (int i = 0; i < len; i++)
            {
                int rowIndex = i + offsetRow;
                DrawRow(rowIndex);
            }

            if (relativeLines)
            {
                DrawIndexes();
            }
        }

        private static void DrawRow(int index)
        {
            rowIndex = (index + 1) + " ";
            DrawRowIndex();

            int lenToDraw = text[index].Length - offsetCol;

            if (lenToDraw > Console.WindowWidth - rowIndex.Length && lineNumbers)
            {
                lenToDraw = Console.WindowWidth - rowIndex.Length;

                if (relativeLines)
                {
                    lenToDraw = Console.WindowWidth - row.ToString().Length - 2;
                }
            }

            else if (lenToDraw >= Console.WindowWidth)
            {
                lenToDraw = Console.WindowWidth;
            }

            if (lenToDraw > 0)
            {
                Console.Write(text[index][offsetCol..(lenToDraw + offsetCol)]);
            }

            if (index < Console.WindowHeight + offsetRow - 1)
            {
                Console.CursorTop++;
                Console.CursorLeft = 0;
            }
        }

        private static void DrawIndexes()
        {
            Console.Write($"{ESC}?25l");
            Console.SetCursorPosition(0, 0);
            int num = row - offsetRow;
            int i;
            for (i = offsetRow; i < Console.WindowHeight + offsetRow - 1; i++)
            {
                Console.Write(new string(' ', rowIndex.Length));
                Console.CursorLeft = 0;
                DrawIndex(i, ref num);
                Console.CursorTop++;
                Console.CursorLeft = 0;
            }

            DrawIndex(i, ref num);
            Console.CursorLeft = 0;
            Console.Write($"{ESC}0m");
            Console.Write($"{ESC}?25h");
        }

        private static void DrawIndex(int index, ref int num)
        {
            Console.Write($"{ESC}32m");
            num = index > row ? num + 1 : num;
            string idx = row == index ? row + " " : num + " ";

            if (row == index)
            {
                Console.Write($"{ESC}31m");
            }

            if (row < 100)
            {
                Console.Write($"{idx,3}");
            }

            else if (row >= 100 && row < 1000)
            {
                Console.Write($"{idx,4}");
                Console.CursorLeft = idx.ToString().Length;
            }

            num = index < row ? num - 1 : num;
        }

        private static void ClearScreen()
        {
            for (int i = 0; i < Console.WindowHeight - 1; i++)
            {
                Console.WriteLine(new string(' ', Console.WindowWidth));
            }

            Console.Write(new string(' ', Console.WindowWidth));
        }

        private static void DrawRowIndex()
        {
            if (lineNumbers)
            {
                int lastNumber = Console.WindowHeight + offsetRow;
                if (lastNumber < 100)
                {
                    Console.Write($"{ESC}32m{rowIndex,3}{ESC}0m");
                }

                else if (lastNumber >= 100 && lastNumber < 1000)
                {
                    Console.Write($"{ESC}32m{rowIndex,4}{ESC}0m");
                    Console.CursorLeft = rowIndex.Length;
                }
            }
        }

        private static void DrawCursor()
        {
            if (lineNumbers)
            {
                Console.SetCursorPosition(Math.Min(col - offsetCol + rowIndex.Length, Console.WindowWidth - 1), Math.Max(row - offsetRow, 0));
                return;
            }

            Console.SetCursorPosition(Math.Min(col - offsetCol, Console.WindowWidth - 1), Math.Max(row - offsetRow, 0));
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

        private static void HandleInput(ConsoleKeyInfo ch)
        {
            if (char.IsDigit(ch.KeyChar) && ch.Key != ConsoleKey.D0)
            {
                HandleSimpleMovement(ch);
                return;
            }

            MoveTo(ch.Key, 1);
            HandleSpecialKeys(ch);
            HandleArrows(ch.Key);
            HandleKeys(ch.Key);
            MoveWord(ch);
            MoveChar(ch, 1);
        }

        private static void HandleSimpleMovement(ConsoleKeyInfo ch)
        {
            string num = ch.KeyChar.ToString();
            while (true)
            {
                ch = Console.ReadKey(true);
                if (char.IsDigit(ch.KeyChar))
                {
                    num += ch.KeyChar.ToString();
                    continue;
                }

                if (int.TryParse(num, out int number))
                {
                    MoveTo(ch.Key, number);
                    MoveWords(ch, number);
                    MoveChar(ch, number);
                }

                break;
            }
        }

        private static void FindCharLowerCase(ConsoleKeyInfo ch)
        {
            int index = text[row].IndexOf(ch.KeyChar.ToString(), Math.Min(col + 1, text[row].Length - 1));
            col = index != -1 ? index : col;
        }

        private static void FindCharUpperCase(ConsoleKeyInfo ch)
        {
            int index = text[row].LastIndexOf(ch.KeyChar.ToString(), Math.Max(col - 1, 0));
            col = index != -1 ? index : col;
        }

        private static void MoveChar(ConsoleKeyInfo ch, int num)
        {
            if (ch.KeyChar.ToString() == "f")
            {
                var chToFind = Console.ReadKey(true);
                for (int i = 0; i < num; i++)
                {
                    FindCharLowerCase(chToFind);
                }
            }

            if (ch.KeyChar.ToString() == "F")
            {
                var chToFind = Console.ReadKey(true);
                for (int i = 0; i < num; i++)
                {
                    FindCharUpperCase(chToFind);
                }
            }
        }

        private static void MoveWords(ConsoleKeyInfo ch, int num)
        {
            for (int i = 0; i < num; i++)
            {
                MoveWord(ch);
            }
        }

        private static void MoveWord(ConsoleKeyInfo ch)
        {
            if (ch.KeyChar.ToString() == "w")
            {
                MoveForwardLowerCase();
                while ((col == text[row].Length || char.IsWhiteSpace(text[row][col])) && row < text.Length - 1)
                {
                    row++;
                    col = 0;
                    MoveForwardLowerCase();
                }
            }

            if (ch.KeyChar.ToString() == "b")
            {
                MoveBackwards();
                while ((col == text[row].Length || char.IsWhiteSpace(text[row][col])) && row > 0)
                {
                    row--;
                    col = text[row].Length;
                    MoveBackwards();
                }
            }

            if (ch.KeyChar.ToString() == "W")
            {
                MoveForwardUpperCase();
                while ((col == text[row].Length || char.IsWhiteSpace(text[row][col])) && row < text.Length - 1)
                {
                    row++;
                    col = 0;
                    MoveForwardUpperCase();
                }
            }
        }

        private static void MoveForwardLowerCase()
        {
            if (col < text[row].Length && char.IsPunctuation(text[row][col]))
            {
                while (col < text[row].Length && char.IsPunctuation(text[row][col]))
                {
                    col++;
                }
            }

            else if (col < text[row].Length && char.IsLetter(text[row][col]))
            {
                while (col < text[row].Length && char.IsLetter(text[row][col]))
                {
                    col++;
                }
            }

            else if (col < text[row].Length)
            {
                while (col < text[row].Length && !char.IsLetter(text[row][col]) && !char.IsPunctuation(text[row][col]))
                {
                    col++;
                }
            }

            while (col < text[row].Length && char.IsWhiteSpace(text[row][col]))
            {
                col++;
            }
        }

        private static void MoveForwardUpperCase()
        {
            if (col < text[row].Length && char.IsLetter(text[row][col]))
            {
                while (col < text[row].Length && char.IsLetter(text[row][col]))
                {
                    col++;
                }
            }

            else if (col < text[row].Length)
            {
                while (col < text[row].Length && !char.IsLetter(text[row][col]))
                {
                    col++;
                }
            }

            while (col < text[row].Length && (char.IsWhiteSpace(text[row][col]) || char.IsPunctuation(text[row][col])))
            {
                col++;
            }
        }

        private static void MoveBackwards()
        {
            col--;
            if (col >= text[row].Length)
            {
                col = text[row].Length - 1;
            }

            while (col > 0 && char.IsWhiteSpace(text[row][col]))
            {
                col--;
            }

            while (col > 0 && !char.IsWhiteSpace(text[row][col]))
            {
                col--;
            }
            col++;
        }

        private static void HandleSpecialKeys(ConsoleKeyInfo ch)
        {
            if (ch.KeyChar.ToString() == "$")
            {
                if (lineNumbers)
                {
                    col = text[row].Length > Console.WindowWidth ? text[row].Length + rowIndex.Length : text[row].Length;
                }

                else
                {
                    col = text[row].Length;
                }
            }

            else if (ch.KeyChar.ToString() == "0")
            {
                col = 0;
            }

            else if (ch.KeyChar.ToString() == "^")
            {
                HandleKeys(ConsoleKey.Home);
            }
        }

        private static void MoveTo(ConsoleKey ch, int number)
        {
            if (ch == ConsoleKey.J)
            {
                Move(ConsoleKey.DownArrow, number);
            }

            else if (ch == ConsoleKey.K)
            {
                Move(ConsoleKey.UpArrow, number);
            }

            else if (ch == ConsoleKey.H)
            {
                Move(ConsoleKey.LeftArrow, number);
            }

            else if (ch == ConsoleKey.L)
            {
                Move(ConsoleKey.RightArrow, number);
            }
        }

        private static void Move(ConsoleKey ch, int num)
        {
            for (int i = 0; i < num; i++)
            {
                HandleArrows(ch);
            }
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
                    col = lineNumbers ? text[row].Length + rowIndex.Length : text[row].Length;
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

                if (relativeLines)
                {
                    DrawIndexes();
                }
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

                if (relativeLines)
                {
                    DrawIndexes();
                }
            }

            else if (ch == ConsoleKey.RightArrow && (col < text[row].Length || (text[row].Length > Console.WindowWidth && col < text[row].Length + rowIndex.Length - 1)))
            {
                col++;
                if (col > text[row].Length && !lineNumbers)
                {
                    col = text[row].Length;
                }
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

                ShowLineNumbers();

                text = File.ReadAllLines(Path.GetFullPath(fileName));
                return;
            }

            ShowLineNumbers();
            text = File.ReadAllLines(args[0]);
        }

        static void ShowLineNumbers()
        {
            Console.Write("Show line numbers? (Press Y for yes): ");

            var ch = Console.ReadKey();

            if (ch.KeyChar.ToString() == "Y")
            {
                lineNumbers = true;

                Console.WriteLine();
                Console.Write("Show line numbers relative to the current line? (Press Y for yes):");

                ch = Console.ReadKey();

                if (ch.KeyChar.ToString() == "Y")
                {
                    relativeLines = true;
                }
            }
        }
    }
}