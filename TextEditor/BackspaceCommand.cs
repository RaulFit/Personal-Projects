namespace TextEditor
{
    public class BackspaceCommand : ICommand
    {
        Navigator navigator;
        int row;
        int col;
        char chToInsert;

        public BackspaceCommand(Navigator navigator, int row, int col)
        {
            this.navigator = navigator;
            this.row = row;
            this.col = col;
            if (col > 0)
            {
                chToInsert = navigator.text[row][col - 1];
            }
        }

        public void Execute()
        {
            if (col == 0)
            {
                if (row > 0)
                {
                    navigator.HandleArrows(ConsoleKey.UpArrow);
                    row = row > 0 ? row - 1 : row;
                    row = navigator.row;
                    col = navigator.text[row].Length;
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
                col = navigator.col;
            }
        }

        public void UnExecute()
        {
            if (col > 0)
            {
                navigator.text[row] = navigator.text[row].Insert(col - 1, chToInsert.ToString());
                navigator.HandleArrows(ConsoleKey.RightArrow);
                col = navigator.col;
            }
        }
    }
}
