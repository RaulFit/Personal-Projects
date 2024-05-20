using System.Configuration;

namespace TextEditor
{
    static class Finder
    {
        const string ESC = "\x1b[";
        public static string match = "";
        public static int startIndex = 0;
        public static int endIndex = 0;
        public static int currentIndex = 0;


        public static void OpenFinder()
        {
            Console.Clear();
            DrawFinder();
            while (true)
            {
                var ch = Console.ReadKey();
                Files.SearchFile(ch);

                if (ch.Key == ConsoleKey.UpArrow)
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

                    Navigator.text = File.ReadAllLines(Path.GetFullPath(Files.filteredFiles.ElementAt(currentIndex)));
                    Drawer.lineNumbers = Convert.ToBoolean(ConfigurationManager.AppSettings.Get("lineNumbers"));
                    Drawer.relativeLines = Convert.ToBoolean(ConfigurationManager.AppSettings.Get("relativeLines"));
                    Navigator.RunNavigator();
                }

                Console.Write($"{ESC}?25l");
                Files.RefreshFiles();
                ColorMatchingLetters();
                Console.Write($"{ESC}?25h");
            }
        }

        private static void DrawFinder()
        {
            Console.SetCursorPosition(0, 0);
            PrintVerticalBorders(Console.WindowHeight - 4);
            Console.SetCursorPosition(0, 0);
            PrintHorizontalBorder(Console.WindowWidth / 2 - 4);
            Console.Write("Results");
            PrintHorizontalBorder(Console.WindowWidth / 2 - 3);
            Console.SetCursorPosition(0, Console.WindowHeight - 5);
            PrintHorizontalBorder(Console.WindowWidth);
            Console.SetCursorPosition(0, Console.WindowHeight - 3);
            PrintHorizontalBorder(Console.WindowWidth / 2 - 5);
            Console.Write("Find Files");
            PrintHorizontalBorder(Console.WindowWidth / 2 - 5);
            Console.SetCursorPosition(0, Console.CursorTop + 1);
            PrintVerticalBorders(1);
            PrintHorizontalBorder(Console.WindowWidth);
            Console.SetCursorPosition(1, Console.WindowHeight - 6);
            Files.files = Directory.GetFiles(Environment.CurrentDirectory, "*.*", SearchOption.AllDirectories).Select(Path.GetFullPath).ToArray();
            Files.filteredFiles = new string[Files.files.Length];
            Files.files.CopyTo(Files.filteredFiles, 0);
            endIndex = Math.Min(Files.files.Length - 1, Console.WindowHeight - 6);
            Files.PrintFiles(Files.filteredFiles.Select(f => Path.GetFileName(f)).ToArray());
            DrawCorners();
            Console.SetCursorPosition(1, Console.WindowHeight - 2);
        }

        private static void DrawCorners()
        {
            Console.Write("\x1b" + "(0");
            Console.Write(ESC + "31m");
            Console.SetCursorPosition(0, 0);
            Console.Write("l");
            Console.SetCursorPosition(Console.WindowWidth - 1, 0);
            Console.Write("k");
            Console.SetCursorPosition(0, Console.WindowHeight - 5);
            Console.Write("m");
            Console.SetCursorPosition(Console.WindowWidth - 1, Console.WindowHeight - 5);
            Console.Write("j");
            Console.SetCursorPosition(0, Console.WindowHeight - 3);
            Console.Write("l");
            Console.SetCursorPosition(Console.WindowWidth - 1, Console.WindowHeight - 3);
            Console.Write("k");
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            Console.Write("m");
            Console.SetCursorPosition(Console.WindowWidth - 1, Console.WindowHeight - 1);
            Console.Write("j");
            Console.Write(ESC + "0m");
            Console.Write("\x1b" + "(B");
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
            string fileName = Path.GetFileName(file);
            string lowerFileName = fileName.ToLower();
            string lowerMatch = match.ToLower();

            if (lowerFileName.Contains(lowerMatch))
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
            while (i < pat.Length)
            {
                for (int j = 0; j < text.Length && i < pat.Length; j++)
                {
                    if (text[j] == pat[i])
                    {
                        pattern += text[j];
                        i++;
                    }
                }

                break;
            }

            return pat == pattern;
        }

        private static void PrintVerticalBorders(int length)
        {
            for (int i = 0; i < length; i++)
            {
                PrintBorder(true);
                Console.CursorLeft = Console.WindowWidth - 1;
                PrintBorder(true);
                Console.SetCursorPosition(0, Console.CursorTop + 1);
            }
        }

        private static void PrintHorizontalBorder(int length)
        {
            for (int i = 0; i < length; i++)
            {
                PrintBorder(false);
            }
        }

        private static void PrintBorder(bool vertical)
        {
            Console.Write("\x1b" + "(0");
            Console.Write(ESC + "31m");
            if (vertical)
            {
                Console.Write("x");
            }
            else
            {
                Console.Write("q");
            }
            Console.Write(ESC + "0m");
            Console.Write("\x1b" + "(B");
        }
    }
}
