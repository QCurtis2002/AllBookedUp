using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBookedUp.Shared
{
    public class ServiceResponse<T>
    {
        //Data from the response
        public T Data { get; set; }
        //If the call was successful
        public bool Success { get; set; } = true;
        //Message with the response
        public string Message { get; set; }
    }
}
