using System;
using System.IO;
using NetworkBasics.Sockets;
using NetworkBasics.SimpleHttp;

namespace NetworkBasics.PageDownloader
{
    /// <summary>
    /// Web Page downloader.
    /// </summary>
    /// <Author>Jose Mauro da Silva Sandy</Author>
    /// <Date>10/11/2017</Date>
    class Program
    {
        private static readonly string GET_COMMAND = "GET {0} HTTP/1.1\r\nHost: {1}\r\n\r\n\r\n";

        static int Main(string[] args)
        {
            try
            {
                if (args == null || args.Length == 0)
                {
                    throw new Exception("Invalid Url.");                    
                }

                var url = args[0];
                var uri = new Uri(url.StartsWith("http://") || url.StartsWith("https://") ? url : "http://" + url);

                var port = 80;
                if (args.Length > 1)
                {
                    if (!Int32.TryParse(args[1], out port) || port <= 0)
                    {
                        throw new Exception("Invalid Port.");
                    }
                }

                Console.WriteLine("Establishing connection with the address: {0}:{1}", uri.Host, port);
                using (var socketClient = new SocketClient(uri.Host, (ushort)port))
                {
                    socketClient.Connect();

                    Console.WriteLine("Request: {0}", string.Format(GET_COMMAND, uri.AbsolutePath, uri.Host).Trim());
                    socketClient.SendData(string.Format(GET_COMMAND, uri.AbsolutePath, uri.Host));

                    Console.WriteLine("Waiting data...");
                    var data = socketClient.ReceiveData();

                    Console.WriteLine("Processing response...");
                    var response = new HttpResponse();                    
                    response.Parser(data);

                    Console.WriteLine("Debugging Response Parameters");
                    foreach (var option in response.Attributes) 
                    {
                        Console.WriteLine("\t{0}: {1}", option.Key, option.Value);
                    }

                    if ("200".Equals(response.GetAttributeValue(HttpResponseHeaderOptions.StatusCode)))
                    {                        
                        var filename = Path.GetFileName(uri.AbsolutePath);
                        Console.WriteLine("\nSaving file: {0}", (string.IsNullOrEmpty(Path.GetExtension(filename)) ? Path.Combine(filename, ".html") : filename));

                        using (var fs = new FileStream((string.IsNullOrEmpty(Path.GetExtension(filename)) ? Path.Combine(filename, ".html") : filename), FileMode.Create))
                        {
                            response.Body.Seek(0, SeekOrigin.Begin);
                            response.Body.CopyTo(fs);
                            fs.Flush();
                            fs.Close();
                        }
                    }
                    else
                    {
                        Console.WriteLine("{0} {1}", response.GetAttributeValue(HttpResponseHeaderOptions.StatusCode), 
                            response.GetAttributeValue(HttpResponseHeaderOptions.StatusMessage));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return 0;
        }
    }
}
