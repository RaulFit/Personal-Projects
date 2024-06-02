using System.Configuration;
using System.IO;

namespace TextEditor
{
    static class MainProgram
    {
        static void Main(string[] args)
        {
            bool lineNumbers = Convert.ToBoolean(ConfigurationManager.AppSettings.Get("lineNumbers"));
            bool relativeLines = Convert.ToBoolean(ConfigurationManager.AppSettings.Get("relativeLines"));
            Navigator navigator;

            if (args.Length > 0 && Path.Exists(args[0]))
            {
                Drawer drawer = new Drawer(lineNumbers, relativeLines);
                string path = Path.GetFullPath(args[0]);
                navigator = new Navigator(File.ReadAllLines(path).ToList(), drawer, new CommandMode(path));
                navigator.RunNavigator();
            }

            navigator = new Navigator(new List<string>() { "" }, new Drawer(lineNumbers, relativeLines), new CommandMode(""));
            navigator.RunNavigator();
        }
    }
}