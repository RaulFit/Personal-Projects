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

            var invalidValue = new InvalidValue();

            var match = invalidValue.Match(fileContent);

            int errorLine = 0;

            for (int i = 0; i < fileContent.IndexOf(match.ModifiedText()); i++)
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


    