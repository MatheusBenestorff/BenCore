using System.Net.Sockets;
using System.Text;
namespace BenCore.ORM.Providers
{
    public class KrakenProvider : IDbProvider, IDisposable
    {
        private readonly string _host;
        private readonly int _port;
        private TcpClient _client;

        public KrakenProvider(string host = "localhost", int port = 5432)
        {
            _host = host;
            _port = port;
        }

        public async Task<string> ExecuteQueryAsync(string sql)
        {
            try
            {
                _client = new TcpClient();
                await _client.ConnectAsync(_host, _port);
                using NetworkStream stream = _client.GetStream();
                
                byte[] data = Encoding.UTF8.GetBytes(sql);
                await stream.WriteAsync(data, 0, data.Length);

                byte[] buffer = new byte[8192];
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                
                return Encoding.UTF8.GetString(buffer, 0, bytesRead);
            }
            catch (Exception ex)
            {
                return $"{{\"Success\":false, \"Message\":\"Connection error with KrakenDB: {ex.Message}\"}}";
            }
            finally
            {
                _client?.Close();
            }
        }

        public void Dispose() => _client?.Dispose();
    }
}