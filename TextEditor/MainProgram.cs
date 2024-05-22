using System.Configuration;

namespace TextEditor
{
    static class MainProgram
    {
        static void Main(string[] args)
        {
            if (args.Length > 0 && Path.Exists(args[0]))
            {
                Navigator.text = File.ReadAllLines(Path.GetFullPath(args[0]));
                Drawer.lineNumbers = Convert.ToBoolean(ConfigurationManager.AppSettings.Get("lineNumbers"));
                Drawer.relativeLines = Convert.ToBoolean(ConfigurationManager.AppSettings.Get("relativeLines"));
                Navigator.RunNavigator();
            }

            Finder.OpenFinder();
        }
    }
}