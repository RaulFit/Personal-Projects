namespace Editor
{
    class TextEditor
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                throw new ArgumentNullException("The specified path does not exist. Please provide a correct path");
            }

            string filePath = args[0];

            if (!File.Exists(filePath))
            {
                throw new ArgumentException("The specified path is incorrect or does not exist. Please provide a correct path");
            }

            Console.WriteLine(File.ReadAllText(filePath));
        }
    }
}