using System;

namespace WebApplication1.Responses
{
    /// <summary>
    /// Base error message contains request id and date and time to aid debugging later
    /// </summary>
    public class BaseResponse
    {
        public Guid RequestId { get; set; }

        public DateTime RequestTime { get; set; }
    }
}
