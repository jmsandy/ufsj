using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Net.Sockets;

namespace NetworkBasics.Sockets
{
    /// <summary>
    /// Socket client for the communication with server.
    /// </summary>
    /// <Author>Jose Mauro da Silva Sandy</Author>
    /// <Date>10/11/2017</Date>
    public class SocketClient : SocketBase
    {
        /// <summary>
        /// Controls the waiting for the data to be received.
        /// </summary>
        private ManualResetEvent _receiveDone = new ManualResetEvent(false);

        /// <summary>
        /// Creates the socket by endpoint.
        /// </summary>
        /// <param name="endPoint">endpoint</param>
        public SocketClient(IPEndPoint endPoint)
        {
            _endPoint = endPoint;
        }

        /// <summary>
        /// Creates the socket by ip address and port.
        /// </summary>
        /// <param name="address">ip address.</param>
        /// <param name="port">port.</param>
        public SocketClient(IPAddress address, UInt16 port)
            : this(new IPEndPoint(address, port))
        {
        }

        /// <summary>
        /// Creates the socket by string adress and port.
        /// </summary>
        /// <param name="address">string address.</param>
        /// <param name="port">port.</param>
        public SocketClient(string address, UInt16 port)
            : this(new IPEndPoint(Dns.GetHostEntry(address).AddressList[0], port))
        {
        }

        /// <summary>
        /// Destructor.
        /// </summary>
        ~SocketClient()
        {
            Dispose(false);
        }

        /// <summary>
        /// Connects with server socket.
        /// </summary>
        public void Connect()
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socket.Connect(_endPoint);
        }

        /// <summary>
        /// Receives the data from server.
        /// </summary>
        /// <returns></returns>
        public Stream ReceiveData()
        {
            var state = new StateObject()
            {
                Socket = _socket
            };
            _socket.BeginReceive(state.Buffer, 0, StateObject.BUFFER_SIZE, SocketFlags.None, new AsyncCallback(OnReceiveCallback), state);
            _receiveDone.WaitOne();

            return state.Stream;
        }

        /// <summary>
        /// Sends data to server.
        /// </summary>
        /// <param name="data">data to send.</param>
        public void SendData(string data)
        {
            var buffer = Encoding.ASCII.GetBytes(data);
            _socket.Send(buffer);
        }

        /// <summary>
        /// Callback to control the receiving data from server.
        /// </summary>
        /// <param name="ar"></param>
        private void OnReceiveCallback(IAsyncResult ar)
        {
            var state = (StateObject)ar.AsyncState;
            Socket socket = state.Socket;

            SocketError errorCode;
            int read = socket.EndReceive(ar, out errorCode);
            if (errorCode != SocketError.Success)
            {
                read = 0;
            }

            if (read > 0)
            {
                state.Stream.Write(state.Buffer, 0, read);
                state.Stream.Flush();
                if (read == StateObject.BUFFER_SIZE)
                {
                    Thread.Sleep(100);
                    socket.BeginReceive(state.Buffer, 0, StateObject.BUFFER_SIZE, SocketFlags.None, new AsyncCallback(OnReceiveCallback), state);
                }
                else
                {
                    _receiveDone.Set();
                }
            }
            else
            {
                _receiveDone.Set();
            }
        }
    }
}
