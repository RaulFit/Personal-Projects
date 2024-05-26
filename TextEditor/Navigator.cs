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

        public static void RunNavigator()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Drawer.DrawContent();
            while (true)
            {
                Drawer.Scroll();
                Drawer.RefreshScreen();
                var ch = Console.ReadKey(true);
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
                if (Drawer.lineNumbers)
                {
                    col = text[row].Length > Console.WindowWidth ? text[row].Length + Drawer.rowIndex.Length : text[row].Length;
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
                    col = Drawer.lineNumbers ? text[row].Length + Drawer.rowIndex.Length : text[row].Length;
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

            else if (ch == ConsoleKey.Spacebar)
            {
                var c = Console.ReadKey();

                if (c.Key == ConsoleKey.F)
                {
                    Finder.ResetSettings();
                    Finder.OpenFinder();
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

                if (Drawer.relativeLines)
                {
                    Drawer.DrawRelativeIndexes();
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

                if (Drawer.relativeLines)
                {
                    Drawer.DrawRelativeIndexes();
                }
            }

            else if (ch == ConsoleKey.RightArrow && (col < text[row].Length || (text[row].Length > Console.WindowWidth && col < text[row].Length + Drawer.rowIndex.Length - 1)))
            {
                col++;
                if (col > text[row].Length && !Drawer.lineNumbers)
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