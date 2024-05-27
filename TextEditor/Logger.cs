using System.Configuration;

namespace TextEditor
{
    public static class Logger
    {
        public static void WriteLog(string messsage)
        {
            string logPath = ConfigurationManager.AppSettings["logPath"];

            using (StreamWriter writer = new StreamWriter(logPath, true))
            {
                writer.WriteLine($"{DateTime.Now} : {messsage}");
            }
        }
    }
}
