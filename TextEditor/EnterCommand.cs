namespace TextEditor
{
    public class EnterCommand : ICommand
    {
        Navigator navigator;
        int row;
        int col;

        public EnterCommand(Navigator navigator, int row, int col)
        {
            this.navigator = navigator;
            this.row = row;
            this.col = col;
        }

        public void Execute()
        {
            if (col == 0)
            {
                navigator.text.Insert(row, "");
            }

            else
            {
                navigator.text.Insert(row + 1, navigator.text[row][col..]);
                navigator.text[row] = navigator.text[row][0..col];
            }

            navigator.HandleArrows(ConsoleKey.DownArrow);
        }

        public void UnExecute()
        {
            navigator.HandleArrows(ConsoleKey.UpArrow);
            col = navigator.text[row].Length;
            navigator.text[row] = navigator.text[row].Insert(navigator.text[row].Length, navigator.text[row + 1]);
            navigator.text.RemoveAt(row + 1);
            if (navigator.text.Count + navigator.offsetRow > navigator.text.Count)
            {
                navigator.offsetRow--;
            }
        }
    }
}
