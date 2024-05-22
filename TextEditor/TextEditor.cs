using System.Configuration;

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
        static bool lineNumbers = false;
        static bool relativeLines = false;
        static string match = "";
        static string[] files = { };
        static string[] filteredFiles = { };
        static int startIndex = 0;
        static int endIndex = 0;
        static int currentIndex = 0;

        static void Main(string[] args)
        {
            if (args.Length > 0 && Path.Exists(args[0]))
            {
                text = File.ReadAllLines(Path.GetFullPath(args[0]));
                lineNumbers = Convert.ToBoolean(ConfigurationManager.AppSettings.Get("lineNumbers"));
                relativeLines = Convert.ToBoolean(ConfigurationManager.AppSettings.Get("relativeLines"));
                RunNavigator();
            }

            OpenFinder();
        }

        private static void OpenFinder()
        {
            Console.Clear();
            DrawFinder();
            while (true)
            {
                var ch = Console.ReadKey();
                SearchFile(ch);

                if (ch.Key == ConsoleKey.UpArrow)
                {
                    Console.SetCursorPosition(1, Console.WindowHeight - 6);
                    while (ch.Key != ConsoleKey.Enter)
                    {
                        ch = Console.ReadKey(true);
                        if (ch.Key == ConsoleKey.UpArrow || ch.Key == ConsoleKey.DownArrow)
                        {
                            SelectFile(ch, filteredFiles);
                        }

                        else if (ch.Key != ConsoleKey.Enter)
                        {
                            Console.SetCursorPosition(match.Length + 1, Console.WindowHeight - 2);
                            Console.Write(ch.KeyChar.ToString());
                            currentIndex = 0;
                            SearchFile(ch);
                            Console.Write($"{ESC}?25l");
                            RefreshFiles();
                            ColorMatchingLetters();
                            Console.Write($"{ESC}?25h");
                            Console.SetCursorPosition(1, Console.WindowHeight - 6);
                        }
                    }

                    text = File.ReadAllLines(Path.GetFullPath(filteredFiles.ElementAt(currentIndex)));
                    lineNumbers = Convert.ToBoolean(ConfigurationManager.AppSettings.Get("lineNumbers"));
                    relativeLines = Convert.ToBoolean(ConfigurationManager.AppSettings.Get("relativeLines"));
                    RunNavigator();
                }

                Console.Write($"{ESC}?25l");
                RefreshFiles();
                ColorMatchingLetters();
                Console.Write($"{ESC}?25h");
            }
        }

        private static void RunNavigator()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            DrawContent();
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

        private static void DrawFinder()
        {
            Console.SetCursorPosition(0, 0);
            PrintVerticalBorders(Console.WindowHeight - 4);
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
            files = Directory.GetFiles(Environment.CurrentDirectory, "*.*", SearchOption.AllDirectories).Select(Path.GetFullPath).ToArray();
            filteredFiles = new string[files.Length];
            files.CopyTo(filteredFiles, 0);
            endIndex = Math.Min(files.Length - 1, Console.WindowHeight - 6);
            PrintFiles(filteredFiles.Select(f => Path.GetFileName(f)).ToArray());
            DrawCorners();
            Console.SetCursorPosition(1, Console.WindowHeight - 2);
        }

        private static void DrawCorners()
        {
            Console.Write("\x1b" + "(0");
            Console.Write(ESC + "31m");
            Console.SetCursorPosition(0, 0);
            Console.Write("l");
            Console.SetCursorPosition(Console.WindowWidth - 1, 0);
            Console.Write("k");
            Console.SetCursorPosition(0, Console.WindowHeight - 5);
            Console.Write("m");
            Console.SetCursorPosition(Console.WindowWidth - 1, Console.WindowHeight - 5);
            Console.Write("j");
            Console.SetCursorPosition(0, Console.WindowHeight - 3);
            Console.Write("l");
            Console.SetCursorPosition(Console.WindowWidth - 1, Console.WindowHeight - 3);
            Console.Write("k");
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            Console.Write("m");
            Console.SetCursorPosition(Console.WindowWidth - 1, Console.WindowHeight - 1);
            Console.Write("j");
            Console.Write(ESC + "0m");
            Console.Write("\x1b" + "(B");
        }

        private static void PrintFiles(string[] files)
        {
            for (int i = startIndex; i < endIndex; i++)
            {
                Console.Write(files[i]);
                Console.SetCursorPosition(1, Console.CursorTop - 1);
            }

            int filesLength = new string(filteredFiles.Length.ToString()).Length;
            Console.SetCursorPosition(Console.WindowWidth - 10, Console.WindowHeight - 2);
            Console.Write(new string(' ', 9));
            Console.SetCursorPosition(Console.WindowWidth - 6 - filesLength, Console.WindowHeight - 2);
            Console.Write($"{filteredFiles.Length} / {TextEditor.files.Length}");
            Console.SetCursorPosition(match.Length + 1, Console.WindowHeight - 2);
        }

        private static void ColorMatchingLetters()
        {
            Console.SetCursorPosition(1, Console.WindowHeight - 6);
            Console.Write($"{ESC}32m");
            for (int i = startIndex; i < endIndex && Console.CursorTop > 0; i++)
            {
                ColorPattern(filteredFiles[i], 0);
                Console.SetCursorPosition(1, Console.CursorTop - 1);
            }
            Console.SetCursorPosition(match.Length + 1, Console.WindowHeight - 2);
            Console.Write($"{ESC}0m");
        }

        private static void ColorPattern(string file, int start)
        {
            string fileName = Path.GetFileName(file);
            string lowerFileName = fileName.ToLower();
            string lowerMatch = match.ToLower();
            if (lowerFileName.Contains(lowerMatch))
            {
                Console.CursorLeft = lowerFileName.IndexOf(lowerMatch) + 1;
                Console.Write(new string(' ', lowerMatch.Length));
                Console.CursorLeft = lowerFileName.IndexOf(lowerMatch) + 1;
                Console.Write(fileName.Substring(Console.CursorLeft - 1, match.Length));
                return;
            }

            for (int j = 0; j < fileName.Length && start < lowerMatch.Length; j++)
            {
                if (lowerMatch[start] == lowerFileName[j])
                {
                    Console.CursorLeft = lowerFileName.IndexOf(lowerMatch[start], j) + 1;
                    start++;
                    Console.Write(fileName[Console.CursorLeft - 1]);
                }
            }
        }

        private static void SelectFile(ConsoleKeyInfo ch, string[] files)
        {
            int top = Console.CursorTop;
            if (ch.Key == ConsoleKey.UpArrow)
            {
                if (top == 1 && endIndex < files.Length - 1 && currentIndex < files.Length - 1)
                {
                    startIndex++;
                    endIndex++;
                    RefreshFiles();
                    ColorMatchingLetters();
                }

                if (currentIndex < filteredFiles.Length - 1)
                {
                    currentIndex++;
                    Console.SetCursorPosition(1, Math.Max(1, top - 1));
                }
            }

            if (ch.Key == ConsoleKey.DownArrow)
            {
                if (top == Console.WindowHeight - 6 && startIndex > 0)
                {

                    startIndex--;
                    endIndex--;
                    RefreshFiles();
                    ColorMatchingLetters();
                }

                if (currentIndex > 0)
                {
                    currentIndex--;
                }

                Console.SetCursorPosition(1, Math.Min(Console.WindowHeight - 6, top + 1));
            }
        }

        private static void SearchFile(ConsoleKeyInfo ch)
        {
            if (ch.Key == ConsoleKey.Backspace)
            {
                if (Console.CursorLeft == 0)
                {
                    Console.CursorLeft++;
                }
                Console.Write(' ');
                match = match.Length > 0 ? match.Remove(match.Length - 1, 1) : match;
                filteredFiles = FilterFiles();
                currentIndex = 0;
                startIndex = 0;
                endIndex = Math.Min(Console.WindowHeight - 6, filteredFiles.Length);
                return;
            }

            if (ch.Key == ConsoleKey.UpArrow)
            {
                return;
            }

            match += ch.KeyChar.ToString();
            filteredFiles = FilterFiles();
            currentIndex = 0;
            startIndex = 0;
            endIndex = Math.Min(Console.WindowHeight - 6, filteredFiles.Length);
        }

        private static void RefreshFiles()
        {
            Console.SetCursorPosition(1, Console.WindowHeight - 6);
            for (int i = 0; i < Console.WindowHeight - 6; i++)
            {
                Console.Write(new string(' ', Console.WindowWidth - 2));
                Console.SetCursorPosition(1, Console.CursorTop - 1);
            }

            Console.SetCursorPosition(1, Console.WindowHeight - 6);
            PrintFiles(filteredFiles.Select(f => Path.GetFileName(f)).ToArray());
        }

        private static string[] FilterFiles()
        {
            string[] caseInsensitiveFiles = files.Where(file => FuzzySearch(match, Path.GetFileName(file))).ToArray();

            if (caseInsensitiveFiles.Length > 0)
            {
                return caseInsensitiveFiles;
            }

            return files.Where(file => FuzzySearch(match.ToLower(), Path.GetFileName(file).ToLower())).ToArray();
        }

        private static bool FuzzySearch(string pat, string text)
        {
            string pattern = "";
            int i = 0;
            while (i < pat.Length)
            {
                for (int j = 0; j < text.Length && i < pat.Length; j++)
                {
                    if (text[j] == pat[i])
                    {
                        pattern += text[j];
                        i++;
                    }
                }

                break;
            }

            return pat == pattern;
        }

        private static void PrintVerticalBorders(int length)
        {
            for (int i = 0; i < length; i++)
            {
                PrintBorder(true);
                Console.CursorLeft = Console.WindowWidth - 1;
                PrintBorder(true);
                Console.SetCursorPosition(0, Console.CursorTop + 1);
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

            else if (ch == ConsoleKey.P)
            {
                currentIndex = 0;
                row = col = 0;
                offsetRow = offsetCol = 0;
                startIndex = 0;
                endIndex = Math.Min(Console.WindowHeight - 6, files.Length - 1);
                match = "";
                filteredFiles = new string[] { };
                OpenFinder();
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
    }
}