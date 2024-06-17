namespace TextEditor
{
    public class Navigator
    {
        public CommandMode CommandMode;
        public Drawer Drawer;
        public List<string> text = [];
        public Stack<(List<string>, int row, int col)> undo;
        public Stack<(List<string>, int row, int col)> redo;
        public int row;
        public int offsetRow;
        public int col;
        public int offsetCol;
        public int prevCol;
        public bool insertMode;
        public bool hasChanges;

        public Navigator(List<string> text, Drawer drawer, CommandMode commandMode)
        {
            this.text = text;
            undo = new Stack<(List<string>, int row, int col)>();
            redo = new Stack<(List<string>, int row, int col)>();
            row = 0;
            offsetRow = 0;
            col = 0;
            offsetCol = 0;
            prevCol = 0;
            insertMode = false;
            hasChanges = false;
            Drawer = drawer;
            CommandMode = commandMode;
        }

        public void RunNavigator(bool warning)
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Drawer.DrawContent(this);
            if (warning)
            {
                Drawer.DrawWarning();
                var key = Console.ReadKey(true);
                Drawer.shouldRefresh = true;
            }

            while (true)
            {
                Drawer.Scroll(this);
                Drawer.RefreshText(this);
                Drawer.RefreshScreen(this);
                ConsoleKeyInfo ch = insertMode ? Console.ReadKey() : Console.ReadKey(true);
                HandleInput(ch);
            }
        }

        public void HandleInput(ConsoleKeyInfo ch)
        {
            if (!insertMode)
            {
                if (char.IsDigit(ch.KeyChar) && ch.Key != ConsoleKey.D0)
                {
                    HandleSimpleMovement(ch);
                    return;
                }

                HandleUndoRedo(ch);
                ToggleInsertMode(ch);
                HandleToggleInsertKeys(ch);
                MoveTo(ch.Key, 1);
                HandleSpecialKeys(ch);
                HandleArrows(ch.Key);
                HandleKeys(ch.Key);
                HandlePageScroll(ch);
                MoveWord(ch);
                MoveChar(ch, 1);
                return;
            }

            ToggleInsertMode(ch);
            HandleInsert(ch);
            HandleArrows(ch.Key);
            HandleKeys(ch.Key);
            HandlePageScroll(ch);
        }

        public void HandleUndoRedo(ConsoleKeyInfo ch)
        {
            if (ch.KeyChar.ToString() == "u" & hasChanges)
            {
                if (undo.Count > 0)
                {
                    (List<string> originalText, int row, int col) values = undo.Pop();
                    redo.Push((text, values.row, values.col));
                    text = new List<string>(values.originalText);
                    row = values.row;
                    col = values.col;
                    Drawer.shouldRefresh = true;
                }
            }

            if ((ch.Modifiers & ConsoleModifiers.Control) != 0 && ch.Key == ConsoleKey.R)
            {
                if (redo.Count > 0)
                {
                    (List<string> newText, int row, int col) values = redo.Pop();
                    undo.Push((text, values.row, values.col));
                    text = new List<string>(values.newText);
                    row = values.row;
                    col = values.col;
                    hasChanges = true;
                    Drawer.shouldRefresh = true;
                }
            }
        }

        public void HandleInsert(ConsoleKeyInfo ch)
        {
            if (ch.Key == ConsoleKey.Enter)
            {
                HandleEnter();
                hasChanges = true;
                return;
            }

            if (ch.Key == ConsoleKey.Backspace)
            {
                HandleBackspace();
                hasChanges = true;
                return;
            }

            if (ch.Key == ConsoleKey.Delete)
            {
                HandleDelete();
                hasChanges = true;
                return;
            }

            if (ch.Key == ConsoleKey.RightArrow || ch.Key == ConsoleKey.LeftArrow || ch.Key == ConsoleKey.UpArrow || ch.Key == ConsoleKey.DownArrow)
            {
                return;
            }

            if (!char.IsControl(ch.KeyChar))
            {
                text[row] = text[row].Insert(col, ch.KeyChar.ToString());
                HandleArrows(ConsoleKey.RightArrow);
            }

            hasChanges = true;
        }

        private void HandleEnter()
        {
            if (col == 0)
            {
                text.Insert(row, "");
            }

            else
            {
                text.Insert(row + 1, text[row][col..]);
                text[row] = text[row][0..col];
            }

            HandleArrows(ConsoleKey.DownArrow);
        }

        private void HandleBackspace()
        {
            if (col == 0)
            {
                if (row > 0)
                {
                    HandleArrows(ConsoleKey.UpArrow);
                    col = text[row].Length;
                    text[row] = text[row].Insert(text[row].Length, text[row + 1]);
                    text.RemoveAt(row + 1);
                    if (text.Count + offsetRow > text.Count)
                    {
                        offsetRow--;
                    }
                }
            }

            else
            {
                text[row] = text[row].Remove(col - 1, 1);
                HandleArrows(ConsoleKey.LeftArrow);
            }
        }

        private void HandleDelete()
        {
            if (col == text[row].Length)
            {
                if (row < text.Count - 1)
                {
                    text[row] = text[row].Insert(text[row].Length, text[row + 1]);
                    text.RemoveAt(row + 1);
                    if (text.Count + offsetRow > text.Count)
                    {
                        offsetRow--;
                    }
                }
            }

            else
            {
                text[row] = text[row].Remove(col, 1);
            }
        }

        private void HandleToggleInsertKeys(ConsoleKeyInfo ch)
        {
            if (ch.KeyChar.ToString() == "a" && !insertMode)
            {
                undo.Push((new List<string>(text), row, col));
                HandleArrows(ConsoleKey.RightArrow);
                insertMode = true;
            }

            if (ch.KeyChar.ToString() == "A" && !insertMode)
            {
                undo.Push((new List<string>(text), row, col));
                HandleKeys(ConsoleKey.End);
                insertMode = true;
            }

            if (ch.KeyChar.ToString() == "I" && !insertMode)
            {
                undo.Push((new List<string>(text), row, col));
                HandleKeys(ConsoleKey.Home);
                insertMode = true;
            }
        }

        private void ToggleInsertMode(ConsoleKeyInfo ch)
        {
            if (ch.KeyChar.ToString() == "i" && !insertMode)
            {
                undo.Push((new List<string>(text), row, col));
                insertMode = true;
            }

            if (ch.Key == ConsoleKey.Escape && insertMode)
            {
                insertMode = false;
            }
        }

        private void HandleSimpleMovement(ConsoleKeyInfo ch)
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

        private void FindCharLowerCase(ConsoleKeyInfo ch)
        {
            int index = text[row].IndexOf(ch.KeyChar.ToString(), Math.Min(col + 1, text[row].Length));
            col = index != -1 ? index : col;
        }

        private void FindCharUpperCase(ConsoleKeyInfo ch)
        {
            int index = text[row].LastIndexOf(ch.KeyChar.ToString(), Math.Max(col - 1, 0));
            col = index != -1 ? index : col;
        }

        private void MoveChar(ConsoleKeyInfo ch, int num)
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

        private void MoveWords(ConsoleKeyInfo ch, int num)
        {
            for (int i = 0; i < num; i++)
            {
                MoveWord(ch);
            }
        }

        private void MoveWord(ConsoleKeyInfo ch)
        {
            if (ch.KeyChar.ToString() == "w")
            {
                MoveForwardLowerCase();
                while ((col == text[row].Length || char.IsWhiteSpace(text[row][col])) && row < text.Count - 1)
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
                while ((col == text[row].Length || char.IsWhiteSpace(text[row][col])) && row < text.Count - 1)
                {
                    row++;
                    col = 0;
                    MoveForwardUpperCase();
                }
            }
        }

        private void MoveForwardLowerCase()
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

        private void MoveForwardUpperCase()
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

        private void MoveBackwards()
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

        private void HandleSpecialKeys(ConsoleKeyInfo ch)
        {
            if (ch.KeyChar.ToString() == ":")
            {
                CommandMode.DrawCommandMode(this);
                Drawer.shouldRefresh = true;
                Drawer.RefreshScreen(this);
            }

            if (ch.KeyChar.ToString() == "$")
            {
                if (Drawer.lineNumbers)
                {
                    col = text[row].Length;
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

            if (ch.KeyChar.ToString() == "o")
            {
                undo.Push((new List<string>(text), row, col));
                string emptySpace = new string(' ', Math.Max(text[row].IndexOf(text[row].First(c => !char.IsWhiteSpace(c))), 0));
                HandleArrows(ConsoleKey.DownArrow);
                text.Insert(row, emptySpace);
                col = text[row].Length;
                insertMode = true;
            }

            if (ch.KeyChar.ToString() == "O")
            {
                undo.Push((new List<string>(text), row, col));
                text.Insert(row, new string(' ', Math.Max(text[row].IndexOf(text[row].First(c => !char.IsWhiteSpace(c))), 0)));
                col = text[row].Length;
                insertMode = true;
            }
        }

        public void MoveTo(ConsoleKey ch, int number)
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

        public void Move(ConsoleKey ch, int num)
        {
            for (int i = 0; i < num; i++)
            {
                HandleArrows(ch);
            }
        }

        private void HandleKeys(ConsoleKey ch)
        {
            if (ch == ConsoleKey.Home)
            {
                col = text[row].Length > 0 ? text[row].IndexOf(text[row].First(c => !char.IsWhiteSpace(c))) : 0;
            }

            if (ch == ConsoleKey.End)
            {
                col = text[row].Length;
            }

            if (ch == ConsoleKey.Spacebar && !insertMode)
            {
                var c = Console.ReadKey();
                if (c.Key == ConsoleKey.F)
                {
                    Finder.ResetSettings();
                    Finder.OpenFinder(this);
                }
            }
        }

        private void HandlePageScroll(ConsoleKeyInfo ch)
        {
            if (ch.Key == ConsoleKey.PageDown || ((ch.Modifiers & ConsoleModifiers.Control) != 0 && ch.Key == ConsoleKey.D))
            {
                row = Drawer.windowHeight + offsetRow - 2;
                int end = row + Drawer.windowHeight;
                for (int i = row; i < end && i < text.Count - 1; i++)
                {
                    HandleArrows(ConsoleKey.DownArrow);
                }
            }

            if (ch.Key == ConsoleKey.PageUp || ((ch.Modifiers & ConsoleModifiers.Control) != 0 && ch.Key == ConsoleKey.U))
            {
                row = offsetRow;
                int end = row - Drawer.windowHeight;
                for (int i = row; i > end && i > 0; i--)
                {
                    HandleArrows(ConsoleKey.UpArrow);
                }
            }
        }

        private void HandleArrows(ConsoleKey ch)
        {
            if (ch == ConsoleKey.UpArrow && row > 0)
            {
                HandleUpArrow();
            }

            if (ch == ConsoleKey.DownArrow && row < text.Count - 1)
            {
                HandleDownArrow();
            }

            if (ch == ConsoleKey.RightArrow && col < text[row].Length)
            {
                HandleRightArrow();
            }

            if (ch == ConsoleKey.LeftArrow && col > 0)
            {
                col--;
                prevCol = col;
            }
        }

        private void HandleUpArrow()
        {
            if (col > prevCol)
            {
                prevCol = col;
            }

            if (col >= text[row - 1].Length)
            {
                col = Math.Min(Drawer.windowWidth + offsetCol - 1, text[row - 1].Length);
            }

            else if (text[row - 1].Length > text[row].Length)
            {
                col = prevCol > text[row - 1].Length ? text[row - 1].Length : prevCol;
            }

            row--;

            if (Drawer.relativeLines)
            {
                Drawer.DrawRelativeIndexes(this);
            }
        }

        private void HandleDownArrow()
        {
            if (col > prevCol)
            {
                prevCol = col;
            }

            if (col >= text[row + 1].Length)
            {
                col = Math.Min(Drawer.windowWidth + offsetCol - 1, text[row + 1].Length);
            }

            else if (text[row + 1].Length > text[row].Length)
            {
                col = prevCol > text[row + 1].Length ? text[row + 1].Length : prevCol;
            }

            row++;

            if (Drawer.relativeLines)
            {
                Drawer.DrawRelativeIndexes(this);
            }
        }

        private void HandleRightArrow()
        {
            col++;

            if (col > text[row].Length)
            {
                col = text[row].Length;
            }

            prevCol = col;
        }
    }
}