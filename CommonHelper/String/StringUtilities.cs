using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.Mvc;

namespace CommonHelper.String
{
    public static class StringUtilities
    {
        public static string FixDecimalNumber(this string val)
        {
            val = val.Replace(",", "#");
            val = val.Replace(".", ",");
            val = val.Replace("#", ".");
            return val;
        }

        public static int GetTotalPage(this int totalItem, int pageSize = 20)
        {
            int sotrang = totalItem / pageSize;
            if (totalItem % pageSize != 0)
            {
                sotrang += 1;
            }
            return sotrang;
        }

        public static long GetTotalPage(this long totalItem, long pageSize = 20)
        {
            long sotrang = totalItem / pageSize;
            if (totalItem % pageSize != 0)
            {
                sotrang += 1;
            }
            return sotrang;
        }

        public static string ValueOrDefault(this string value, string valueDefault)
        {
            return !string.IsNullOrEmpty(value) ? value : valueDefault;
        }

        public static string ValueX100OrDefault(this string value, string valueDefault)
        {
            if (!string.IsNullOrEmpty(value) && double.TryParse(value.Replace(".", ","), out var dbValue))
            {
                return (dbValue * 100).ToString();
            }
            return valueDefault;
        }

        public static bool ValueOrDefault(this bool? value, bool valueDefault)
        {
            return value.HasValue ? value.Value : valueDefault;
        }

        public static string ValueOrDefault(this IEnumerable<string> values, string valueDefault)
        {
            if (values != null && values.Count() > 0)
            {
                return string.Join(", ", values);
            }
            return valueDefault;
        }

        public static string GetNewFileName(this string fileName)
        {
            var ext = Path.GetExtension(fileName);
            var name = Path.GetFileNameWithoutExtension(fileName);
            var rd = new Random();
            var TimeExt = DateTime.Now.ToString("yyyyMMdd-HHmmss-") + rd.Next(1000);
            return $"{name}_{TimeExt}{ext}";
        }

        public static string LayHoTrongHoTen(this string hoten)
        {
            if (!string.IsNullOrEmpty(hoten))
            {
                hoten = hoten.Trimtxt();
                var arrName = hoten.Split(' ').ToList();
                if (arrName != null)
                {
                    var ten = arrName.LastOrDefault();
                    arrName.Remove(ten);
                    if (arrName.Any())
                    {
                        return string.Join(" ", arrName);
                    }
                }
            }
            return string.Empty;
        }

        public static string LayTenTrongHoTen(this string hoten)
        {
            if (!string.IsNullOrEmpty(hoten))
            {
                hoten = hoten.Trimtxt();
                var arrName = hoten.Split(' ').ToList();
                if (arrName != null)
                {
                    var ten = arrName.LastOrDefault();
                    return ten;
                }
            }
            return string.Empty;
        }

        public static string Matchtext(this string text)
        {
            var Checktext = Regex.Match(text, @"\.{3,}");
            if (Checktext.Success)
            {
                return "";
            }
            return text;
        }

        public static string LayNameABC(this int so)
        {
            string[] mang = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "v", "x", "y", "z" };
            if (so > 0 && so <= mang.Length)
            {
                return mang[so - 1];
            }
            return "";
        }

        public static DateTime GetDateByMonth(int year, int month, bool isStart = true)
        {
            if (isStart)
            {
                return new DateTime(year, month, 1);
            }
            else
            {
                return new DateTime(year, month, DateTime.DaysInMonth(year, month));
            }
        }

        public static string ToStringDate(this DateTime? dateTime, string format = "dd/MM/yyyy")
        {
            if (dateTime.HasValue)
                return dateTime.Value.ToString(format);
            return string.Empty;
        }

        public static string GetEditMessage(string objectName, int type)
        {
            if (type == 1)
            {
                return $"Cập nhật đối tượng {objectName} vào hệ thống thành công";
            }
            else
            {
                return $"Cập nhật đối tượng {objectName} vào hệ thống thất bại";
            }
        }

        public static string GetDeleteMessage(string objectName, int type)
        {
            if (type == 1)
            {
                return $"Xóa đối tượng {objectName} khỏi hệ thống thành công";
            }
            else
            {
                return $"Xóa nhật đối tượng {objectName} khỏi hệ thống thất bại";
            }
        }

        public static string GetNullObjectMessage(string objectName)
        {
            return $"Đối tượng {objectName} không tồn tại trong hệ thống";
        }

        //Kiem tra ngay cham cong
        /// <summary>
        ///
        /// </summary>
        /// <param name="cDate"></param>
        /// <returns>true; Duoc phep cham cong</returns>
        public static bool AllowChamCongDate(this DateTime cDate)
        {
            var rs = false;
            var dnow = DateTime.Now.Date;
            if (dnow.DayOfWeek == DayOfWeek.Friday)
            {
                if (cDate == dnow.AddDays(1) || cDate == dnow.AddDays(2))
                {
                    rs = true;
                }
            }
            if (dnow.DayOfWeek == DayOfWeek.Monday)
            {
                if (cDate == dnow.AddDays(-1) || cDate == dnow.AddDays(-2))
                {
                    rs = true;
                }
            }
            if (dnow.DayOfWeek == DayOfWeek.Saturday)
            {
                if (cDate == dnow.AddDays(1))
                {
                    rs = true;
                }
            }
            if (dnow.DayOfWeek == DayOfWeek.Sunday)
            {
                if (cDate == dnow.AddDays(-1))
                {
                    rs = true;
                }
            }
            if (dnow == cDate)
            {
                rs = true;
            }
            return rs;
        }

