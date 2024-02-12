namespace TextEditor
{
    class TextEditor
    {
        static void Main(string[] args)
        {
            ConsoleMode.EnableVTProcessing();
            HelperFunctions.ClearScreen();
            Cursor.MoveTo(1, 1);

            string[] input = (args.Length == 0 || !Path.Exists(args[0]) ? ReadFile() : File.ReadAllText(args[0])).Split("\n");

            foreach (string line in input)
            {
                Console.WriteLine(line);
            }

            Cursor.Move(input);
        }

        static string ReadFile()
        {
            Console.WriteLine("Enter the name of the text file in the format name.txt: ");
            string? fileName = Console.ReadLine();

            while (!Path.Exists(fileName))
            {
                Console.WriteLine("The specified file does not exist. Please enter a different file name: ");
                fileName = Console.ReadLine();
            }

            return File.ReadAllText(fileName);
        }
    }
}