using Hinet.Web.Areas.SiteConfigArea.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hinet.Web.Models
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public string Error { get; set; }

        public static JsonResult SuccessResponse(object data = null, string message = "Thành công")
        {
            return new JsonResult
            {
                Data = new ApiResponse
                {
                    Success = true,
                    Message = message,
                    Data = data
                },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public static JsonResult ErrorResponse(string message = "Đã xảy ra lỗi", string error = null)
        {
            return new JsonResult
            {
                Data = new ApiResponse
                {
                    Success = false,
                    Message = message,
                    Error = error
                },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}