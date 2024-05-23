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
                Console.Write(Path.GetRelativePath(Directory.GetCurrentDirectory(), Path.GetFullPath(files[i])));
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

                if (Finder.match.Length > 0)
                {
                    Console.Write(' ');
                    string remainingText = Finder.match.Substring(Math.Min(Finder.match.Length, Console.CursorLeft - 1));
                    Finder.match = Finder.match.Length > 0 ? Finder.match.Remove(Console.CursorLeft - 2, 1) : Finder.match;
                    Console.CursorLeft--;
                    int prevPos = Console.CursorLeft;
                    Console.Write(new string(' ', remainingText.Length + 2));
                    Console.CursorLeft = prevPos;
                    Console.Write(remainingText);
                    Console.CursorLeft = prevPos;
                    filteredFiles = FilterFiles();
                    Finder.currentIndex = 0;
                    Finder.startIndex = 0;
                    Finder.endIndex = Math.Min(Console.WindowHeight - 6, filteredFiles.Length);
                }

                return;
            }

            if (ch.Key == ConsoleKey.UpArrow || ch.Key == ConsoleKey.DownArrow || ch.Key == ConsoleKey.Enter)
            {
                return;
            }

            if (ch.Key == ConsoleKey.LeftArrow)
            {
                if (Console.CursorLeft > 1)
                {
                    Console.CursorLeft--;
                    
                }

                return;
            }

            if (ch.Key == ConsoleKey.RightArrow)
            {
                if (Console.CursorLeft < Finder.match.Length + 1)
                {
                    Console.CursorLeft++;

                }
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
            PrintFiles(filteredFiles.Select(f => Path.GetRelativePath(Directory.GetCurrentDirectory(), Path.GetFullPath(f))).ToArray());
        }

        private static string[] FilterFiles()
        {
            string[] caseInsensitiveFiles = files.Where(file => Finder.FuzzySearch(Finder.match, Path.GetRelativePath(Directory.GetCurrentDirectory(), Path.GetFullPath(file)))).ToArray();

            if (caseInsensitiveFiles.Length > 0)
            {
                caseInsensitive = true;
                return caseInsensitiveFiles;
            }

            caseInsensitive = false;
            return files.Where(file => Finder.FuzzySearch(Finder.match.ToLower(), Path.GetRelativePath(Directory.GetCurrentDirectory(), Path.GetFullPath(file)).ToLower())).ToArray();
        }
    }
}