using System;
using System.IO;
using System.Text;
using NetworkBasics.Extensions;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NetworkBasics.SimpleHttp
{
    /// <summary>
    /// Html Response.
    /// </summary>
    /// <Author>Jose Mauro da Silva Sandy</Author>
    /// <Date>10/11/2017</Date>
    public sealed class HttpResponse
    {
        /// <summary>
        /// Received html.
        /// </summary>
        private Stream _data;

        /// <summary>
        /// Header Attributes.
        /// </summary>
        public Dictionary<HttpResponseHeaderOptions, string> Attributes { get; private set; }

        /// <summary>
        /// Response body.
        /// </summary>
        public Stream Body { get; private set; }

        /// <summary>
        /// Default Constructor.
        /// </summary>
        public HttpResponse()
        {
            Body = new MemoryStream();
            Attributes = new Dictionary<HttpResponseHeaderOptions, string>();
        }

        /// <summary>
        /// Gets the attribute value associated with the received type.
        /// </summary>
        /// <param name="option">attribute type.</param>
        /// <returns>attribute value.</returns>
        public string GetAttributeValue(HttpResponseHeaderOptions option)
        {
            return Attributes.ContainsKey(option) ? Attributes[option] : null;
        }

        /// <summary>
        /// Realizes the parser from received html.
        /// </summary>
        /// <param name="data">received html.</param>
        public void Parser(Stream data)
        {
            _data = data;
            var parser = new HttpParser();
            Attributes = parser.ExtractParameters<HttpResponseHeaderOptions>(data, false);
            Body = parser.Body;
        }

        /// <summary>
        /// Converts the attributes to string representation.
        /// </summary>
        /// <returns>string representation for the header attributes.</returns>
        public string HeaderToString()
        {
            var content = new StringBuilder();
            content.AppendFormat("HTTP/1.1 {0}\r\n", Attributes[HttpResponseHeaderOptions.StatusCode]);

            foreach (var parameter in Attributes)
            {
                content.AppendFormat("{0}: {1}\r\n", parameter.Key.ToDescription(), parameter.Value);
            }

            content.Append("\r\n\r\n");
            return content.ToString();
        }
    }
}