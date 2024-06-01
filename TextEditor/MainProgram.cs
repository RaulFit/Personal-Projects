using System.Configuration;

namespace TextEditor
{
    static class MainProgram
    {
        static void Main(string[] args)
        {
            if (args.Length > 0 && Path.Exists(args[0]))
            {
                bool lineNumbers = Convert.ToBoolean(ConfigurationManager.AppSettings.Get("lineNumbers"));
                bool relativeLines = Convert.ToBoolean(ConfigurationManager.AppSettings.Get("relativeLines"));
                Drawer drawer = new Drawer(lineNumbers, relativeLines);
                string path = Path.GetFullPath(args[0]);
                Navigator navigator = new Navigator(File.ReadAllLines(path).ToList(), drawer, new CommandMode(path));
                navigator.RunNavigator();
            }

            Finder.OpenFinder();
        }
    }
}