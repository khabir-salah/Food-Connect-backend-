using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DTOs
{
    public class BaseResponse<T>
    {
        public T? Data { get; set; }
        public bool IsSuccessfull { get; set; }
        public string? Message { get; set; }
    }
}
