
 using System.Net;

namespace InsumosAPI.Middleware.Models
{
    public class MessageInfoDTO
    {
        public HttpStatusCode Status { get; set; }
        public string? Message { get; set; }
        public string Success { get; set; }
        public dynamic? Detail { get; set; }

    }
}