using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommonHelper
{
	public class SlugHelper
	{
		public static string GenerateSlug(string phrase, int maxLength = 100)
		{
			if (string.IsNullOrWhiteSpace(phrase))
				return string.Empty;

			// Chuyển về chữ thường
			string str = phrase.ToLowerInvariant();

			// Xử lý riêng cho ký tự đ
			str = str.Replace("đ", "d");

			// Bỏ dấu tiếng Việt hoặc ký tự có dấu
			str = RemoveDiacritics(str);

			// Chỉ giữ lại ký tự a-z, 0-9, khoảng trắng
			str = Regex.Replace(str, @"[^a-z0-9\s-]", "");

			// Thay nhiều khoảng trắng thành 1 khoảng trắng
			str = Regex.Replace(str, @"\s+", " ").Trim();

			// Giới hạn độ dài
			if (str.Length > maxLength)
				str = str.Substring(0, maxLength).Trim();

			// Thay khoảng trắng bằng dấu '-'
			str = Regex.Replace(str, @"\s", "-");

			return str;
		}

		private static string RemoveDiacritics(string text)
		{
			var normalizedString = text.Normalize(NormalizationForm.FormD);
			var stringBuilder = new StringBuilder();

			foreach (var c in normalizedString)
			{
				var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
				if (unicodeCategory != UnicodeCategory.NonSpacingMark)
				{
					stringBuilder.Append(c);
				}
			}

			return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
		}
	}
}