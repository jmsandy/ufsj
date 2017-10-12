using System;
using System.IO;
using System.Net;
using System.Text;
using System.Net.Sockets;

namespace NetworkBasics.Sockets
{
    /// <summary>
    /// Object communication.
    /// </summary>
    /// <Author>Jose Mauro da Silva Sandy</Author>
    /// <Date>10/11/2017</Date>
    public class StateObject
    {
        /// <summary>
        /// Buffer size.
        /// </summary>
        public const int BUFFER_SIZE = 4096;

        /// <summary>
        /// Content.
        /// </summary>
        public MemoryStream Stream { get; private set; }

        /// <summary>
        /// Socket associated with communication.
        /// </summary>
        public Socket Socket { get; set; }

        /// <summary>
        /// Communication buffer.
        /// </summary>
        public byte[] Buffer { get; private set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public StateObject()
        {
            Stream = new MemoryStream();
            Buffer = new byte[BUFFER_SIZE];
        }
    }
}
