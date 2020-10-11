using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace App.Common.SeedWork
{
    public class ActionResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Title { get; set; }
    }
}
