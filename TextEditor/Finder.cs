using System.Configuration;
using System.Text;

namespace TextEditor
{
    static class Finder
    {
        const string ESC = "\x1b[";
        public static string match = "";
        public static int startIndex = 0;
        public static int endIndex = 0;
        public static int currentIndex = 0;
        public static StringBuilder finder = new StringBuilder();
        public static string fileName = "";

        public static void OpenFinder(Navigator navigator)
        {
            Console.Clear();
            DrawFinder();
            while (true)
            {
                var ch = Console.ReadKey();
                Files.SearchFile(ch);

                if (ch.Key == ConsoleKey.Enter)
                {
                    ReadText();
                }

                if (ch.Key == ConsoleKey.Escape)
                {
                    navigator.RunNavigator();
                }

                if (ch.Key == ConsoleKey.LeftArrow || ch.Key == ConsoleKey.RightArrow)
                {
                    continue;
                }

                if (ch.Key == ConsoleKey.UpArrow || ch.Key == ConsoleKey.DownArrow)
                {
                    SelectFile(ch);
                    ReadText();
                }

                int left = Console.CursorLeft;
                Console.Write($"{ESC}?25l");
                Files.RefreshFiles();
                ColorMatchingLetters();
                Console.Write($"{ESC}?25h");

                if (ch.Key == ConsoleKey.Backspace)
                {
                    Console.CursorLeft = left;
                }
            }
        }

        private static void ReadText()
        {
            fileName = Path.GetFileName(Files.filteredFiles.ElementAt(currentIndex));
            CommandMode commandMode = new CommandMode(Path.GetFullPath(Files.filteredFiles.ElementAt(currentIndex)));
            bool lineNumbers = Convert.ToBoolean(ConfigurationManager.AppSettings.Get("lineNumbers"));
            bool relativeLines = Convert.ToBoolean(ConfigurationManager.AppSettings.Get("relativeLines"));
            Drawer drawer = new Drawer(lineNumbers, relativeLines);
            Navigator navigator = new Navigator(File.ReadAllLines(Path.GetFullPath(Files.filteredFiles.ElementAt(currentIndex))).ToList(), drawer, commandMode);
            navigator.RunNavigator();
        }

        private static void SelectFile(ConsoleKeyInfo ch)
        {
            Console.SetCursorPosition(1, Console.WindowHeight - 6);
            while (ch.Key != ConsoleKey.Enter)
            {
                ch = Console.ReadKey(true);
                if (ch.Key == ConsoleKey.UpArrow || ch.Key == ConsoleKey.DownArrow)
                {
                    Files.SelectFile(ch, Files.filteredFiles);
                }

                else if (ch.Key != ConsoleKey.Enter)
                {
                    Console.SetCursorPosition(match.Length + 1, Console.WindowHeight - 2);
                    Console.Write(ch.KeyChar.ToString());
                    currentIndex = 0;
                    Files.SearchFile(ch);
                    Console.Write($"{ESC}?25l");
                    Files.RefreshFiles();
                    ColorMatchingLetters();
                    Console.Write($"{ESC}?25h");
                    Console.SetCursorPosition(1, Console.WindowHeight - 6);
                }
            }
        }

        public static void ResetSettings()
        {
            finder = new StringBuilder();
            fileName = "";
            match = "";
            startIndex = 0;
            endIndex = 0;
            currentIndex = 0;
        }

        private static void DrawFinder()
        {
            DrawBorders();
            DrawCorners();
            Console.SetCursorPosition(0, 0);
            Console.Write(finder);
            Console.SetCursorPosition(1, Console.WindowHeight - 6);
            Files.files = Directory.GetFiles(Environment.CurrentDirectory, "*.*", SearchOption.AllDirectories).Select(Path.GetFullPath).ToArray();
            Files.filteredFiles = new string[Files.files.Length];
            Files.files.CopyTo(Files.filteredFiles, 0);
            endIndex = Math.Min(Files.files.Length - 1, Console.WindowHeight - 6);
            Files.PrintFiles(Files.filteredFiles.Select(f => Path.GetRelativePath(Directory.GetCurrentDirectory(), Path.GetFullPath(f))).ToArray());
            Console.SetCursorPosition(1, Console.WindowHeight - 2);
        }

