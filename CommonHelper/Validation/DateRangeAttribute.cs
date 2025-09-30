using System;
using System.ComponentModel.DataAnnotations;

namespace CommonHelper.Validation
{
    public class DateRangeAttribute : RangeAttribute
    {
        public DateRangeAttribute(string mininumDate) : base(typeof(DateTime), mininumDate, string.Format("{0:dd/MM/yyyy}", DateTime.Now))
        {
            ErrorMessage = "Vui lòng nhập ngày nhỏ hơn hiện tại";
        }
    }
}