using Hinet.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hinet.Web.Models
{
    public class CheckerResult
    {
        public bool Status { get; set; }
        public string Message { get; set; }

        public CheckerResult(bool status, string message)
        {
            Status = status;
            Message = message;
        }

        public static CheckerResult Success(string msg = "Giao dịch hợp lệ")
            => new CheckerResult(true, msg);
        public static CheckerResult Error(string msg)
            => new CheckerResult(false, msg);
    }
}