using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace NetworkBasics.Sockets
{
    /// <summary>
    /// Socket server.
    /// </summary>
    /// <Author>Jose Mauro da Silva Sandy</Author>
    /// <Date>10/12/2017</Date>
    public class SocketServer : SocketBase
    {
        /// <summary>
        /// Event handler from data received.
        /// </summary>
        public event EventHandler DataReceived;

        /// <summary>
        /// Creates the server socket.
        /// </summary>
        /// <param name="port">listening port.</param>
        public SocketServer(UInt16 port)
        {
            _endPoint = new IPEndPoint(Dns.GetHostEntry(Dns.GetHostName()).AddressList[1], port);
        }

        /// <summary>
        /// Destructor.
        /// </summary>
        ~SocketServer()
        {
            Dispose(false);
        }

        /// <summary>
        /// Opens the socket to connection.
        /// </summary>
        /// <param name="maximumPendingConnections">maximum of pending connections.</param>
        /// <returns>task representing the socket listening.</returns>
        public Task Accept(int maximumPendingConnections = 100)
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socket.Bind(_endPoint);
            _socket.Listen(maximumPendingConnections);

            var task = new Task(() =>
            {
                while (true)
                {
                    var socketClient = _socket.Accept();

                    var state = new StateObject()
                    {
                        Socket = socketClient
                    };
                    socketClient.BeginReceive(state.Buffer, 0, StateObject.BUFFER_SIZE, SocketFlags.None, new AsyncCallback(OnReceiveCallback), state);
                }
            });
            task.Start();
            return task;
        }

        /// <summary>
        /// Callback to control the receiving data from client.
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
                var teste = Encoding.ASCII.GetString(state.Buffer, 0, read);
                state.Stream.Write(state.Buffer, 0, read);
                state.Stream.Flush();
                if (read == StateObject.BUFFER_SIZE)
                {
                    Thread.Sleep(1000);
                    socket.BeginReceive(state.Buffer, 0, StateObject.BUFFER_SIZE, SocketFlags.None, new AsyncCallback(OnReceiveCallback), state);
                }
                else
                {
                    OnDataReceived(state, EventArgs.Empty);
                }
            }
            else
            {
                OnDataReceived(state, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Fires the event handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnDataReceived(StateObject sender, EventArgs e)
        {
            EventHandler handler = DataReceived;
            if (handler != null)
            {
                handler(sender, e);
            }
        }
    }
}
