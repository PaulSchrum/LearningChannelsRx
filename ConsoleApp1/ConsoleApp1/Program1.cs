using System;
using System.IO;
using System.IO.Pipes;
using System.Diagnostics;

namespace CA1
{
    class Program1
    {
        static void Main()
        {
            Process pipeClient = new Process();

            pipeClient.StartInfo.FileName = @"E:\SourceModules\CSharp\OtherMinor\LearningChannels" +
                @"Rx\ConsoleApp1\ConsoleApp2\bin\Debug\net6.0\ConsoleApp2.exe";

            using (NamedPipeServerStream pipeServer =
                new NamedPipeServerStream("abc_named_pipe", PipeDirection.InOut))
            {
                Console.WriteLine("S: Named Pipe establihed.");

                pipeClient.StartInfo.FileName = @"E:\SourceModules\CSharp\OtherMinor\LearningChannels" +
                    @"Rx\ConsoleApp1\ConsoleApp2\bin\Debug\net6.0\ConsoleApp2.exe";

                // Pass the client process a handle to the server.
                //pipeClient.StartInfo.UseShellExecute = false;
                //pipeClient.StartInfo.CreateNoWindow = false;
                pipeClient.Start();

                Console.WriteLine("S: Client started; Awaiting connection.");
                pipeServer.WaitForConnection();
                Console.WriteLine("S: Connected.");


                try
                {
                    using (StreamWriter sw = new StreamWriter(pipeServer))
                    {
                        Thread.Sleep(500);
                        sw.AutoFlush = true;
                        Console.WriteLine("S: Enter text: ");
                        sw.WriteLine(Console.ReadLine());
                    }
                }


                // Catch the IOException that is raised if the pipe is broken
                // or disconnected.
                catch (IOException e)
                {
                    Console.WriteLine("[SERVER] Error: {0}", e.Message);
                }
            }

            pipeClient.WaitForExit();
            pipeClient.Close();
            Console.WriteLine("[SERVER] Client quit. Server terminating.");
        }
    }
}




