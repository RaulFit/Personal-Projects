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
        public static string fileName = "";
        public static StringBuilder finder = new StringBuilder();
        public static Files Files = new Files(Directory.GetFiles(Environment.CurrentDirectory, "*.*", SearchOption.AllDirectories).Select(Path.GetFullPath).ToList());
        public static HashSet<string> openedFiles = [];

        public static void OpenFinder(Navigator navigator, bool openFiles)
        {
            Files = openFiles ? new Files(openedFiles.ToList()) :
            new Files(Directory.GetFiles(Environment.CurrentDirectory, "*.*", SearchOption.AllDirectories).Select(Path.GetFullPath).ToList());
            Console.Clear();
            DrawFinder();
            while (true)
            {
                var ch = Console.ReadKey();
                if (ch.Key != ConsoleKey.UpArrow && ch.Key != ConsoleKey.DownArrow && ch.Key != ConsoleKey.Enter)
                {
                    Files.SearchFile(ch);
                }

                if (ch.Key == ConsoleKey.LeftArrow || ch.Key == ConsoleKey.RightArrow)
                {
                    continue;
                }

                if (ch.Key == ConsoleKey.Escape)
                {
                    navigator.RunNavigator(false);
                }

                if (ch.Key == ConsoleKey.UpArrow || ch.Key == ConsoleKey.DownArrow || ch.Key == ConsoleKey.Enter)
                {
                    SelectFile(ch);
                    if (navigator.hasChanges && navigator.Drawer.mustSave)
                    {
                        navigator.RunNavigator(true);
                    }

                    else
                    {
                        openedFiles.Add(Files.filteredFiles.ElementAt(currentIndex));
                        ReadText();
                    }
                }

                int left = Console.CursorLeft;
                Console.Write($"{ESC}?25l");
                Files.RefreshFiles();
                ColorMatchingLetters(Files.filteredFiles);
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
            bool mustSave = Convert.ToBoolean(ConfigurationManager.AppSettings.Get("mustSave"));
            Drawer drawer = new Drawer(lineNumbers, relativeLines, mustSave);
            Navigator navigator = new Navigator(File.ReadAllLines(Path.GetFullPath(Files.filteredFiles.ElementAt(currentIndex))).ToList(), drawer, commandMode);
            navigator.RunNavigator(false);
        }

        private static void SelectFile(ConsoleKeyInfo ch)
        {
            Console.SetCursorPosition(1, Console.WindowHeight - 6);
            while (ch.Key != ConsoleKey.Enter)
            {
                ch = Console.ReadKey(true);
                if (ch.Key == ConsoleKey.UpArrow || ch.Key == ConsoleKey.DownArrow)
                {
                    Files.SelectFile(ch);
                }

                else if (ch.Key != ConsoleKey.Enter)
                {
                    Console.SetCursorPosition(match.Length + 1, Console.WindowHeight - 2);
                    Console.Write(ch.KeyChar.ToString());
                    Files.SearchFile(ch);
                    Console.Write($"{ESC}?25l");
                    Files.RefreshFiles();
                    ColorMatchingLetters(Files.filteredFiles);
                    Console.Write($"{ESC}?25h");
                    Console.SetCursorPosition(1, Console.WindowHeight - 6);
                }
            }
        }

        public static void ResetSettings()
        {
            finder = new StringBuilder();
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
            endIndex = Math.Min(Files.files.Count, Console.WindowHeight - 6);
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

        public static void ColorMatchingLetters(List<string> files)
        {
            Console.SetCursorPosition(1, Console.WindowHeight - 6);
            Console.Write($"{ESC}32m");
            for (int i = startIndex; i < endIndex && Console.CursorTop > 0; i++)
            {
                ColorPattern(files[i], 0);
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
            finder = vertical ? finder.Append("x") : finder.Append("q");
            finder.Append(ESC + "0m");
            finder.Append("\x1b" + "(B");
        }
    }
}