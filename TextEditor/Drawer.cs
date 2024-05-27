namespace TextEditor
{
    static class Drawer
    {
        const string ESC = "\x1b[";
        public static string rowIndex = "";
        public static bool shouldRefresh = false;
        public static int windowWidth = Console.WindowWidth;
        public static bool lineNumbers = false;
        public static bool relativeLines = false;

        public static void RefreshScreen()
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

        public static void DrawContent()
        {
            int len = Math.Min(Console.WindowHeight, Navigator.text.Length);

            for (int i = 0; i < len; i++)
            {
                int rowIndex = i + Navigator.offsetRow;
                DrawRow(rowIndex);
            }

            if (relativeLines)
            {
                DrawRelativeIndexes();
            }
        }

        private static void DrawRow(int index)
        {
            rowIndex = (index + 1) + " ";
            DrawRowIndex();

            int lenToDraw = Navigator.text[index].Length - Navigator.offsetCol;

            if (lenToDraw > Console.WindowWidth - rowIndex.Length && lineNumbers)
            {
                lenToDraw = Console.WindowWidth - rowIndex.Length;

                if (relativeLines)
                {
                    lenToDraw = Console.WindowWidth - Navigator.row.ToString().Length - 2;
                }
            }

            else if (lenToDraw >= Console.WindowWidth)
            {
                lenToDraw = Console.WindowWidth;
            }

            if (lenToDraw > 0)
            {
                Console.Write(Navigator.text[index][Navigator.offsetCol..(lenToDraw + Navigator.offsetCol)]);
            }

            if (index < Console.WindowHeight + Navigator.offsetRow - 1)
            {
                Console.CursorTop++;
                Console.CursorLeft = 0;
            }
        }

        public static void DrawRelativeIndexes()
        {
            Console.Write($"{ESC}?25l");
            Console.SetCursorPosition(0, 0);
            int num = Navigator.row - Navigator.offsetRow;
            int i;
            for (i = Navigator.offsetRow; i < Console.WindowHeight + Navigator.offsetRow - 1; i++)
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
            num = index > Navigator.row ? num + 1 : num;
            string idx = Navigator.row == index ? Navigator.row + " " : num + " ";

            if (Navigator.row == index)
            {
                Console.Write($"{ESC}31m");
            }

            if (Navigator.row < 100)
            {
                Console.Write($"{idx,3}");
            }

            else if (Navigator.row >= 100 && Navigator.row < 1000)
            {
                Console.Write($"{idx,4}");
                Console.CursorLeft = idx.ToString().Length;
            }

            num = index < Navigator.row ? num - 1 : num;
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
                int lastNumber = Console.WindowHeight + Navigator.offsetRow;
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
                Console.SetCursorPosition(Math.Min(Navigator.col - Navigator.offsetCol + rowIndex.Length, Console.WindowWidth - 1), Math.Max(Navigator.row - Navigator.offsetRow, 0));
                return;
            }

            Console.SetCursorPosition(Math.Min(Navigator.col - Navigator.offsetCol, Console.WindowWidth - 1), Math.Max(Navigator.row - Navigator.offsetRow, 0));
        }

        public static void Scroll()
        {
            if (Navigator.row >= Console.WindowHeight + Navigator.offsetRow)
            {
                Navigator.offsetRow = Navigator.row - Console.WindowHeight + 1;
                shouldRefresh = true;
            }

            else if (Navigator.row < Navigator.offsetRow)
            {
                Navigator.offsetRow = Navigator.row;
                shouldRefresh = true;
            }

            if (Navigator.col >= Console.WindowWidth + Navigator.offsetCol)
            {
                Navigator.offsetCol = Navigator.col - Console.WindowWidth + 1;
                shouldRefresh = true;
            }

            else if (Navigator.col < Navigator.offsetCol)
            {
                Navigator.offsetCol = Navigator.col;
                shouldRefresh = true;
            }

            if (windowWidth != Console.WindowWidth)
            {
                windowWidth = Console.WindowWidth;
                shouldRefresh = true;
            }
        }
    }
}