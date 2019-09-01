using System;
using System.IO;
using System.Web;
using System.Web;
using Newtonsoft.Json;
using System.Web.Mvc;

namespace SurveyApp.Models
{
    public class JsonNetResult : JsonResult
    {
        public JsonNetResult() : base()
        {
            Settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Error
            };
        }

        public JsonSerializerSettings Settings { get; private set; }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            if (this.JsonRequestBehavior == JsonRequestBehavior.DenyGet && string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("JSON GET is not allowed");

            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = string.IsNullOrEmpty(this.ContentType) ? "application/json" : this.ContentType;

            if (this.ContentEncoding != null)
                response.ContentEncoding = this.ContentEncoding;
            if (this.Data == null)
                return;

            HttpResponse Response = HttpContext.Current.Response;

            string AcceptEncoding = HttpContext.Current.Request.Headers["Accept-Encoding"];
            if (AcceptEncoding.Contains("gzip"))
            {
                Response.Filter = new System.IO.Compression.GZipStream(Response.Filter,
                                          System.IO.Compression.CompressionMode.Compress);
                Response.AppendHeader("Content-Encoding", "gzip");
            }
            else
            {
                Response.Filter = new System.IO.Compression.DeflateStream(Response.Filter,
                                          System.IO.Compression.CompressionMode.Compress);
                Response.AppendHeader("Content-Encoding", "deflate");
            }

            var scriptSerializer = JsonSerializer.Create(this.Settings);

            using (var sw = new StringWriter())
            {
                scriptSerializer.Serialize(sw, this.Data);
                response.Write(sw.ToString());
            }
        }
    }
}