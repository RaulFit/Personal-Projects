namespace TextEditor
{
    class TextEditor
    {
        const string ESC = "\x1b[";
        static string[] text = { };
        static int row = 0;
        static int col = 0;
        static int offsetRow = 0;
        static int offsetCol = 0;

        static void Main(string[] args)
        {
            ConsoleMode.EnableVTProcessing();
            OpenFile(args);
           
            while (true)
            {
                Scroll();
                RefreshScreen();
                HandleInput();
            }
        }

        private static void ClearScreen() => Console.Write($"{ESC}2J");

        private static void MoveTo(int row, int col) => Console.Write($"{ESC}{row};{col}H");

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

        private static void RefreshScreen()
        {
            ClearScreen();
            MoveTo(1, 1);
            DrawContent();
            DrawCursor();
        }

        private static void DrawContent()
        {
            int length = Math.Min(Console.WindowHeight, text.Length);

            for (int i = 0; i < length; i++)
            {
                int rowIndex = i + offsetRow;
                int lengthToDraw = text[rowIndex].Length - offsetCol;

                if (lengthToDraw < 0)
                {
                    lengthToDraw = 0;
                }

                if (lengthToDraw > Console.WindowWidth)
                {
                    lengthToDraw = Console.WindowWidth;
                }

                if (lengthToDraw > 0)
                {
                    Console.WriteLine(text[rowIndex][offsetCol..(lengthToDraw + offsetCol - 1)]);
                }
            }
        }

        private static void DrawCursor() => MoveTo(row - offsetRow + 1, col - offsetCol + 1);

        private static void HandleInput()
        {
            char ch = Console.ReadKey().KeyChar;

            if (ch == 27)
            {
                (char next1, char next2) = (Console.ReadKey().KeyChar, Console.ReadKey().KeyChar);

                if (next1 == '[')
                {
                    HandleArrows(next2);
                }
            }
        }

        private static void HandleArrows(char arrow)
        {
            if (arrow == 'A' && row > 0)
            {
                row--;
            }

            else if (arrow == 'B' && row < text.Length - 1)
            {
                row++;
            }

            else if (arrow == 'C' && col < text[row].Length - 1)
            {
                col++;
            }

            else if (arrow == 'D' && col > 0)
            {
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