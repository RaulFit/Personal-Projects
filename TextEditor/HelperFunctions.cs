namespace TextEditor
{
    public static class HelperFunctions
    {
        const string ESC = "\x1b[";

        public static void ClearScreen() => Console.Write($"{ESC}2J");
    }
}
