namespace TextEditor
{
    public interface ICommand
    {
        void Execute();
        void UnExecute();
    }
}