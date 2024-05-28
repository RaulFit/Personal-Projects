using System.Text;

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
        public static StringBuilder text = new StringBuilder();

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
            text = new StringBuilder();

            for (int i = 0; i < len; i++)
            {
                int rowIndex = i + Navigator.offsetRow;
                DrawRow(rowIndex);
            }

            if (lineNumbers && relativeLines)
            {
                DrawRelativeIndexes();
            }

            Console.SetCursorPosition(0, 0);
            Console.Write(text);
        }

        private static void DrawRow(int index)
        {
            rowIndex = (index + 1) + " ";
            DrawRowIndex();

            int lenToDraw = Navigator.text[index].Length - Navigator.offsetCol;

            if (lenToDraw > Console.WindowWidth - rowIndex.Length && lineNumbers)
            {
                lenToDraw = Console.WindowWidth - rowIndex.Length;
            }

            else if (lenToDraw >= Console.WindowWidth)
            {
                lenToDraw = Console.WindowWidth;
            }

            AppendLine(index, lenToDraw);
        }

        private static void AppendLine(int index, int length)
        {
            if (relativeLines)
            {
                text.Append(new string(' ', 3));
            }

            if (length > 0)
            {
                text.Append(Navigator.text[index][Navigator.offsetCol..(length + Navigator.offsetCol)]);
            }

            if (index < Console.WindowHeight + Navigator.offsetRow - 1)
            {
                text.Append("\r\n");
            }
        }

        private static void DrawRowIndex()
        {
            if (lineNumbers && !relativeLines)
            {
                int lastNumber = Console.WindowHeight + Navigator.offsetRow;
                if (lastNumber < 100)
                {
                    text.Append($"{ESC}32m{rowIndex,3}{ESC}0m");
                }

                else if (lastNumber >= 100 && lastNumber < 1000)
                {
                    text.Append($"{ESC}32m{rowIndex,4}{ESC}0m").Append($"{ESC}1D");
                }
            }
        }

        public static void DrawRelativeIndexes()
        {
            text.Append($"{ESC}0;0H");
            int num = Navigator.row - Navigator.offsetRow;
            int i;
            for (i = Navigator.offsetRow; i < Console.WindowHeight + Navigator.offsetRow - 1; i++)
            {
                text.Append($"{ESC}0G");
                DrawIndex(i, ref num);
                text.Append("\r\n");
            }

            DrawIndex(i, ref num);
            text.Append($"{ESC}0G");
            text.Append($"{ESC}0m");
        }

        private static void DrawIndex(int index, ref int num)
        {
            text.Append($"{ESC}32m");
            num = index > Navigator.row ? num + 1 : num;
            string idx = Navigator.row == index ? Navigator.row + " " : num + " ";

            if (Navigator.row == index)
            {
                text.Append($"{ESC}31m");
            }

            if (Navigator.row < 100)
            {
                text.Append($"{idx,3}");
            }

            else if (Navigator.row >= 100 && Navigator.row < 1000)
            {
                text.Append($"{idx,4}");
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

        private static void DrawCursor()
        {
            if (lineNumbers)
            {
                if (Console.WindowHeight + Navigator.offsetRow > 100)
                {
                    Console.SetCursorPosition(Math.Min(Navigator.col - Navigator.offsetCol + rowIndex.Length - 1, Console.WindowWidth - 1), Math.Max(Navigator.row - Navigator.offsetRow, 0));
                }

                else
                {
                    Console.SetCursorPosition(Math.Min(Navigator.col - Navigator.offsetCol + rowIndex.Length, Console.WindowWidth - 1), Math.Max(Navigator.row - Navigator.offsetRow, 0));
                }
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
        }

        public static void RefreshText()
        {
            if (windowWidth != Console.WindowWidth)
            {
                windowWidth = Console.WindowWidth;
                shouldRefresh = true;
            }

            if (Navigator.insertMode)
            {
                shouldRefresh = true;
            }
        }
    }
}