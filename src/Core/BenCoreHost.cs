using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Text;
using System.Text.Json;
using Torff.Ttp;
using BenCore.Mvc;


namespace BenCore.Core
{
    public class BenCoreHost
    {
        private readonly int _port;
        private readonly RouteScanner _scanner;

        public BenCoreHost(int port = 5000)
        {
            _port = port;
            _scanner = new RouteScanner();
        }

        public async Task StartAsync()
        {
            _scanner.Scan();
            
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
                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[8192];

                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);

                if (bytesRead > 0)
                {
                    string jsonRequest = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    TtpRequest request = JsonSerializer.Deserialize<TtpRequest>(jsonRequest);

                    Console.WriteLine($"\n[BenCore] TTP Request Received!");
                    Console.WriteLine($"[BenCore] Route: {request.Method} {request.Path}");
                    Console.WriteLine($"[BenCore] Screening: {request.RequestId}");

                    string routeKey = $"{request.Method.ToUpper()}|{request.Path}";

                    TtpResponse response;

                    if (_scanner.Routes.TryGetValue(routeKey, out System.Reflection.MethodInfo methodInfo))
                    {
                        Type controllerType = methodInfo.DeclaringType;

                        BenController controllerInstance = (BenController)Activator.CreateInstance(controllerType);

                        controllerInstance.Request = request;

                        response = (TtpResponse)methodInfo.Invoke(controllerInstance, null);
                        
                        Console.WriteLine($"[BenCore] Sucess: {controllerType.Name}.{methodInfo.Name}() Invoked!");
                    }
                    else
                    {
                        // Rota não encontrada no mapa! 
                        Console.WriteLine($"[BenCore] 404 Error: Route Not Foud.");
                        
                        string jsonErro = JsonSerializer.Serialize(new { erro = "Route Not Foud in BenCore" });
                        response = new TtpResponse
                        {
                            StatusCode = 404,
                            ContentType = "application/json",
                            Body = Encoding.UTF8.GetBytes(jsonErro)
                        };
                    }

                    string jsonResponse = JsonSerializer.Serialize(response);
                    byte[] responseBytes = Encoding.UTF8.GetBytes(jsonResponse);

                    await stream.WriteAsync(responseBytes, 0, responseBytes.Length);
                    Console.WriteLine($"[BenCore] Response processed and returned to Torff!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[BenCore] TTP communication error: {ex.Message}");
            }
            finally
            {
                client.Close();
            }
        }
    }
}