        public static DateTime SetTime(this DateTime date, TimeSpan time)
        {
            date = date.AddHours(time.Hours);
            date = date.AddMinutes(time.Minutes);
            date = date.AddSeconds(time.Seconds);
            return date;
        }

        public static string GetParrentCode(this string path)
        {
            var rgx = new Regex(@"-[0-9a-zA-Z]*$");
            if (!string.IsNullOrEmpty(path))
            {
                return rgx.Replace(path, "");
            }
            else
            {
                return string.Empty;
            }
        }

        public static string Base64Encode(this string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        /// <summary>
        /// Bỏ các slash ở đầu của link
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string StandardPath(this string path)
        {
            var rgx = new Regex(@"^\/+");
            if (!string.IsNullOrEmpty(path))
            {
                return rgx.Replace(path, "");
            }
            else
            {
                return string.Empty;
            }
            return path;
        }

        /// <summary>
        /// Hiển thị dung lượng file
        /// </summary>
        /// <param name="Size"></param>
        /// <returns></returns>
        public static string ConvertToStringMb(this decimal? Size)
        {
            if (Size == null)
            {
                return "0";
            }
            var MbData = Math.Round(Size.GetValueOrDefault() / 1048576, 2);
            return MbData.ToString("#,#");
        }

        public static int LaySoThang(DateTime A, DateTime B)
        {
            var soThang = 0;
            while (A < B)
            {
                A = A.AddMonths(1);
                soThang++;
            }
            return soThang;
        }

        public static string ConvertToStringMb(this int Size)
        {
            if (Size < 1048576)
            {
                return Math.Round((decimal)Size / 1024, 4).ToString("#,#") + " KB";
            }
            return Math.Round((decimal)Size / 1048576, 4).ToString("#,#") + " MB";
        }

        public static List<SelectListItem> AddDefault(this List<SelectListItem> obj, string messageInit)
        {
            if (string.IsNullOrEmpty(messageInit))
            {
                messageInit = "--Chọn--";
            }
            if (obj == null)
            {
                obj = new List<SelectListItem>();
            }

            if (!obj.Any(x => x.Text.Equals(messageInit)))
            {
                obj.Add(new SelectListItem()
                {
                    Text = messageInit,
                    Value = "",
                    Selected = obj.Any(x => x.Selected) ? false : true
                });
            }
            return obj;
        }

        public static string InHoaChuCaiDau(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                str = str.Trim();
                var splitarr = str.Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
                var arrNew = new List<string>();
                if (splitarr.Any())
                {
                    foreach (var item in splitarr)
                    {
                        var lower = item.ToLower();
                        if (lower.Count() > 0)
                        {
                            lower = lower[0].ToString().ToUpper() + lower.Substring(1).ToLower();
                        }
                        arrNew.Add(lower);
                    }
                    str = string.Join(" ", arrNew);
                }
            }
            return str;
        }

