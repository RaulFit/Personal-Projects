namespace TextEditor
{
    public class DeleteCommand : ICommand
    {
        Navigator navigator;
        List<string> text;
        int row;
        int col;

        public DeleteCommand(Navigator navigator, int row, int col)
        {
            this.navigator = navigator;
            text = new List<string>(navigator.text);
            this.row = row;
            this.col = col;
        }

        public void Execute()
        {
            if (col == text[row].Length)
            {
                if (row < text.Count - 1)
                {
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
                navigator.text[row] = navigator.text[row].Remove(col, 1);
            }

            navigator.col = Math.Min(navigator.col, navigator.text[row].Length);
        }

        public void UnExecute()
        {
            navigator.text = new List<string>(text);
        }
    }
}
