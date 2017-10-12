using System;
using System.IO;
using NetworkBasics.Sockets;

namespace NetworkBasics.WebServer
{
    /// <summary>
    /// Simple Web Server.
    /// </summary>
    /// <Author>Jose Mauro da Silva Sandy</Author>
    /// <Date>10/12/2017</Date>
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args == null || args.Length != 2)
                {
                    throw new Exception("Please enter the directory and port for sharing.");
                }

                var directory = args[0];
                if (!Directory.Exists(directory))
                {
                    throw new Exception("Directory not exits.");
                }

                var port = 0;
                if (!Int32.TryParse(args[1], out port) || port <= 0)
                {
                    throw new Exception("Invalid Port.");
                }

                using (var socket = new SocketWebServer(directory, (ushort)port))
                {
                    var task = socket.Start();
                    task.Wait();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