        public static string InHoaChuCaiDauTien(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                str = str.Trim();
                str = str[0].ToString().ToUpper() + str.Substring(1).ToLower();
            }
            return str;
        }

        public static DateTime? getDateTimeFromMonthYear(int? month, int? year)
        {
            if (year != null && year > 0)
            {
                if (month != null && month.Value <= 12 && month.Value >= 1)
                {
                    return new DateTime(year.Value, month.Value, 1);
                }
                return new DateTime(year.Value, 1, 1);
            }
            return null;
        }

        /// <summary>
        /// ddMMyyyy
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static DateTime? GetDateFromtxt(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }
            if (input.Length != 8)
            {
                return null;
            }

            var day = input.Substring(0, 2);
            var month = input.Substring(2, 2);
            var year = input.Substring(4, 4);
            return DateTime.ParseExact(string.Format("{0}/{1}/{2}", day, month, year), "dd/MM/yyyy", null);
        }

        public static DateTime? ToDateTime(this string input)
        {
            try
            {
                if (string.IsNullOrEmpty(input))
                {
                    return null;
                }
                var date = input.Split('/');
                if (string.IsNullOrEmpty(date[0]) || string.IsNullOrEmpty(date[1]) || string.IsNullOrEmpty(date[2]))
                {
                    return null;
                }
                var day = int.Parse(date[0]).ToString("00");
                var month = int.Parse(date[1]).ToString("00");
                var year = int.Parse(date[2]).ToString("0000");
                return DateTime.ParseExact(string.Format("{0}/{1}/{2}", day, month, year), "dd/MM/yyyy", null);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static DateTime? ToDateTimeMMDDYYYY(this string input)
        {
            try
            {
                if (string.IsNullOrEmpty(input))
                {
                    return null;
                }
                var date = input.Split('/');
                if (string.IsNullOrEmpty(date[0]) || string.IsNullOrEmpty(date[1]) || string.IsNullOrEmpty(date[2]))
                {
                    return null;
                }
                var month = int.Parse(date[0]).ToString("00");
                var day = int.Parse(date[1]).ToString("00");
                var year = int.Parse(date[2]).ToString("0000");
                return DateTime.ParseExact(string.Format("{0}/{1}/{2}", day, month, year), "dd/MM/yyyy", null);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static IEnumerable<T> ToListNumber<T>(this string input, char splitKey = ',') where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>, IFormattable
        {
            if (!string.IsNullOrEmpty(input))
            {
                var list = input.Split(splitKey);
                foreach (var item in list)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        yield return (T)Convert.ChangeType(item, typeof(T));
                    }
                }
            }
        }

        public static DateTime? GetDateTimeFromAllowNull(int? y, int? m, int? d)
        {
            if (d == null || d <= 0)
            {
                d = 1;
            }
            if (m == null || m <= 0)
            {
                m = 1;
            }

            if (y == null || y <= 0)
            {
                return null;
            }
            else
                return new DateTime(y.Value, m.Value, d.Value);
        }

        public static Tuple<bool, string> CheckDateTimeFromAllowNull(int? y, int? m, int? d)
        {
            if (y == null && (m != null || d != null))
            {
                return new Tuple<bool, string>(false, "Vui lòng nhập năm");
            }
            if (d != null && m == null)
            {
                return new Tuple<bool, string>(false, "Vui lòng nhập tháng");
            }

            if (y != null)
            {
                if (y < 1945)
                {
                    return new Tuple<bool, string>(false, "Vui lòng nhập năm lớn hơn 1945");
                }
                if (y > 3000)
                {
                    return new Tuple<bool, string>(false, "Vui lòng nhập năm nhỏ hơn 3000");
                }
                if (m != null)
                {
                    if (m < 1 || m > 12)
                    {
                        return new Tuple<bool, string>(false, "Vui lòng nhập tháng từ 1 đến 12");
                    }
                }

                if (d != null)
                {
                    var maxDate = DateTime.DaysInMonth(y.Value, m.Value);
                    if (d < 0 || d > maxDate)
                    {
                        return new Tuple<bool, string>(false, "Vui lòng nhập ngày trong khoảng từ 1 đến " + maxDate);
                    }
                }
            }
            return new Tuple<bool, string>(true, "");
        }

        public static Nullable<T> ToNullableNumber<T>(this string input) where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>, IFormattable
        {
            if (string.IsNullOrEmpty(input) || !input.All(char.IsDigit))
            {
                return null;
            }
            var result = (T)Convert.ChangeType(input, typeof(T));
            return result;
        }

        public static T ToNumber<T>(this string input) where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>, IFormattable
        {
            if (string.IsNullOrEmpty(input) || !input.All(char.IsDigit))
            {
                return default(T);
            }
            var result = (T)Convert.ChangeType(input, typeof(T));
            return result;
        }

        public static string GetErrors(this ModelStateDictionary modelState)
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

        public static DateTime? ToEndDay(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }
            var parts = input.Split('/');
            if (string.IsNullOrEmpty(parts[0]) || string.IsNullOrEmpty(parts[1]) || string.IsNullOrEmpty(parts[2]))
            {
                return null;
            }
            int day, month, year;
            day = month = year = 0;

            if (!int.TryParse(parts[0], out day) || !int.TryParse(parts[1], out month) || !int.TryParse(parts[2], out year))
            {
                return null;
            }
            return DateTime.ParseExact(string.Format("{0}/{1}/{2} 23:59:59", day.ToString("00"), month.ToString("00"), year.ToString("0000")), "dd/MM/yyyy HH:mm:ss", null);
        }

        public static string ConvertToUnsign(string str)
        {
            string[] signs = new string[] {
        "aAeEoOuUiIdDyY",
        "áàạảãâấầậẩẫăắằặẳẵ",
        "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
        "éèẹẻẽêếềệểễ",
        "ÉÈẸẺẼÊẾỀỆỂỄ",
        "óòọỏõôốồộổỗơớờợởỡ",
        "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
        "úùụủũưứừựửữ",
        "ÚÙỤỦŨƯỨỪỰỬỮ",
        "íìịỉĩ",
        "ÍÌỊỈĨ",
        "đ",
        "Đ",
        "ýỳỵỷỹ",
        "ÝỲỴỶỸ",
        "!@#$%^&*(),.[]{}"
   };
            for (int i = 1; i < signs.Length; i++)
            {
                for (int j = 0; j < signs[i].Length; j++)
                {
                    str = str.Replace(signs[i][j], signs[0][i - 1]);
                }
            }
            for (int i = 0; i < signs[signs.Length - 1].Length; i++)
            {
                str = str.Replace(signs[signs.Length - 1][i], ' ');
            }
            return str;
        }

        public static string RemoveUnicode(this string text)
        {
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
                "đ",
                "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
                "í","ì","ỉ","ĩ","ị",
                "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
                "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
                "ý","ỳ","ỷ","ỹ","ỵ",};
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
            "d",
            "e","e","e","e","e","e","e","e","e","e","e",
            "i","i","i","i","i",
            "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
            "u","u","u","u","u","u","u","u","u","u","u",
            "y","y","y","y","y",};
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
                text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
            }
            string otherChars = "!@#$%^&*(),.[]{}";
            for (int i = 0; i < otherChars.Length; i++)
            {
                text = text.Replace(otherChars[i], ' ');
            }
            text = text.Replace("\t", "");
            return text;
        }

        public static DateTime? ToStartDay(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }
            var parts = input.Split('/');
            if (string.IsNullOrEmpty(parts[0]) || string.IsNullOrEmpty(parts[1]) || string.IsNullOrEmpty(parts[2]))
            {
                return null;
            }
            int day, month, year;
            day = month = year = 0;

            if (!int.TryParse(parts[0], out day) || !int.TryParse(parts[1], out month) || !int.TryParse(parts[2], out year))
            {
                return null;
            }
            return DateTime.ParseExact(string.Format("{0}/{1}/{2} 00:00:00", day.ToString("00"), month.ToString("00"), year.ToString("0000")), "dd/MM/yyyy HH:mm:ss", null);
        }

        /// <summary>
        /// Kiểm tra thao tác có nằm trong danh sách thao tác mà user có quyền không
        /// </summary>
        /// <param name="list_thaotac">Danh sách thao tác của user đang đăng nhập</param>
        /// <param name="ma_thaotac">Thao tác muốn kiểm tra quyền</param>
        /// <returns></returns>

        private static readonly string[] VietnameseSigns = new string[]

        {
            "aAeEoOuUiIdDyY",

            "áàạảãâấầậẩẫăắằặẳẵ",

            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",

            "éèẹẻẽêếềệểễ",

            "ÉÈẸẺẼÊẾỀỆỂỄ",

            "óòọỏõôốồộổỗơớờợởỡ",

            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",

            "úùụủũưứừựửữ",

            "ÚÙỤỦŨƯỨỪỰỬỮ",

            "íìịỉĩ",

            "ÍÌỊỈĨ",

            "đ",

            "Đ",

            "ýỳỵỷỹ",

            "ÝỲỴỶỸ"
        };

        public static string RemoveSign4VietnameseString(string str)
        {
            //Tiến hành thay thế , lọc bỏ dấu cho chuỗi
            for (int i = 1; i < VietnameseSigns.Length; i++)
            {
                for (int j = 0; j < VietnameseSigns[i].Length; j++)
                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
            }
            return str;
        }

        public static string GetFileNameFormart(this string str)
        {
            var result = RemoveSign4VietnameseString(str);
            result = result.Trim();
            result = result.Replace(' ', '_');
            return result;
        }

        public static bool ToBoolByOnOff(this string obj)
        {
            if (!string.IsNullOrEmpty(obj) && obj.ToLower().Equals("On".ToLower()))
            {
                return true;
            }
            return false;
        }

        public static bool? ToBoolByOnOffNULL(this string obj)
        {
            if (string.IsNullOrEmpty(obj))
            {
                return null;
            }
            if (!string.IsNullOrEmpty(obj) && obj.ToLower().Equals("On".ToLower()))
            {
                return true;
            }
            return false;
        }

        public static string ToBoolByOneZero(this string obj, string[] rule)
        {
            if (!string.IsNullOrEmpty(obj) && rule != null && rule.Length > 0)
            {
                if (obj.ToLower() == rule[0].ToLower())
                {
                    return "False";
                }
                if (obj.ToLower() == rule[1].ToLower())
                {
                    return "True";
                }
            }
            return "True";
        }

        public static DateTime? ToDateTimeFromMonth(this string obj)
        {
            //try
            //{
            if (!string.IsNullOrEmpty(obj))
            {
                var date = obj.Split('/');
                if (date != null)
                {
                    if (!string.IsNullOrEmpty(date[0]) && !string.IsNullOrEmpty(date[1]))
                    {
                        var month = int.Parse(date[0]).ToString("00");
                        var year = int.Parse(date[1]).ToString("0000");
                        return DateTime.ParseExact(string.Format("{0}/{1}/{2}", "01", month, year), "dd/MM/yyyy", null);
                    }
                }
                return null;
            }
            else
            {
                return null;
            }
        }

        public static DateTime? ToDateTimeFromYear(this string obj)
        {
            //try
            //{
            if (!string.IsNullOrEmpty(obj))
            {
                return DateTime.ParseExact(string.Format("{0}/{1}/{2}", "01", "01", obj), "dd/MM/yyyy", null);
            }
            else
            {
                return null;
            }
            //}catch(Exception ex){
            //    return null;
            // }
        }

        public static DateTime? ToEndYear(this DateTime? obj)
        {
            if (obj.HasValue)
            {
                var day = DateTime.DaysInMonth(obj.Value.Year, 12).ToString("00");

                return DateTime.ParseExact(string.Format("{0}/{1}/{2} 23:59:59", day, "12", obj.Value.Year.ToString("0000")), "dd/MM/yyyy HH:mm:ss", null);
            }
            else
            {
                return null;
            }
        }

        public static DateTime? ToEndMonth(this DateTime? obj)
        {
            if (obj.HasValue)
            {
                var day = DateTime.DaysInMonth(obj.Value.Year, obj.Value.Month).ToString("00");

                return DateTime.ParseExact(string.Format("{0}/{1}/{2} 23:59:59", day, obj.Value.Month.ToString("00"), obj.Value.Year.ToString("0000")), "dd/MM/yyyy HH:mm:ss", null);
            }
            else
            {
                return null;
            }
        }

        public static DateTime ToEndMonth(this DateTime obj)
        {
            var day = DateTime.DaysInMonth(obj.Year, obj.Month).ToString("00");

            return DateTime.ParseExact(string.Format("{0}/{1}/{2} 23:59:59", day, obj.Month.ToString("00"), obj.Year.ToString("0000")), "dd/MM/yyyy HH:mm:ss", null);
        }

        public static DateTime? ToEndDay(this DateTime? obj)
        {
            if (obj.HasValue)
            {
                return DateTime.ParseExact(string.Format("{0}/{1}/{2} 23:59:59", obj.Value.Day.ToString("00"), obj.Value.Month.ToString("00"), obj.Value.Year.ToString("0000")), "dd/MM/yyyy HH:mm:ss", null);
            }
            else
            {
                return null;
            }
        }

        public static DateTime ToEndDay(this DateTime obj)
        {
            return DateTime.ParseExact(string.Format("{0}/{1}/{2} 23:59:59", obj.Day.ToString("00"), obj.Month.ToString("00"), obj.Year.ToString("0000")), "dd/MM/yyyy HH:mm:ss", null);
        }

        public static DateTime? ToStartDay(this DateTime? obj)
        {
            if (obj.HasValue)
            {
                return DateTime.ParseExact(string.Format("{0}/{1}/{2} 00:00:00", obj.Value.Day.ToString("00"), obj.Value.Month.ToString("00"), obj.Value.Year.ToString("0000")), "dd/MM/yyyy HH:mm:ss", null);
            }
            else
            {
                return null;
            }
        }

        public static DateTime ToStartDay(this DateTime obj)
        {
            return DateTime.ParseExact(string.Format("{0}/{1}/{2} 00:00:00", obj.Day.ToString("00"), obj.Month.ToString("00"), obj.Year.ToString("0000")), "dd/MM/yyyy HH:mm:ss", null);
        }

        public static DateTime? ToStartMonth(this DateTime? obj)
        {
            if (obj.HasValue)
            {
                return DateTime.ParseExact(string.Format("{0}/{1}/{2} 00:00:00", "01", obj.Value.Month.ToString("00"), obj.Value.Year.ToString("0000")), "dd/MM/yyyy HH:mm:ss", null);
            }
            else
            {
                return null;
            }
        }

        public static DateTime ToStartMonth(this DateTime obj)
        {
            return DateTime.ParseExact(string.Format("{0}/{1}/{2} 00:00:00", "01", obj.Month.ToString("00"), obj.Year.ToString("0000")), "dd/MM/yyyy HH:mm:ss", null);
        }

        public static DateTime ToEndYear(this DateTime obj)
        {
            var day = DateTime.DaysInMonth(obj.Year, 12).ToString("00");

            return DateTime.ParseExact(string.Format("{0}/{1}/{2} 23:59:59", day, "12", obj.Year.ToString("0000")), "dd/MM/yyyy HH:mm:ss", null);
        }

        public static DateTime? ToStartYear(this DateTime? obj)
        {
            if (obj.HasValue)
            {
                return DateTime.ParseExact(string.Format("{0}/{1}/{2} 00:00:00", "01", "01", obj.Value.Year.ToString("0000")), "dd/MM/yyyy HH:mm:ss", null);
            }
            else
            {
                return null;
            }
        }

        public static DateTime ToStartYear(this DateTime obj)
        {
            return DateTime.ParseExact(string.Format("{0}/{1}/{2} 00:00:00", "01", "01", obj.Year.ToString("0000")), "dd/MM/yyyy HH:mm:ss", null);
        }

        public static short? ToShortOrNULL(this string obj)
        {
            if (!string.IsNullOrEmpty(obj))
            {
                return short.Parse(obj);
            }
            else
            {
                return null;
            }
        }

        public static int? ToIntOrNULL(this string obj)
        {
            if (!string.IsNullOrEmpty(obj))
            {
                return int.Parse(obj);
            }
            else
            {
                return null;
            }
        }

        public static long? ToLongOrNULL(this string obj)
        {
            if (!string.IsNullOrEmpty(obj))
            {
                return long.Parse(obj);
            }
            else
            {
                return null;
            }
        }

        public static short ToShortOrZero(this string obj)
        {
            if (!string.IsNullOrEmpty(obj))
            {
                return short.Parse(obj);
            }
            else
            {
                return 0;
            }
        }

        public static int ToIntOrZero(this string obj)
        {
            if (!string.IsNullOrEmpty(obj))
            {
                return int.Parse(obj);
            }
            else
            {
                return 0;
            }
        }

        public static bool ToBoolOrFalse(this string obj)
        {
            if (!string.IsNullOrEmpty(obj))
            {
                return bool.Parse(obj);
            }
            else
            {
                return false;
            }
        }

        public static long ToLongOrZero(this string obj)
        {
            if (!string.IsNullOrEmpty(obj))
            {
                return long.Parse(obj);
            }
            else
            {
                return 0;
            }
        }

        public static float ToFloatOrZero(this string obj)
        {
            try
            {
                if (!string.IsNullOrEmpty(obj))
                {
                    return float.Parse(obj);
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return 0;
            }
        }

        public static float? ToFloatOrNull(this string obj)
        {
            try
            {
                if (!string.IsNullOrEmpty(obj))
                {
                    return float.Parse(obj);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public static decimal ToDecimalOrZero(this string obj)
        {
            try
            {
                if (!string.IsNullOrEmpty(obj))
                {
                    return decimal.Parse(obj);
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return 0;
            }
        }

        public static decimal? ToDecimalOrNull(this string obj)
        {
            try
            {
                if (!string.IsNullOrEmpty(obj))
                {
                    return decimal.Parse(obj);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public static decimal? ToDecimalOrNullobject(this object obj)
        {
            try
            {
                if (obj != null)
                {
                    return Decimal.Parse(obj.ToString());
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public static List<long> ToListLong(this string obj, char split_key)
        {
            List<long> listLong = new List<long>();
            if (!string.IsNullOrEmpty(obj))
            {
                var list = obj.Split(split_key);
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            listLong.Add(long.Parse(item));
                        }
                    }
                }
            }
            return listLong;
        }

        public static List<int> ToListInt(this string obj, char split_key)
        {
            List<int> listInt = new List<int>();
            if (!string.IsNullOrEmpty(obj))
            {
                var list = obj.Split(split_key);
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            listInt.Add(int.Parse(item));
                        }
                    }
                }
            }
            return listInt;
        }

        public static List<string> ToListStringLower(this string obj, char split_key)
        {
            List<string> listInt = new List<string>();
            if (!string.IsNullOrEmpty(obj))
            {
                var list = obj.Split(split_key);
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            listInt.Add(item);
                        }
                    }
                }
            }
            return listInt;
        }

        public static List<short> ToListShort(this string obj, char split_key)
        {
            List<short> listInt = new List<short>();
            if (!string.IsNullOrEmpty(obj))
            {
                var list = obj.Split(split_key);
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            listInt.Add(short.Parse(item));
                        }
                    }
                }
            }
            return listInt;
        }

        public static DateTime GetMonday(this DateTime date)
        {
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return date;

                case DayOfWeek.Tuesday:
                    return date.AddDays(-1);

                case DayOfWeek.Wednesday:
                    return date.AddDays(-2);

                case DayOfWeek.Thursday:
                    return date.AddDays(-3);

                case DayOfWeek.Friday:
                    return date.AddDays(-4);

                case DayOfWeek.Saturday:
                    return date.AddDays(-5);

                case DayOfWeek.Sunday:
                    return date.AddDays(-6);

                default:
                    return date;
            }
        }

        public static DateTime FirstDateOfWeek(int year, int weekOfYear, System.Globalization.CultureInfo ci)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = (int)ci.DateTimeFormat.FirstDayOfWeek - (int)jan1.DayOfWeek;
            DateTime firstWeekDay = jan1.AddDays(daysOffset);
            int firstWeek = ci.Calendar.GetWeekOfYear(jan1, ci.DateTimeFormat.CalendarWeekRule, ci.DateTimeFormat.FirstDayOfWeek);
            if (firstWeek <= 1 || firstWeek > 50)
            {
                weekOfYear -= 1;
            }
            return firstWeekDay.AddDays(weekOfYear * 7);
        }

        public static int GetIso8601WeekOfYear(DateTime time)
        {
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        public static DateTime GetStartOfWeek(int year, int month, int weekofmonth)
        {
            //lấy ngày bắt đầu của tuần trong tháng
            var day = weekofmonth * 7 - 6;
            var StartDate = new DateTime(year, month, day);
            var weekOfYear = GetIso8601WeekOfYear(StartDate);
            return FirstDateOfWeek(year, weekOfYear, CultureInfo.CurrentCulture);
        }

        public static DateTime GetEndOfWeek(DateTime startOfWeek)
        {
            return startOfWeek.AddDays(6);
        }

        public static string Truncate(string input = "", int length = 0)
        {
            if (!string.IsNullOrEmpty(input))
            {
                if (input.Length <= length)
                {
                    return input;
                }
                else
                {
                    return input.Substring(0, length) + "...";
                }
            }
            return string.Empty;
        }

        public static string GetSummary(this string input, int length = 0)
        {
            if (!string.IsNullOrEmpty(input))
            {
                if (input.Length <= length)
                {
                    return input;
                }
                else
                {
                    return input.Substring(0, length) + "...";
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// Chuyển số tiền sang dạng chữ
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string ChuyenSo(this string number)
        {
            string[] dv = { "", "mươi", "trăm", "nghìn, ", "triệu, ", "tỉ, " };
            string[] cs = { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };

            var length = number.Length;
            number += "ss";
            var doc = new StringBuilder();
            var rd = 0;

            var i = 0;
            while (i < length)
            {
                //So chu so o hang dang duyet
                var n = (length - i + 2) % 3 + 1;

                //Kiem tra so 0
                var found = 0;
                int j;
                for (j = 0; j < n; j++)
                {
                    if (number[i + j] == '0') continue;
                    found = 1;
                    break;
                }

                //Duyet n chu so
                int k;
                if (found == 1)
                {
                    rd = 1;
                    for (j = 0; j < n; j++)
                    {
                        var ddv = 1;
                        switch (number[i + j])
                        {
                            case '0':
                                if (n - j == 3)
                                    doc.Append(cs[0]);
                                if (n - j == 2)
                                {
                                    if (number[i + j + 1] != '0')
                                        doc.Append("lẻ");
                                    ddv = 0;
                                }
                                break;

                            case '1':
                                switch (n - j)
                                {
                                    case 3:
                                        doc.Append(cs[1]);
                                        break;

                                    case 2:
                                        doc.Append("mười");
                                        ddv = 0;
                                        break;

                                    case 1:
                                        k = (i + j == 0) ? 0 : i + j - 1;
                                        doc.Append((number[k] != '1' && number[k] != '0') ? "mốt" : cs[1]);
                                        break;
                                }
                                break;

                            case '5':
                                doc.Append((i + j == length - 1) ? "lăm" : cs[5]);
                                break;

                            default:
                                doc.Append(cs[number[i + j] - 48]);
                                break;
                        }

                        doc.Append(" ");

                        //Doc don vi nho
                        if (ddv == 1)
                            doc.Append(dv[n - j - 1] + " ");
                    }
                }

                //Doc don vi lon
                if (length - i - n > 0)
                {
                    if ((length - i - n) % 9 == 0)
                    {
                        if (rd == 1)
                            for (k = 0; k < (length - i - n) / 9; k++)
                                doc.Append("tỉ, ");
                        rd = 0;
                    }
                    else
                        if (found != 0) doc.Append(dv[((length - i - n + 1) % 9) / 3 + 2] + " ");
                }

                i += n;
            }
            var result = (length == 1) && (number[0] == '0' || number[0] == '5') ? cs[number[0] - 48] : doc.ToString();
            result = result.Trim().Trimtxt();
            if (result[result.Length - 1] == ',')
            {
                result = result.Substring(0, result.Length - 1);
            }
            return result;
        }

        public static string Trimtxt(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                str = str.TrimStart();
                str = str.TrimEnd();
                str = str.Trim();
                while (str.Contains("  "))
                {
                    str = str.Replace("  ", " ");
                }
            }
            else
            {
                str = string.Empty;
            }

            return str;
        }

        public static string ToDateTimeTextZero(this string obj)
        {
            if (!string.IsNullOrEmpty(obj))
            {
                var date = obj.Split('/');
                if (date != null)
                {
                    if (date.Length == 3)
                    {
                        return obj;
                    }

                    if (date.Length == 2)
                    {
                        return "00/" + obj;
                    }

                    if (date.Length == 1)
                    {
                        return "00/00/" + obj;
                    }
                }
                return string.Empty;
            }
            else
            {
                return string.Empty;
            }
        }

        public static string ToSafeFileName(this string s)
        {
            return s
                .Replace("\\", "")
                .Replace("/", "")
                .Replace("\"", "")
                .Replace("*", "")
                .Replace(":", "")
                .Replace("?", "")
                .Replace("<", "")
                .Replace(">", "")
                .Replace("|", "");
        }

        public static double ToDoubleOrZero(this string obj)
        {
            try
            {
                if (!string.IsNullOrEmpty(obj))
                {
                    return double.Parse(obj);
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return 0;
            }
        }

        public static string ToDateVNFormat(this DateTime obj)
        {
            return string.Format("{0: dd/MM/yyyy}", obj);
        }

        public static string ToDateVNFormat(this DateTime? obj)
        {
            return string.Format("{0: dd/MM/yyyy}", obj);
        }

        public static string ToDatetimeVNFormat(this DateTime obj)
        {
            return string.Format("{0: dd/MM/yyyy HH:mm:ss}", obj);
        }

        public static string ToDatetimeVNFormat(this DateTime? obj)
        {
            return string.Format("{0: dd/MM/yyyy HH:mm:ss}", obj);
        }

        public static double? ToDoublelOrNull(this string obj)
        {
            try
            {
                if (!string.IsNullOrEmpty(obj))
                {
                    return double.Parse(obj);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Hiển thị ngày tháng
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetTextDisplay(this DateTime dt)
        {
            var nameArr = new string[] { "Chủ nhật", "Thứ hai", "Thứ ba", "Thứ tư", "Thứ năm", "Thứ sáu", "Thứ bảy" };
            return nameArr[(int)dt.DayOfWeek] + ", ngày " + dt.ToString("dd/MM/yyyy");
        }

        public static string GetTextDayDisplay(this DateTime dt)
        {
            var nameArr = new string[] { "Chủ nhật", "Thứ hai", "Thứ ba", "Thứ tư", "Thứ năm", "Thứ sáu", "Thứ bảy" };
            return nameArr[(int)dt.DayOfWeek];
        }

        public static string ConvertToVN(this string chucodau)
        {
            const string FindText = "áàảãạâấầẩẫậăắằẳẵặđéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵÁÀẢÃẠÂẤẦẨẪẬĂẮẰẲẴẶĐÉÈẺẼẸÊẾỀỂỄỆÍÌỈĨỊÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢÚÙỦŨỤƯỨỪỬỮỰÝỲỶỸỴ";
            const string ReplText = "aaaaaaaaaaaaaaaaadeeeeeeeeeeeiiiiiooooooooooooooooouuuuuuuuuuuyyyyyAAAAAAAAAAAAAAAAADEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYY";
            int index = -1;
            char[] arrChar = FindText.ToCharArray();
            while ((index = chucodau.IndexOfAny(arrChar)) != -1)
            {
                int index2 = FindText.IndexOf(chucodau[index]);
                chucodau = chucodau.Replace(chucodau[index], ReplText[index2]);
            }
            return chucodau;
        }

        public static string GetUserName(this string obj)
        {
            string result = string.Empty;

            obj = obj.Trim().Trimtxt();
            obj = obj.ConvertToVN();
            var tmpStr = obj.Split(' ').ToList();
            tmpStr.Reverse();
            if (tmpStr.Count == 1)
            {
                result += obj;
            }
            else if (tmpStr.Count == 2)
            {
                result += tmpStr[0];
                result += tmpStr[1][0];
            }
            else
            {
                result += tmpStr[0];
                tmpStr.Reverse();
                for (int i = 0; i < tmpStr.Count - 1; i++)
                {
                    result += tmpStr[i][0];
                }
            }
            return result;
        }

        public static string GetChuCaiDauTrongHoTen(this string obj)
        {
            obj = obj.Trim().Trimtxt();
            obj = obj.ConvertToVN();
            var tmpStr = obj.Split(' ');
            string result = string.Empty;
            if (!string.IsNullOrEmpty(obj))
            {
                if (tmpStr.Length == 1)
                {
                    if (tmpStr[0].Length == 1)
                    {
                        result += tmpStr[0][0];
                        result += tmpStr[0][0];
                        result += tmpStr[0][0];
                    }
                    else
                    {
                        result += tmpStr[0][0];
                        result += tmpStr[0][1];
                        result += tmpStr[0][tmpStr[0].Length - 1];
                    }
                }
                else if (tmpStr.Length == 2)
                {
                    result += tmpStr[0][0];
                    result += tmpStr[1][0];
                    result += tmpStr[1][tmpStr[1].Length - 1];
                }
                else
                {
                    result += tmpStr[0][0];
                    result += tmpStr[tmpStr.Length - 2][0];
                    result += tmpStr[tmpStr.Length - 1][0];
                }
            }
            return result.ToUpper();
        }

        public static string GenerateCoupon(int length)
        {
            Random random = new Random();
            string characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            StringBuilder result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(characters[random.Next(characters.Length)]);
            }
            return result.ToString();
        }

        /// <summary>
        /// Tạo tên đường dẫn cho url
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SlugTitleName(this string obj)
        {
            obj = obj.Trim();
            obj = RemoveSign4VietnameseString(obj);
            obj = obj.Replace("\"", "");
            obj = obj.Replace("\'", "");
            obj = Regex.Replace(obj, @"[!@#$%^&*()<>,.:\/|?]+", "");
            obj = Regex.Replace(obj, @"[\s\t\n]+", " ");

            var result = obj.Replace(" ", "-");
            result = result + '-' + GenerateCoupon(10);
            return result;
        }

        public static string RemoveAccent(this string txt)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(txt);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }

        public static string Slugify(this string phrase)
        {
            if (string.IsNullOrEmpty(phrase))
            {
                return string.Empty;
            }

            string str = phrase.RemoveAccent().ToLower();
            str = System.Text.RegularExpressions.Regex.Replace(str, @"[^a-z0-9\s-]", ""); // Remove all non valid chars
            str = System.Text.RegularExpressions.Regex.Replace(str, @"\s+", " ").Trim(); // convert multiple spaces into one space
            str = System.Text.RegularExpressions.Regex.Replace(str, @"\s", "-"); // //Replace spaces by dashes
            return str;
        }

        public static double GetTotalDayOff(DateTime fromDate, string fromTypeDate, DateTime toDate, string toTypeDate)
        {
            if (fromDate.Date == toDate.Date)
            {
                return fromTypeDate == "CANGAY" ? 1 : 0.5;
            }
            else
            {
                double total = 0;
                var midDate = (toDate.Date - fromDate.Date).Days - 1;
                total += (double)midDate;
                total += fromTypeDate == "CANGAY" ? 1 : 0.5;
                total += toTypeDate == "CANGAY" ? 1 : 0.5;
                return total;
            }
        }

        public static List<DateTime> CountSaturdayAndSundayOfMonth(int month, int year)
        {
            List<DateTime> dates = new List<DateTime>();

            DayOfWeek day = DayOfWeek.Sunday;
            DayOfWeek sat = DayOfWeek.Saturday;
            System.Globalization.CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
            for (int i = 1; i <= currentCulture.Calendar.GetDaysInMonth(year, month); i++)
            {
                DateTime d = new DateTime(year, month, i);
                if (d.DayOfWeek == day || d.DayOfWeek == sat)
                {
                    dates.Add(d);
                }
            }
            return dates;
        }

        public static List<DateTime> CountDaysOfMonth(DayOfWeek day, int month, int year)
        {
            List<DateTime> dates = new List<DateTime>();
            System.Globalization.CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
            for (int i = 1; i <= currentCulture.Calendar.GetDaysInMonth(year, month); i++)
            {
                DateTime d = new DateTime(year, month, i);
                if (d.DayOfWeek == day)
                {
                    dates.Add(d);
                }
            }
            return dates;
        }

        public static string MD5Hash(this string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits
                //for each byte
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }

        public static string md5EndCode(this string input)
        {
            MD5 md5Hash = new MD5CryptoServiceProvider();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        public static int CalcThamNien(int monthTD, int yearTD, int monXet, int yearXet)
        {
            if (yearXet < yearTD)
            {
                return 0;
            }
            else
            {
                var YRturn = yearXet - yearTD;
                if (monthTD < monXet)
                {
                    YRturn -= 1;
                }
                return YRturn;
            }
        }

        public static string ConvertToRoman(this int number)
        {
            if (number < 1 || number > 3999)
            {
                throw new ArgumentOutOfRangeException("Input must be between 1 and 3999");
            }

            string[] romanNumerals = { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };
            int[] arabicValues = { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };

            List<string> result = new List<string>();

            for (int i = 0; i < romanNumerals.Length; i++)
            {
                while (number >= arabicValues[i])
                {
                    result.Add(romanNumerals[i]);
                    number -= arabicValues[i];
                }
            }

            return string.Join("", result);
        }
    }
}