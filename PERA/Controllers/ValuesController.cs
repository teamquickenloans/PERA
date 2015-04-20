using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PERA.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/<controller>
        public HttpResponseMessage GetFile()
        {
            Trace.WriteLine("HELLO");
            HttpResponseMessage result = null;
            var localFilePath = System.Web.HttpContext.Current.Server.MapPath("~/output.xlsx");

            if (!System.IO.File.Exists(localFilePath))
            {
                result = Request.CreateResponse(HttpStatusCode.Gone);
            }
            else
            {// serve the file to the client
                result = Request.CreateResponse(HttpStatusCode.OK);
                result.Content = new StreamContent(new FileStream(localFilePath, FileMode.Open, FileAccess.Read));
                result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                result.Content.Headers.ContentDisposition.FileName = "IssueReport.xlsx";
            }

            return result;
        }
    }
}