        private static void DrawBorders()
        {
            PrintVerticalBorders(Console.WindowHeight - 4, finder);
            finder.Append($"{ESC}{0};{0}H");
            PrintHorizontalBorder(Console.WindowWidth / 2 - 4, finder);
            finder.Append("Results");
            PrintHorizontalBorder(Console.WindowWidth / 2 - 4, finder);
            finder.Append($"{ESC}{Console.WindowHeight - 4};{0}H");
            PrintHorizontalBorder(Console.WindowWidth - 1, finder);
            finder.Append($"{ESC}{Console.WindowHeight - 2};{0}H");
            PrintHorizontalBorder(Console.WindowWidth / 2 - 5, finder);
            finder.Append("Find Files");
            PrintHorizontalBorder(Console.WindowWidth / 2 - 6, finder);
            finder.Append($"{ESC}{Console.WindowHeight - 1};{0}H");
            PrintBorder(true, finder);
            finder.Append($"{ESC}{Console.WindowHeight - 1};{Console.WindowWidth - 1}H");
            PrintBorder(true, finder);
            finder.Append($"{ESC}{Console.WindowHeight};{0}H");
            PrintHorizontalBorder(Console.WindowWidth - 1, finder);
        }

        public static void PrintVerticalBorders(int length, StringBuilder finder)
        {
            for (int i = 0; i < length; i++)
            {
                finder.Append($"{ESC}{i};{0}H");
                PrintBorder(true, finder);
                finder.Append($"{ESC}{i};{Console.WindowWidth - 1}H");
                PrintBorder(true, finder);
            }
        }

        public static void PrintHorizontalBorder(int length, StringBuilder finder)
        {
            for (int i = 0; i < length; i++)
            {
                PrintBorder(false, finder);
            }
        }

        private static void DrawCorners()
        {
            finder.Append("\x1b" + "(0");
            finder.Append(ESC + "31m");
            finder.Append($"{ESC}{0};{0}H");
            finder.Append("l");
            finder.Append($"{ESC}{Console.WindowHeight - 4};{0}H");
            finder.Append("m");
            finder.Append($"{ESC}{0};{Console.WindowWidth - 1}H");
            finder.Append("k");
            finder.Append($"{ESC}{Console.WindowHeight - 4};{Console.WindowWidth - 1}H");
            finder.Append("j");
            finder.Append($"{ESC}{Console.WindowHeight - 2};{0}H");
            finder.Append("l");
            finder.Append($"{ESC}{Console.WindowHeight};{0}H");
            finder.Append("m");
            finder.Append($"{ESC}{Console.WindowHeight - 2};{Console.WindowWidth - 1}H");
            finder.Append("k");
            finder.Append($"{ESC}{Console.WindowHeight};{Console.WindowWidth - 1}H");
            finder.Append("j");
            finder.Append(ESC + "0m");
            finder.Append("\x1b" + "(B");
        }

        public static void ColorMatchingLetters()
        {
            Console.SetCursorPosition(1, Console.WindowHeight - 6);
            Console.Write($"{ESC}32m");
            for (int i = startIndex; i < endIndex && Console.CursorTop > 0; i++)
            {
                ColorPattern(Files.filteredFiles[i], 0);
                Console.SetCursorPosition(1, Console.CursorTop - 1);
            }
            Console.SetCursorPosition(match.Length + 1, Console.WindowHeight - 2);
            Console.Write($"{ESC}0m");
        }

        private static void ColorPattern(string file, int start)
        {
            string fileName = Path.GetRelativePath(Directory.GetCurrentDirectory(), Path.GetFullPath(file));
            string lowerFileName = fileName.ToLower();
            string lowerMatch = match.ToLower();

            if (lowerFileName.Contains(lowerMatch) && !Files.caseInsensitive)
            {
                Console.CursorLeft = lowerFileName.IndexOf(lowerMatch) + 1;
                Console.Write(new string(' ', lowerMatch.Length));
                Console.CursorLeft = lowerFileName.IndexOf(lowerMatch) + 1;
                Console.Write(fileName.Substring(Console.CursorLeft - 1, match.Length));
                return;
            }

            if (Files.caseInsensitive)
            {
                lowerFileName = fileName;
                lowerMatch = match;
            }

            for (int j = 0; j < fileName.Length && start < lowerMatch.Length; j++)
            {
                if (lowerMatch[start] == lowerFileName[j])
                {
                    Console.CursorLeft = lowerFileName.IndexOf(lowerMatch[start], j) + 1;
                    start++;
                    Console.Write(fileName[Console.CursorLeft - 1]);
                }
            }
        }

        public static bool FuzzySearch(string pat, string text)
        {
            string pattern = "";
            int i = 0;
            for (int j = 0; j < text.Length && i < pat.Length; j++)
            {
                if (text[j] == pat[i])
                {
                    pattern += text[j];
                    i++;
                }
            }

            return pat == pattern;
        }

        public static void PrintBorder(bool vertical, StringBuilder finder)
        {
            finder.Append("\x1b" + "(0");
            finder.Append(ESC + "31m");
            if (vertical)
            {
                finder.Append("x");
            }
            else
            {
                finder.Append("q");
            }
            finder.Append(ESC + "0m");
            finder.Append("\x1b" + "(B");
        }
    }
}
