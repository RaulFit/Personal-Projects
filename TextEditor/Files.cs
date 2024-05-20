namespace TextEditor
{
    static class Files
    {
        public static string[] files = { };
        public static string[] filteredFiles = { };
        public static bool caseInsensitive = false;

        public static void PrintFiles(string[] files)
        {
            for (int i = Finder.startIndex; i < Finder.endIndex; i++)
            {
                Console.Write(files[i]);
                Console.SetCursorPosition(1, Console.CursorTop - 1);
            }

            int filesLength = new string(filteredFiles.Length.ToString()).Length;
            Console.SetCursorPosition(Console.WindowWidth - 10, Console.WindowHeight - 2);
            Console.Write(new string(' ', 9));
            Console.SetCursorPosition(Console.WindowWidth - 6 - filesLength, Console.WindowHeight - 2);
            Console.Write($"{filteredFiles.Length} / {Files.files.Length}");
            Console.SetCursorPosition(Finder.match.Length + 1, Console.WindowHeight - 2);
        }

        public static void SelectFile(ConsoleKeyInfo ch, string[] files)
        {
            int top = Console.CursorTop;
            if (ch.Key == ConsoleKey.UpArrow)
            {
                if (top == 1 && Finder.endIndex < files.Length - 1 && Finder.currentIndex < files.Length - 1)
                {
                    Finder.startIndex++;
                    Finder.endIndex++;
                    RefreshFiles();
                    Finder.ColorMatchingLetters();
                }

                if (Finder.currentIndex < filteredFiles.Length - 1)
                {
                    Finder.currentIndex++;
                    Console.SetCursorPosition(1, Math.Max(1, top - 1));
                }
            }

            if (ch.Key == ConsoleKey.DownArrow)
            {
                if (top == Console.WindowHeight - 6 && Finder.startIndex > 0)
                {

                    Finder.startIndex--;
                    Finder.endIndex--;
                    RefreshFiles();
                    Finder.ColorMatchingLetters();
                }

                if (Finder.currentIndex > 0)
                {
                    Finder.currentIndex--;
                }

                Console.SetCursorPosition(1, Math.Min(Console.WindowHeight - 6, top + 1));
            }
        }

        public static void SearchFile(ConsoleKeyInfo ch)
        {
            if (ch.Key == ConsoleKey.Backspace)
            {
                if (Console.CursorLeft == 0)
                {
                    Console.CursorLeft++;
                }
                Console.Write(' ');
                Finder.match = Finder.match.Length > 0 ? Finder.match.Remove(Finder.match.Length - 1, 1) : Finder.match;
                filteredFiles = FilterFiles();
                Finder.currentIndex = 0;
                Finder.startIndex = 0;
                Finder.endIndex = Math.Min(Console.WindowHeight - 6, filteredFiles.Length);
                return;
            }

            if (ch.Key == ConsoleKey.UpArrow)
            {
                return;
            }

            Finder.match += ch.KeyChar.ToString();
            filteredFiles = FilterFiles();
            Finder.currentIndex = 0;
            Finder.startIndex = 0;
            Finder.endIndex = Math.Min(Console.WindowHeight - 6, filteredFiles.Length);
        }

        public static void RefreshFiles()
        {
            Console.SetCursorPosition(1, Console.WindowHeight - 6);
            for (int i = 0; i < Console.WindowHeight - 6; i++)
            {
                Console.Write(new string(' ', Console.WindowWidth - 2));
                Console.SetCursorPosition(1, Console.CursorTop - 1);
            }

            Console.SetCursorPosition(1, Console.WindowHeight - 6);
            PrintFiles(filteredFiles.Select(f => Path.GetFileName(f)).ToArray());
        }

        private static string[] FilterFiles()
        {
            string[] caseInsensitiveFiles = files.Where(file => Finder.FuzzySearch(Finder.match, Path.GetFileName(file))).ToArray();

            if (caseInsensitiveFiles.Length > 0)
            {
                caseInsensitive = true;
                return caseInsensitiveFiles;
            }

            caseInsensitive = false;
            return files.Where(file => Finder.FuzzySearch(Finder.match.ToLower(), Path.GetFileName(file).ToLower())).ToArray();
        }
    }
}
