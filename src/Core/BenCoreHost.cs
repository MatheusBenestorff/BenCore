using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace BenCore.Core
{
    public class BenCoreHost
    {
        private readonly int _port;

        public BenCoreHost(int port = 5000)
        {
            _port = port;
        }

        public async Task StartAsync()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, _port);
            listener.Start();
            
            Console.WriteLine($"[BenCore] Framework started and listening on port {_port}");

            while (true)
            {
                TcpClient client = await listener.AcceptTcpClientAsync();
                
                _ = ProcessConnectionAsync(client);
            }
        }

        private async Task ProcessConnectionAsync(TcpClient client)
        {
            try
            {
                Console.WriteLine($"\n[BenCore] CONNECTION RECEIVED!");
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[BenCore] Communication error: {ex.Message}");
            }
            finally
            {
                client.Close();
            }
        }
    }
}