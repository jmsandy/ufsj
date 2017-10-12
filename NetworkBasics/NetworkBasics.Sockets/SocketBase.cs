using System;
using System.Net;
using System.Net.Sockets;

namespace NetworkBasics.Sockets
{
    /// <summary>
    /// Socket base for the socket's manipulation.
    /// </summary>
    /// <Author>Jose Mauro da Silva Sandy</Author>
    /// <Date>10/12/2017</Date>
    public abstract class SocketBase : IDisposable
    {
        /// <summary>
        /// Socket in handling.
        /// </summary>
        protected Socket _socket;

        /// <summary>
        /// Socket address.
        /// </summary>
        protected IPEndPoint _endPoint;

        /// <summary>
        /// Closes the socket.
        /// </summary>
        public void Close()
        {
            if (_socket != null)
            {
                if (_socket.Connected)
                {
                    _socket.Shutdown(SocketShutdown.Both);
                    _socket.Close();
                }
            }
        }

        /// <summary>
        /// Disposes the resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the resources.
        /// </summary>
        /// <param name="disposing"></param>
        public virtual void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    Close();
                }
            }
            catch { }
        }
    }
}
