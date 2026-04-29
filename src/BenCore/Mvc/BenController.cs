using System.Text;
using System.Text.Json;
using Torff.Ttp;

namespace BenCore.Mvc
{
    public abstract class BenController
    {
        public TtpRequest Request { get; set; }

        protected TtpResponse Ok(object data)
        {
            string jsonBody = JsonSerializer.Serialize(data);
            
            return new TtpResponse
            {
                StatusCode = 200,
                ContentType = "application/json",
                Body = Encoding.UTF8.GetBytes(jsonBody)
            };
        }

        protected TtpResponse NotFound(string message = "Resource not found")
        {
            string jsonBody = JsonSerializer.Serialize(new { erro = message });
            
            return new TtpResponse
            {
                StatusCode = 404,
                ContentType = "application/json",
                Body = Encoding.UTF8.GetBytes(jsonBody)
            };
        }
    }
}