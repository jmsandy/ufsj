using System;
using System.IO;
using System.Collections.Generic;

namespace NetworkBasics.SimpleHttp
{
    /// <summary>
    /// Html Request.
    /// </summary>
    /// <Author>Jose Mauro da Silva Sandy</Author>
    /// <Date>10/12/2017</Date>
    public class HttpRequest
    {
        /// <summary>
        /// Received html.
        /// </summary>
        private Stream _data;

        /// <summary>
        /// Header Attributes.
        /// </summary>
        public Dictionary<HttpRequestHeaderOptions, string> Attributes { get; private set; }

        /// <summary>
        /// Default Constructor.
        /// </summary>
        public HttpRequest()
        {
            Attributes = new Dictionary<HttpRequestHeaderOptions, string>();
        }

        /// <summary>
        /// Gets the attribute value associated with the received type.
        /// </summary>
        /// <param name="option">attribute type.</param>
        /// <returns>attribute value.</returns>
        public string GetAttributeValue(HttpRequestHeaderOptions option)
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
            Attributes = parser.ExtractParameters<HttpRequestHeaderOptions>(data, true);
        }
    }
}
