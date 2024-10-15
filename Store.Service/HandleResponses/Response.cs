using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.HandleResponses
{
    public class Response
    {

        public Response(int statuscode,string ? message=null) { 

            StatusCode = statuscode;
            Message = message?? GetdefaultMwssageForStatuscode(statuscode);
        }
        public int StatusCode { get; set; }
        public string? Message { get; set; }

        private string GetdefaultMwssageForStatuscode(int code)
            => code switch
            {
                400 => "BadRequest",
                401 => "Unauthorized",
                403 => "Forbidden",
                404 => "NotFound",
                405 => "MethodNotAllowed",
                500 => "InternalServerError",
                _ => "An error occurred."

            };
    }
}
