namespace TextEditor
{
    public class InsertCommand : ICommand
    {
        Navigator navigator;
        ConsoleKeyInfo ch;
        int row;
        int col;

        public InsertCommand(Navigator navigator, ConsoleKeyInfo ch, int row, int col)
        {
            this.navigator = navigator;
            this.ch = ch;
            this.row = row;
            this.col = col;
        }

        public void Execute()
        {
            navigator.text[row] = navigator.text[row].Insert(col, ch.KeyChar.ToString());
            navigator.HandleArrows(ConsoleKey.RightArrow);
        }

        public void UnExecute()
        {
            navigator.text[row] = navigator.text[row].Remove(col, 1);
            navigator.HandleArrows(ConsoleKey.LeftArrow);
        }
    }
}
