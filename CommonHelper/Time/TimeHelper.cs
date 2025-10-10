using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper.Time
{
    public static class TimeHelper
    {
        public static string ToTimeAgo(this DateTime dateTime)
        {
            var now = DateTime.Now;
            var ts = now - dateTime;

            if (ts.TotalSeconds < 60)
                return "Vừa xong";

            if (ts.TotalMinutes < 60)
                return $"{(int)ts.TotalMinutes} phút trước";

            if (ts.TotalHours < 24)
                return $"{(int)ts.TotalHours} giờ trước";

            if (ts.TotalDays < 2)
                return $"Hôm qua lúc {dateTime:HH:mm}";

            if (ts.TotalDays < 7)
                return $"{(int)ts.TotalDays} ngày trước";

            if (ts.TotalDays < 30)
            {
                var weeks = (int)(ts.TotalDays / 7);
                return $"{weeks} tuần trước";
            }

            if (ts.TotalDays < 365)
                return dateTime.ToString("dd/MM/yyyy HH:mm");

            return dateTime.ToString("dd/MM/yyyy");
        }
    }
}
