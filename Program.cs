using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace QuickDiff
{
    /// <summary>
    /// QuickDiff by Rod Howarth (http://www.rodhowarth.com)
    /// Used to verify that two files are the same
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("You must supply the results file name and expected results file name");
                return;
            }

            string input = args[0];
            string output = args[1];

            using (StreamReader inputReader = new StreamReader(input))
            {
                using (StreamReader outputReader = new StreamReader(output))
                {
                    int differentLine = -1;

                    int currentLine = 0;
                    while (!inputReader.EndOfStream)
                    {
                        currentLine++;
                        string inputLine = inputReader.ReadLine();

                        if (outputReader.EndOfStream || outputReader.ReadLine() != inputLine)
                        {
                            differentLine = currentLine;
                            break;
                        }
                    }

                    //check back the other way
                    currentLine = 0;
                    while (!outputReader.EndOfStream && differentLine == -1)
                    {
                        currentLine++;
                        string inputLine = outputReader.ReadLine();

                        if (inputReader.EndOfStream || inputReader.ReadLine() != inputLine)
                        {
                            differentLine = currentLine;
                            break;
                        }
                    }

                    if (differentLine == -1)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;

                        Console.WriteLine("------SUCCESS!-----");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Input is different to output. First instance is at line " + differentLine);
                    }

                    Console.ResetColor();
                }
            }
        }
    }
}
