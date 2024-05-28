using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Utilities.Http
{
    public class ServiceResponse : IServiceResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public object Optional { get; set; }
        public ExcepitonContainer Exception { get; set; }

        public static ServiceResponse Fail(string errorMessage = "Error message withheld by server.")
        {
            return Fail(errorMessage, null);
        }

        public static ServiceResponse Success(string message = "Success!", object optional = null)
        {
            var sr = new ServiceResponse
            {
                Status = true,
                Message = message,
                Optional = optional
            };

            return sr;
        }

        public static ServiceResponse Fail(string errorMessage, Exception ex)
        {
            var sr = new ServiceResponse
            {
                Status = false,
                Message = errorMessage
            };

            if (ex != null)
                sr.Exception = new ExcepitonContainer(ex);

            return sr;
        }
    }

    public interface IServiceResponse
    {
        bool Status { get; set; }
        string Message { get; set; }
    }

    public class ExcepitonContainer
    {
        public ExcepitonContainer() { }
        public ExcepitonContainer(Exception ex)
        {
            if (ex == null) return;

            if (ex.Data != null)
            {
                Data = new Dictionary<object, object>();
                foreach (var i in ex.Data.Keys)
                    try { Data.Add(i, ex.Data[i]); }
                    catch { }
            }

            try { HelpLink = ex.HelpLink; } catch { }
            try { HResult = ex.HResult; } catch { }
            try { Message = ex.Message; } catch { }
            try { Source = ex.Source; } catch { }
            try { StackTrace = ex.StackTrace; } catch { }

            if (ex.InnerException != null)
                try { InnerException = new ExcepitonContainer(ex.InnerException); } catch { }

        }

        public Dictionary<object, object> Data { get; set; }

        public string HelpLink { get; set; }

        public int HResult { get; set; }

        public ExcepitonContainer InnerException { get; set; }

        public string Message { get; set; }

        public string Source { get; set; }

        public string StackTrace { get; set; }
    }
}
