using System.Web.Mvc;

namespace CommonHelper.String.HTML
{
    public static class HTMLStringUltilities
    {
        public static MvcHtmlString ToEmptyString(this string obj)
        {
            if (!string.IsNullOrEmpty(obj))
            {
                return MvcHtmlString.Create(obj);
            }
            return MvcHtmlString.Create("<span style='color:orange;font-style: italic;'>Không có dữ liệu</span>");
        }

        /// <summary>
        /// Tạo dường dẫn chi tiết cho chuỗi hoặc trả về chuỗi rỗng
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="prefixUrl">Đường dẫn phía trước</param>
        /// <param name="paramUrl">Id của sản phẩm cần truy vấn tới nếu có</param>
        /// <returns></returns>
        public static MvcHtmlString ToEmptyLink(this string obj, string prefixUrl = "", string paramUrl = "")
        {
            if (!string.IsNullOrEmpty(obj))
            {
                string referredUrl = prefixUrl;
                if (!string.IsNullOrEmpty(paramUrl))
                {
                    referredUrl += paramUrl;
                }
                else
                {
                    referredUrl += obj;
                }
                return MvcHtmlString.Create("<a href='" + referredUrl + "'>" + obj + "</a>");
            }
            return MvcHtmlString.Create("<span style='color:orange;font-style: italic;'>Không có dữ liệu</span>");
        }

        public static MvcHtmlString ToMoneyEmpty(this object obj, string moneyExtension = "đồng")
        {
            if (obj != null)
            {
                if (obj.ToString().ToDecimalOrZero() > 0)
                {
                    return MvcHtmlString.Create(string.Format("{0:#,#} " + moneyExtension, obj));
                }
                return MvcHtmlString.Create("0 đồng");
            }
            return MvcHtmlString.Create("<span class='italic'>Chưa có thông tin</span>");
        }

        /// <summary>
        /// Đổi tiền sang định dạng chữ,
        /// áp dụng đối với đơn vị là lưu tiền là triệu đồng
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static MvcHtmlString ToMoneyTextEmpty(this object obj)
        {
            if (obj != null)
            {
                var d = obj.ToString().Replace(",", "");
                var a = long.Parse(d.ToString());
                long b = a * 1;
                var c = b.ToString();
                if (!string.IsNullOrEmpty(c))
                {
                    c = c.Replace(",", "");
                    var model = StringUtilities.ChuyenSo(c);
                    model = model.Replace(",", "");
                    if (model != "")
                    {
                        model = model.Substring(0, 1).ToUpper() + model.Substring(1).ToLower();
                        return MvcHtmlString.Create(string.Format("({0} đồng)", model));
                    }
                }
            }
            return MvcHtmlString.Create("<span></span>");
        }

        public static MvcHtmlString ToBoolEmpty(this object obj, string postfix = "phê duyệt")
        {
            if (obj != null)
            {
                var boolObj = obj.ToString().ToBoolOrFalse();
                if (boolObj == true)
                {
                    return MvcHtmlString.Create("<span class='green'>Đã " + postfix + "</span>");
                }
                return MvcHtmlString.Create("<span class='red'>Chưa " + postfix + "</span>");
            }
            return MvcHtmlString.Create("<span>Chưa có thông tin</span>");
        }

        public static MvcHtmlString ShowStatus(this ShowStatusDto obj)
        {
            var color = "#fff";
            var bgcolor = "#ecf0f1";
            var icon = "<i class='fa fa-circle'></i>";
            if (obj != null)
            {
                if (obj.Color != null)
                {
                    color = obj.Color;
                }
                if (obj.BgColor != null)
                {
                    bgcolor = obj.BgColor;
                }

                if (obj.Icon != null)
                {
                    icon = "<i class='" + obj.Icon + "'></i>";
                }

                if (obj.Name != null)
                {
                    return MvcHtmlString.Create($"<div class='statusInTable' style='color:{color}; background-color: ${bgcolor}'>{icon} <span>{obj.Name}</span></div>");
                }
                else
                {
                    return MvcHtmlString.Create($"<div></div>");
                }
            }
            return MvcHtmlString.Create($"<div></div>");
        }
    }

    public class ShowStatusDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string BgColor { get; set; }
        public string Icon { get; set; }
    }
}