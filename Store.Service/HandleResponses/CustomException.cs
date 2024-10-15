using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.HandleResponses
{
    public class CustomException:Response
    {
        public CustomException(int statuscode, string? message=null,string ? details=null):base(statuscode, message) {

            Details = details;
        }

        public string? Details {  get; set; }
    }
}
