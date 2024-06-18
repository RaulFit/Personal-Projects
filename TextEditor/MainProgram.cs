using System.Configuration;

namespace TextEditor
{
    static class MainProgram
    {
        static void Main(string[] args)
        {
            bool lineNumbers = Convert.ToBoolean(ConfigurationManager.AppSettings.Get("lineNumbers"));
            bool relativeLines = Convert.ToBoolean(ConfigurationManager.AppSettings.Get("relativeLines"));
            bool mustSave = Convert.ToBoolean(ConfigurationManager.AppSettings.Get("mustSave"));
            Navigator navigator;

            if (args.Length > 0 && Path.Exists(args[0]))
            {
                Drawer drawer = new Drawer(lineNumbers, relativeLines, mustSave);
                string path = Path.GetFullPath(args[0]);
                navigator = new Navigator(File.ReadAllLines(path).ToList(), drawer, new CommandMode(path));
                navigator.RunNavigator(false);
            }

            navigator = new Navigator(new List<string>() { "" }, new Drawer(lineNumbers, relativeLines, mustSave), new CommandMode(""));
            navigator.RunNavigator(false);
        }
    }
}
