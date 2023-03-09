using System;
using System.Diagnostics;
using System.IO.Pipes;

namespace CA2
{
    class Program2
    {
        static void Main(string[] args)
        {
            using (NamedPipeClientStream pipeClient =
                new NamedPipeClientStream(".", "abc_named_pipe", PipeDirection.InOut))
            {

                // Connect to the pipe or wait until the pipe is available.
                Console.Write("     C: Attempting to connect to pipe...");
                pipeClient.Connect();

                Console.WriteLine("     C: Connected to pipe.");
                Console.WriteLine($"     C: There are currently {pipeClient.NumberOfServerInstances}" +
                    " pipe server instances open.");
                using (StreamReader sr = new StreamReader(pipeClient))
                {
                    // Display the read text to the console
                    string temp;
                    while ((temp = sr.ReadLine()) != null)
                    {
                        Console.WriteLine("     C: Received from server: {0}", temp);
                    }
                }
            }
            Console.Write("     C: Press Enter to continue...");
            Console.ReadLine();
        }
    }
}



