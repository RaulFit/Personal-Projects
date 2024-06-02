using System.Text;

namespace TextEditor
{
    public class Drawer
    {
        const string ESC = "\x1b[";
        public string rowIndex;
        public bool shouldRefresh;
        public int windowWidth;
        public int windowHeight;
        public bool lineNumbers;
        public bool relativeLines;
        public StringBuilder text;

        public Drawer(bool lineNumbers, bool relativeLines)
        {
            this.lineNumbers = lineNumbers;
            this.relativeLines = relativeLines;
            rowIndex = "";
            shouldRefresh = false;
            text = new StringBuilder();
        }

        public void RefreshScreen(Navigator navigator)
        {
            if (shouldRefresh)
            {
                Console.Write($"{ESC}?25l");
                Console.SetCursorPosition(0, 0);
                ClearScreen(Console.WindowHeight - 1);
                Console.SetCursorPosition(0, 0);
                DrawContent(navigator);
                Console.Write($"{ESC}?25h");
                shouldRefresh = false;
            }

            DrawStatusBar(navigator);
            DrawCursor(navigator);
        }

        public void DrawContent(Navigator navigator)
        {
            int len = Math.Min(Console.WindowHeight - 1, navigator.text.Count);
            text = new StringBuilder();
            for (int i = 0; i < len; i++)
            {
                int rowIndex = i + navigator.offsetRow;
                DrawRow(rowIndex, navigator);
            }

            if (lineNumbers && relativeLines)
            {
                DrawRelativeIndexes(navigator);
            }

            Console.SetCursorPosition(0, 0);
            Console.Write(text);
        }

        public void DrawStatusBar(Navigator navigator)
        {
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            Console.Write($"{ESC}46m");
            Console.Write(new string(' ', Console.WindowWidth));
            Console.Write($"{ESC}0G");
            if (navigator.insertMode)
            {
                Console.Write("INS");
            }
            else
            {
                Console.Write("NOR");
            }
            Console.Write(new string(' ', Console.WindowWidth / 2 - 3));
            Console.Write(Finder.fileName);
            Console.Write($"{ESC}{Console.WindowWidth - 6}G");
            Console.Write($"{navigator.row}:{navigator.col}");
            Console.Write($"{ESC}0m");
        }

        private void DrawRow(int index, Navigator navigator)
        {
            rowIndex = (index + 1) + " ";
            DrawRowIndex(navigator);

            int lenToDraw = navigator.text[index].Length - navigator.offsetCol;

            if (lenToDraw >= Console.WindowWidth - rowIndex.Length && lineNumbers)
            {
                lenToDraw = Console.WindowWidth - rowIndex.Length - 1;
            }

            else if (lenToDraw >= Console.WindowWidth)
            {
                lenToDraw = Console.WindowWidth;
            }

            AppendLine(index, lenToDraw, navigator);
        }

        private void AppendLine(int index, int length, Navigator navigator)
        {
            if (relativeLines)
            {
                text.Append(new string(' ', 3));
            }

            if (length > 0)
            {
                text.Append(navigator.text[index][navigator.offsetCol..(length + navigator.offsetCol)]);
            }

            if (index < Console.WindowHeight + navigator.offsetRow - 1)
            {
                text.Append("\r\n");
            }
        }

        private void DrawRowIndex(Navigator navigator)
        {
            if (lineNumbers && !relativeLines)
            {
                int lastNumber = Console.WindowHeight + navigator.offsetRow;
                text = lastNumber < 100 ? text.Append($"{ESC}32m{rowIndex,3}{ESC}0m") : text.Append($"{ESC}32m{rowIndex,4}{ESC}0m").Append($"{ESC}1D");
            }
        }

        public void DrawRelativeIndexes(Navigator navigator)
        {
            text.Append($"{ESC}0;0H");
            int num = navigator.row - navigator.offsetRow;
            int i;
            for (i = navigator.offsetRow; i < Console.WindowHeight + navigator.offsetRow - 1; i++)
            {
                DrawRelativeIndex(i, ref num, navigator);
                text.Append("\r\n");
            }

            text.Append($"{ESC}0m");
        }

        private void DrawRelativeIndex(int index, ref int num, Navigator navigator)
        {
            text.Append($"{ESC}32m");
            num = index > navigator.row ? num + 1 : num;
            string idx = navigator.row == index ? navigator.row + " " : num + " ";

            if (navigator.row == index)
            {
                text.Append($"{ESC}31m");
            }

            text = navigator.row < 100 ? text.Append($"{idx,3}") : text.Append($"{idx,4}");
            num = index < navigator.row ? num - 1 : num;
        }

        public static void ClearScreen(int length)
        {
            for (int i = 0; i < length; i++)
            {
                Console.WriteLine(new string(' ', Console.WindowWidth));
            }

            Console.Write(new string(' ', Console.WindowWidth));
        }

        private void DrawCursor(Navigator navigator)
        {
            if (lineNumbers)
            {
                if (Console.WindowHeight + navigator.offsetRow > 100)
                {
                    Console.SetCursorPosition(Math.Min(navigator.col - navigator.offsetCol + rowIndex.Length - 1, Console.WindowWidth - 1),
                    Math.Min(navigator.row - navigator.offsetRow, Console.WindowHeight - 2));
                }

                else
                {
                    Console.SetCursorPosition(Math.Min(navigator.col - navigator.offsetCol + rowIndex.Length, Console.WindowWidth - 1),
                    Math.Min(navigator.row - navigator.offsetRow, Console.WindowHeight - 2));
                }
                return;
            }

            Console.SetCursorPosition(Math.Min(navigator.col - navigator.offsetCol, Console.WindowWidth - 1),
            Math.Min(navigator.row - navigator.offsetRow, Console.WindowHeight - 2));
        }

        public void Scroll(Navigator navigator)
        {
            if (navigator.row >= Console.WindowHeight + navigator.offsetRow - 1)
            {
                navigator.offsetRow = navigator.row - Console.WindowHeight + 2;
                shouldRefresh = true;
            }

            else if (navigator.row < navigator.offsetRow)
            {
                navigator.offsetRow = navigator.row;
                shouldRefresh = true;
            }

            if (navigator.col >= Console.WindowWidth + navigator.offsetCol)
            {
                navigator.offsetCol = navigator.col - Console.WindowWidth + 1;
                shouldRefresh = true;
            }

            else if (navigator.col < navigator.offsetCol)
            {
                navigator.offsetCol = navigator.col;
                shouldRefresh = true;
            }
        }

        public void RefreshText(Navigator navigator)
        {
            if (windowWidth != Console.WindowWidth)
            {
                windowWidth = Console.WindowWidth;
                shouldRefresh = true;
            }

            if (windowHeight != Console.WindowHeight)
            {
                windowHeight = Console.WindowHeight;
                shouldRefresh = true;
            }

            if (navigator.insertMode)
            {
                shouldRefresh = true;
            }

            if (relativeLines)
            {
                shouldRefresh = true;
            }
        }
    }
}