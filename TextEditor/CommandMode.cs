using System.IO;
using System.Text;

namespace TextEditor
{
    static class CommandMode
    {
        public static StringBuilder commandMode = new StringBuilder();
        public static string currentPath = "";
        const string ESC = "\x1b[";

        public static void DrawCommandMode()
        {
            Console.SetCursorPosition(0, Console.WindowHeight - 3);
            Drawer.ClearScreen(2);
            Finder.PrintHorizontalBorder(Console.WindowWidth / 2 - 7, commandMode);
            commandMode.Append("Command mode");
            Finder.PrintHorizontalBorder(Console.WindowWidth / 2 - 7, commandMode);
            commandMode.Append($"{ESC}{Console.WindowHeight - 1};{0}H");
            Finder.PrintBorder(true, commandMode);
            commandMode.Append($"{ESC}{Console.WindowHeight - 1};{Console.WindowWidth - 1}H");
            Finder.PrintBorder(true, commandMode);
            commandMode.Append($"{ESC}{Console.WindowHeight};{0}H");
            Finder.PrintHorizontalBorder(Console.WindowWidth - 1, commandMode);
            DrawCorners();
            Console.SetCursorPosition(0, Console.WindowHeight - 3);
            Console.Write(commandMode);
            EnterCommand();
        }

        private static void DrawCorners()
        {
            commandMode.Append("\x1b" + "(0");
            commandMode.Append(ESC + "31m");
            commandMode.Append($"{ESC}{Console.WindowHeight - 2};{0}H");
            commandMode.Append("l");
            commandMode.Append($"{ESC}{Console.WindowHeight};{0}H");
            commandMode.Append("m");
            commandMode.Append($"{ESC}{Console.WindowHeight - 2};{Console.WindowWidth - 1}H");
            commandMode.Append("k");
            commandMode.Append($"{ESC}{Console.WindowHeight};{Console.WindowWidth - 1}H");
            commandMode.Append("j");
            commandMode.Append(ESC + "0m");
            commandMode.Append("\x1b" + "(B");
        }

        private static void EnterCommand()
        {
            while (true)
            {
                Console.SetCursorPosition(1, Console.WindowHeight - 2);
                string? input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    string[] components = input.Split(" ");
                    string command = components[0];

                    if (command.ToLower() == "w" || command.ToLower() == "write")
                    {
                        string path = components.Length > 1 ? components[1] : "";
                        WriteToFile(path, Navigator.text);
                    }

                    QuitApp(command);
                }

                else
                {
                    input = "";
                }

                Console.SetCursorPosition(1, Console.WindowHeight - 2);
                Console.Write(new string(' ', input.Length));
            }
        }

        private static void WriteToFile(string path, List<string> text)
        {
            if (string.IsNullOrEmpty(path))
            {
                File.WriteAllLines(currentPath, Navigator.text);
                Navigator.hasChanges = false;
            }

            if (Path.Exists(path))
            {
                File.WriteAllLines(path, Navigator.text);
                Navigator.hasChanges = false;
            }
        }

        private static void QuitApp(string command)
        {
            if (command.ToLower() == "q!" || command.ToLower() == "quit!")
            {
                Environment.Exit(1);
            }

            if (command.ToLower() == "q" || command.ToLower() == "quit")
            {
                if (Navigator.hasChanges)
                {
                    throw new Exception("You have unsaved changes!");
                }

                Environment.Exit(1);
            }
        }
    }
}