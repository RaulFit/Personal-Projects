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

        static void Main(string[] args)
        {
            OpenFile(args);

            while (true)
            {
                Scroll();
                RefreshScreen();
                HandleInput();
            }
        }

        private static void RefreshScreen()
        {
            Console.Write("\x1b[2J");
            Console.Write("\x1b[1;1H");
            DrawContent();
            DrawCursor();
        }

        private static void DrawContent()
        {
            for (int i = 0; i < Console.WindowHeight; i++)
            {
                int rowIndex = i + offsetRow;

                if (rowIndex >= text.Length)
                {
                    return;
                }

                DrawRow(rowIndex);
            }
        }

        private static void DrawRow(int index)
        {
            int lenToDraw = text[index].Length - offsetCol;

            if (lenToDraw <= 0)
            {
                Console.WriteLine();
            }

            if (lenToDraw > Console.WindowWidth)
            {
                lenToDraw = Console.WindowWidth;
            }

            if (lenToDraw > 0)
            {
                if (index == Console.WindowHeight + offsetRow - 1)
                {
                    Console.Write(text[index][offsetCol..(lenToDraw + offsetCol)]);
                }

                else
                {
                    Console.WriteLine(text[index][offsetCol..(lenToDraw + offsetCol - 1)]);
                }

            }
        }

        private static void DrawCursor() => Console.SetCursorPosition(col - offsetCol, row - offsetRow);

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

            else if (arrow == 'C' && col < text[row].Length)
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
