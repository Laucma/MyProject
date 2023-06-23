using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace WorkerServiceDemo.Model
{
    public class HttpResponseItem
    {
        public HttpStatusCode statusCode { get; set; }
        public string responseMsg { get; set; }
    }

    public class ErrorMessage
    {
        public string error { get; set; }
        public string error_description { get; set; }
    }
}
