using System.Net.Sockets;
using System.Net;
using System.Text;
using SocketServerProto; 

namespace SocketServer1
{
    public class tcp_Server
    {
        //TCP
        public static async Task RunAsync()
        {
            try
            {
                IPAddress ipadrs_ = IPAddress.Parse("127.0.0.1");
                TcpListener tcpListener = new TcpListener(ipadrs_, 62443); //???????
                tcpListener.Start();
                Console.WriteLine($"Server 1 is running. IP address: {ipadrs_}; Local endpoint: {tcpListener.LocalEndpoint}");

                Socket socket = await tcpListener.AcceptSocketAsync();
                Console.WriteLine($"Connection established. Client remote endpoint: {socket.RemoteEndPoint}");
                Console.WriteLine($"Client local endpoint: {socket.LocalEndPoint}");

                byte[] buffer = new byte[1024];
                int total_bytes = await socket.ReceiveAsync(buffer, SocketFlags.None);
                string answer_string = Encoding.UTF8.GetString(buffer, 0, total_bytes);

                Console.WriteLine($"Info received by the server: {answer_string}");
                
                socket.Close();
                tcpListener.Stop();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }
    }
}
