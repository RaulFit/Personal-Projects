using Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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

            string[] lines = fileContent.Split('\n');

            var validLine = new ValidLine();

            var validName = new ValidName();

            var validObject = new ValidObject();

            var parentheses = new OneOrMore(new Any("[{"));

            int errorLine = 0;
           
            for(int i = 1; i < lines.Length; i++)
            {
                var line = validLine.Match(lines[i]);
                if (line.Success())
                {
                    continue;
                }

                line = validObject.Match(lines[i]);
                if(line.Success())
                {
                    continue;
                }

                line = parentheses.Match(lines[i]);
                if(line.Success())
                {
                    continue;
                }

                errorLine = i + 1;
                break;
            }

            Console.WriteLine($"Error on line {errorLine}");
        }
    }
}


    