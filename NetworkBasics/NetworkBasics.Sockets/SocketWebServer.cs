using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using NetworkBasics.SimpleHttp;

namespace NetworkBasics.Sockets
{
    /// <summary>
    /// Socket from web server.
    /// </summary>
    /// <Author>Jose Mauro da Silva Sandy</Author>
    /// <Date>10/12/2017</Date>
    public class SocketWebServer : SocketServer
    {
        /// <summary>
        /// Virtual path of web server.
        /// </summary>
        private readonly string _virtualPath;

        /// <summary>
        /// Creates the web server in the path and port.
        /// </summary>
        /// <param name="virtualPath">virtual path.</param>
        /// <param name="port">port.</param>
        public SocketWebServer(string virtualPath, UInt16 port)
            : base(port)
        {
            _virtualPath = virtualPath;
        }

        /// <summary>
        /// Destructor.
        /// </summary>
        ~SocketWebServer()
        {
            Dispose(false);
        }

        /// <summary>
        /// Starts the web server.
        /// </summary>
        /// <returns>task the web server process.</returns>
        public Task Start()
        {
            DataReceived += SocketWebServer_DataReceived;
            return Accept();
        }

        /// <summary>
        /// Event handler to control the receiving data from client.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SocketWebServer_DataReceived(object sender, EventArgs e)
        {
            var state = sender as StateObject;
            var request = new HttpRequest();
            request.Parser(state.Stream);

            var address = request.GetAttributeValue(HttpRequestHeaderOptions.Address);
            if (address != null)
            {
                var response = new HttpResponse();

                var absolutePath = "/".Equals(address) ? _virtualPath : Path.Combine(_virtualPath, address.Substring(1));

                byte[] body = new byte[0];
                string contentType = "text/html";

                if ("".Equals(Path.GetExtension(absolutePath)))
                {
                    if (Directory.Exists(absolutePath))
                    {
                        response.Attributes.Add(HttpResponseHeaderOptions.StatusCode, "200 OK");
                        var contentBody = HtmlHelper.GetHtmlResourcesList(_virtualPath, address);
                        body = Encoding.ASCII.GetBytes(contentBody);
                    }
                    else
                    {
                        response.Attributes.Add(HttpResponseHeaderOptions.StatusCode, "404 NOT FOUND");
                        var contentBody = HtmlHelper.GetHtmlResourceNotFound(address);
                        body = Encoding.ASCII.GetBytes(contentBody);
                    }
                }
                else
                {
                    if (File.Exists(absolutePath))
                    {
                        contentType = System.Web.MimeMapping.GetMimeMapping(absolutePath);
                        response.Attributes.Add(HttpResponseHeaderOptions.StatusCode, "200 OK");
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var fs = File.Open(absolutePath, FileMode.Open))
                            {
                                fs.CopyTo(memoryStream);
                                body = memoryStream.ToArray();
                            }
                        }
                    }
                    else
                    {
                        response.Attributes.Add(HttpResponseHeaderOptions.StatusCode, "404 NOT FOUND");
                        var contentBody = HtmlHelper.GetHtmlResourceNotFound(address);
                        body = Encoding.ASCII.GetBytes(contentBody);
                    }
                }

                response.Attributes.Add(HttpResponseHeaderOptions.Server, "Simple Web Server");
                response.Attributes.Add(HttpResponseHeaderOptions.ContentType, contentType);
                response.Attributes.Add(HttpResponseHeaderOptions.ContentLength, body.Length.ToString());

                var header = response.HeaderToString();
                var contentHeader = Encoding.ASCII.GetBytes(header);

                using (var stream = new MemoryStream())
                {
                    stream.Write(contentHeader, 0, contentHeader.Length);
                    stream.Write(body, 0, body.Length);

                    // Send data.
                    state.Socket.Send(stream.ToArray());
                }
            }
        }
    }
}
