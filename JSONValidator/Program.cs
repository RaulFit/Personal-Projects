using Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

class JSONValidator
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            return;
        }

        string filePath = args[0];

        if (!File.Exists(filePath))
        {
            Console.WriteLine("Path does not exist");
            return;
        }

        string fileContent = File.ReadAllText(filePath);

        var value = new Value();

        if (value.Match(fileContent).Success())
        {
            Console.WriteLine("Valid JSON");
            return;
        }

        Console.WriteLine("Invalid JSON");
    }
}
