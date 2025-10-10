using System.Linq;
using System.Text;

namespace Hinet.API2.Core
{
    public static class ValidateModelExtend
    {
        public static string GetErrorsAPI(this System.Web.Http.ModelBinding.ModelStateDictionary modelState)
        {
            string result = string.Empty;
            foreach (var item in modelState)
            {
                var state = item.Value;
                if (state.Errors.Any())
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var error in state.Errors)
                    {
                        sb.Append(error.ErrorMessage);
                    }
                    result = sb.ToString();
                }
            }
            return result;
        }
    }
}