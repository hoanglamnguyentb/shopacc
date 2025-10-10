using CommonHelper.String;
using System;
using System.Web.Mvc;

namespace Hinet.Web.Core
{
    public class DecimalModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            return (valueProviderResult != null && string.IsNullOrEmpty(valueProviderResult.AttemptedValue)) ? base.BindModel(controllerContext, bindingContext) : Convert.ToDecimal(valueProviderResult.AttemptedValue.FixDecimalNumber());
            // of course replace with your custom conversion logic
        }
    }
}