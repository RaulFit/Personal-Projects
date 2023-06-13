using Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Json
{
    class JSONValidator
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("The specified path does not exist. Please provide a correct path");
                return;
            }

            string filePath = args[0];

            if (!File.Exists(filePath))
            {
                Console.WriteLine("The specified path is incorrect or does not exist. Please provide a correct path");
                return;
            }

            string fileContent = File.ReadAllText(filePath);

            var value = new Value();

            var validJson = value.Match(fileContent);

            if (validJson.Success() && string.IsNullOrEmpty(validJson.RemainingText()))
            {
                Console.WriteLine("Valid JSON");
                return;
            }

            Console.WriteLine("Invalid JSON");

            string[] lines = validJson.RemainingText().Split('\n');
            int errorLine = 0;
            int errorColumn = 0;

            for (int i = 1; i < lines.Length; i++)
            {
                var lineResult = value.Match(lines[i]);
                if (!lineResult.Success())
                {
                    errorLine = i + 1;
                    errorColumn = lines[i].IndexOf(lineResult.RemainingText()) + 1;
                    break;
                }
            }

            Console.WriteLine($"Error on line {errorLine}, column {errorColumn}");
        }
    }
}
