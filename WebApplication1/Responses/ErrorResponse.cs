using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Responses
{
    public class ErrorResponse : BaseResponse
    {
        public string ErrorMessage { get; set; }
    }
}
