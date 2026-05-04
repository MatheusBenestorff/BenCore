namespace BenCore.ORM.Models
{
    public class KrakenResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string[] Data { get; set; }
    }
}