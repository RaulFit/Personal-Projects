namespace TextEditor
{
    class TextEditor
    {
        static void Main(string[] args)
        {
            ConsoleMode.EnableVTProcessing();

            if (args.Length == 0 || !Path.Exists(args[0]))
            {
               ReadFile();
            }

            else
            {
                Console.WriteLine(File.ReadAllText(args[0]));
            }

            Cursor.PrintPosition();
        }

        static void ReadFile()
        {
            Console.WriteLine("Enter the name of the text file in the format name.txt: ");
            string? fileName = Console.ReadLine();

            while (!Path.Exists(fileName))
            {
                Console.WriteLine("The specified file does not exist. Please enter a different file name: ");
                fileName = Console.ReadLine();
            }

            Console.WriteLine(File.ReadAllText(fileName));
        }
    }
}