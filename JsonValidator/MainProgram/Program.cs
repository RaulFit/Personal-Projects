using Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace JsonValidator.MainProgram
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

            var value = new ValueValidator();

            var validJson = value.Match(fileContent);

            if (validJson.Success() && string.IsNullOrEmpty(validJson.RemainingText()))
            {
                Console.WriteLine("Valid JSON");
                return;
            }

            Console.WriteLine("Invalid JSON");

            int errorLine = 0;

            int errorColumn = 0;

            for (int i = 0; i < fileContent.IndexOf(validJson.ModifiedText()); i++)
            {
                errorColumn++;
                if (fileContent[i] == '\n')
                {
                    errorLine++;
                    errorColumn = 0;
                }
            }

            Console.WriteLine($"Error on line {errorLine + 1}, column {errorColumn}");
        }
    }
}