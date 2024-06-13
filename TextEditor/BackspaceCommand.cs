﻿namespace TextEditor
{
    public class BackspaceCommand : ICommand
    {
        Navigator navigator;
        List<string> text;
        int row;
        int col;

        public BackspaceCommand(Navigator navigator, int row, int col)
        {
            this.navigator = navigator;
            text = new List<string>(navigator.text);
            this.row = row;
            this.col = col;
        }

        public void Execute()
        {
            if (col == 0)
            {
                if (row > 0)
                {
                    navigator.HandleArrows(ConsoleKey.UpArrow);
                    row = row > 0 ? row - 1 : row;
                    navigator.text[row] = navigator.text[row].Insert(navigator.text[row].Length, navigator.text[row + 1]);
                    navigator.text.RemoveAt(row + 1);
                    if (navigator.text.Count + navigator.offsetRow > navigator.text.Count)
                    {
                        navigator.offsetRow--;
                    }
                }
            }

            else
            {
                navigator.text[row] = navigator.text[row].Remove(col - 1, 1);
                navigator.HandleArrows(ConsoleKey.LeftArrow);
            }
        }

        public void UnExecute()
        {
            navigator.text = new List<string>(text);
        }
    }
}
