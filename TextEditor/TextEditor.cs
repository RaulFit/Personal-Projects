using System.Runtime.InteropServices;
using TextEditor;

namespace Editor
{
    class TextEditor
    {
        static void Main(string[] args)
        {
            ConsoleMode.EnableVTProcessing();

            Console.WriteLine("Enter the name of the text file in the format name.txt: ");
            string? fileName = Console.ReadLine();

            while (!Path.Exists(fileName))
            {
                Console.WriteLine("The specified file does not exist. Please enter a different file name: ");
                fileName = Console.ReadLine();
            }

            string filePath = Path.GetFullPath(fileName);
            Console.WriteLine(File.ReadAllText(filePath));
        }
    }
}