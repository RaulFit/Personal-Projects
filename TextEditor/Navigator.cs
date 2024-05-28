namespace TextEditor
{
    class Navigator
    {
        public static string[] text = { };
        public static int row = 0;
        public static int offsetRow = 0;
        public static int col = 0;
        public static int offsetCol = 0;
        public static int prevCol = 0;
        public static bool insertMode = false;

        public static void RunNavigator()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Drawer.DrawContent();
            while (true)
            {
                Drawer.Scroll();
                Drawer.RefreshText();
                Drawer.RefreshScreen();
                ConsoleKeyInfo ch;
                ch = insertMode ? Console.ReadKey() : Console.ReadKey(true);
                if (Drawer.relativeLines && (ch.Key == ConsoleKey.UpArrow || ch.Key == ConsoleKey.DownArrow))
                {
                    Drawer.shouldRefresh = true;
                    Drawer.RefreshScreen();
                }
                HandleInput(ch);
            }
        }

        public static void HandleInput(ConsoleKeyInfo ch)
        {
            if (char.IsDigit(ch.KeyChar) && ch.Key != ConsoleKey.D0)
            {
                HandleSimpleMovement(ch);
                return;
            }

            if (!insertMode)
            {
                ToggleInsertMode(ch);
                MoveTo(ch.Key, 1);
                HandleSpecialKeys(ch);
                HandleArrows(ch.Key);
                HandleKeys(ch.Key);
                MoveWord(ch);
                MoveChar(ch, 1);
                return;
            }

            ToggleInsertMode(ch);
            HandleInsert(ch);
            HandleArrows(ch.Key);
            HandleKeys(ch.Key);
        }

        public static void HandleInsert(ConsoleKeyInfo ch)
        {
            if (ch.Key == ConsoleKey.Backspace && text[row].Length > 0 && col + offsetCol <= text[row].Length && col + offsetCol > 0)
            {
                text[row] = text[row].Remove(col + offsetCol - 1, 1);
                HandleArrows(ConsoleKey.LeftArrow);
            }

            else if (!char.IsControl(ch.KeyChar))
            {
                text[row] = text[row].Insert(col + offsetCol, ch.KeyChar.ToString());
                HandleArrows(ConsoleKey.RightArrow);
            }
        }

        private static void ToggleInsertMode(ConsoleKeyInfo ch)
        {
            if (ch.Key == ConsoleKey.I)
            {
                insertMode = true;
            }

            if (ch.Key == ConsoleKey.Escape)
            {
                insertMode = false;
            }
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
            while (col < text[row].Length && char.IsPunctuation(text[row][col]))
            {
                col++;
            }

            while (col < text[row].Length && char.IsLetter(text[row][col]))
            {
                col++;
            }

            while (col < text[row].Length && !char.IsLetter(text[row][col]) && !char.IsPunctuation(text[row][col]))
            {
                col++;
            }

            while (col < text[row].Length && char.IsWhiteSpace(text[row][col]))
            {
                col++;
            }
        }

        private static void MoveForwardUpperCase()
        {
            while (col < text[row].Length && char.IsLetter(text[row][col]))
            {
                col++;
            }

            while (col < text[row].Length && !char.IsLetter(text[row][col]))
            {
                col++;
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
                if (Drawer.lineNumbers)
                {
                    col = text[row].Length > Console.WindowWidth ? text[row].Length + Drawer.rowIndex.Length : text[row].Length;
                }

                else
                {
                    col = text[row].Length;
                }
            }

            if (ch.KeyChar.ToString() == "0")
            {
                col = 0;
            }

            if (ch.KeyChar.ToString() == "^")
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

            if (ch == ConsoleKey.K)
            {
                Move(ConsoleKey.UpArrow, number);
            }

            if (ch == ConsoleKey.H)
            {
                Move(ConsoleKey.LeftArrow, number);
            }

            if (ch == ConsoleKey.L)
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
            HandlePageScroll(ch);

            if (ch == ConsoleKey.Home)
            {
                col = text[row].Length > 0 ? text[row].IndexOf(text[row].First(c => !char.IsWhiteSpace(c))) : 0;
            }

            if (ch == ConsoleKey.End)
            {
                if (text[row].Length > Console.WindowWidth)
                {
                    col = Drawer.lineNumbers ? text[row].Length + Drawer.rowIndex.Length : text[row].Length;
                }

                else
                {
                    col = text[row].Length;
                }
            }

            if (ch == ConsoleKey.Spacebar && !insertMode)
            {
                var c = Console.ReadKey();
                if (c.Key == ConsoleKey.F)
                {
                    Finder.ResetSettings();
                    Finder.OpenFinder();
                }
            }
        }

        private static void HandlePageScroll(ConsoleKey ch)
        {
            if (ch == ConsoleKey.PageDown)
            {
                row = Console.WindowHeight + offsetRow - 1;
                int end = row + Console.WindowHeight;
                for (int i = row; i < end && i < text.Length - 1; i++)
                {
                    HandleArrows(ConsoleKey.DownArrow);
                }
            }

            if (ch == ConsoleKey.PageUp)
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
                HandleUpArrow();
            }

            if (ch == ConsoleKey.DownArrow && row < text.Length - 1)
            {
                HandleDownArrow();
            }

            if (ch == ConsoleKey.RightArrow && (col < text[row].Length || (text[row].Length > Console.WindowWidth && col < text[row].Length + Drawer.rowIndex.Length - 1)))
            {
                HandleRightArrow();
            }

            if (ch == ConsoleKey.LeftArrow && col > 0)
            {
                col--;
                prevCol = col;
            }
        }

        private static void HandleUpArrow()
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

            if (Drawer.relativeLines)
            {
                Drawer.DrawRelativeIndexes();
            }
        }

        private static void HandleDownArrow()
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

            if (Drawer.relativeLines)
            {
                Drawer.DrawRelativeIndexes();
            }
        }

        private static void HandleRightArrow()
        {
            col++;
            if (col > text[row].Length && !Drawer.lineNumbers)
            {
                col = text[row].Length;
            }
            prevCol = col;
        }
    }
}