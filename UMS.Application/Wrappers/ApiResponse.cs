using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Application.Wrappers
{
    public class ApiResponse<T>
    {
        public ApiResponse() { }
        public ApiResponse(T data, string message = "")
        {
            Successed = true;
            Message = message;
            Data = data;
        }
        public ApiResponse(string message)
        {
            Successed = false;
            Message = message;
        }
        public bool Successed { get; set; }
        public string Message { get; set; }
        public List<String> Errors { get; set; }
        public T Data{ get; set; }
    }
}
