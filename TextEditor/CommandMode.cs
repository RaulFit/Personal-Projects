using System.Text;

namespace TextEditor
{
    public class CommandMode
    {
        const string ESC = "\x1b[";
        public StringBuilder commandMode;
        public string CurrentPath;

        public CommandMode(string? currentPath)
        {
            commandMode = new StringBuilder();
            CurrentPath = string.IsNullOrEmpty(currentPath) ? "" : currentPath;
        }

        public void DrawCommandMode(Navigator navigator)
        {
            commandMode = new StringBuilder();
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
            EnterCommand(navigator);
        }

        private void DrawCorners()
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

        private void EnterCommand(Navigator navigator)
        {
            while (true)
            {
                Console.SetCursorPosition(1, Console.WindowHeight - 2);
                string? input = Console.ReadLine();

                if (input == "e" || input == "exit")
                {
                    return;
                }

                if (!string.IsNullOrWhiteSpace(input))
                {
                    string[] components = input.Split(" ");
                    string command = components[0];

                    if (command.ToLower() == "w" || command.ToLower() == "write")
                    {
                        string path = components.Length > 1 ? components[1] : "";
                        WriteToFile(path, navigator);
                    }

                    QuitApp(command, navigator);
                }

                Console.SetCursorPosition(1, Console.WindowHeight - 2);
                Console.Write(new string(' ', Console.WindowWidth - 10));
            }
        }

        private void WriteToFile(string path, Navigator navigator)
        {
            if (string.IsNullOrEmpty(path))
            {
                File.WriteAllLines(CurrentPath, navigator.text);
                navigator.hasChanges = false;
            }

            if (Path.Exists(path))
            {
                File.WriteAllLines(path, navigator.text);
                navigator.hasChanges = false;
            }
        }

        private void QuitApp(string command, Navigator navigator)
        {
            if (command.ToLower() == "q!" || command.ToLower() == "quit!")
            {
                Environment.Exit(1);
            }

            if (command.ToLower() == "q" || command.ToLower() == "quit")
            {
                if (navigator.hasChanges)
                {
                    Console.SetCursorPosition(1, Console.CursorTop - 1);
                    Console.Write("You have unsaved changes! Press any key to continue");
                    var key = Console.ReadKey(true);
                    return;
                }

                Environment.Exit(1);
            }
        }
    }
}