using Json;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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

            int errorLine = 0;

            var ws = new Many(new Any(" \n\r\t"));

            var objectValues = new ObjectValues();

            var objectBegining = new ObjectBegining();

            var comma = new Sequence(ws, new Character(','), ws);

            fileContent = fileContent.TrimStart();

            fileContent = fileContent.TrimEnd();

            fileContent = fileContent.Remove(0, 1);

            fileContent = fileContent.Remove(fileContent.Length - 1);

            IMatch match = new Match(true, fileContent);

            while (match.Success())
            {
                match = objectValues.Match(match.RemainingText());

                match = comma.Match(match.RemainingText());

                match = objectBegining.Match(match.RemainingText());


            }

            for(int i = 0; i < fileContent.IndexOf(match.RemainingText()); i++)
            {
                if (fileContent[i] == '\n')
                {
                    errorLine++;
                }
            }

            Console.WriteLine($"Error on line {errorLine + 1}");
        }
    }
}


    