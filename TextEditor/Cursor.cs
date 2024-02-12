namespace TextEditor
{
    public static class Cursor
    {
        const string ESC = "\x1b[";

        public static void MoveTo(int row, int col) => Console.Write($"{ESC}{row};{col}H");

        public static void Move(string[] input)
        {
            (int row, int col) = (1, 1);

            while (true)
            {
                char ch = Console.ReadKey().KeyChar;

                if (ch == 27)
                {
                    (char next1, char next2) = (Console.ReadKey().KeyChar, Console.ReadKey().KeyChar);

                    if (next1 == '[')
                    {
                        if (next2 == 'A')
                        {
                            row = Math.Max(1, row - 1);
                        }

                        else if (next2 == 'B')
                        {
                            row = Math.Min(input.Length, row + 1);
                        }

                        else if (next2 == 'C')
                        {
                            col = row == input.Length ? Math.Min(input[row - 1].Length + 1, col + 1) : Math.Min(input[row - 1].Length, col + 1);
                        }

                        else if (next2 == 'D')
                        {
                            col = Math.Max(1, col - 1);
                        }
                    }
                }

                MoveTo(row, col);
            }
        }
    }
}
