using System;

namespace WebApplication1.Responses
{
    public class BaseResponse
    {
        public Guid RequestId { get; set; }

        public DateTime RequestTime { get; set; }
    }
}
