using System.Configuration;

namespace TextEditor
{
    public static class Logger
    {
        public static void WriteLog(string messsage)
        {
            string logPath = Directory.GetCurrentDirectory() + "log.txt";

            using (StreamWriter writer = new StreamWriter(logPath, true))
            {

                writer.WriteLine($"{DateTime.Now} : {messsage}");
            }
        }
    }
}
