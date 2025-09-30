using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace CommonHelper
{
	public static class FileHelper
	{
		public static string SaveUploadedFile(HttpPostedFileBase file, string folderVirtualPath)
		{
            if (file == null || file.ContentLength <= 0)
                return null;

            var folderPath = HttpContext.Current.Server.MapPath(folderVirtualPath);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var originalName = Path.GetFileNameWithoutExtension(file.FileName);
            var ext = Path.GetExtension(file.FileName);

            originalName = ToSafeFileName(originalName);

            var shortGuid = Guid.NewGuid().ToString("N").Substring(0, 8);

            var fileName = $"{originalName}_{shortGuid}{ext.ToLowerInvariant()}";
            var fullPath = Path.Combine(folderPath, fileName);
            file.SaveAs(fullPath);


            return VirtualPathUtility.ToAbsolute(Path.Combine(folderVirtualPath, fileName));
        }

		public static bool DeleteFile(string relativePath)
		{
			if (string.IsNullOrEmpty(relativePath))
				return false;

			try
			{
				var fullPath = HttpContext.Current.Server.MapPath(relativePath);

				if (File.Exists(fullPath))
				{
					File.Delete(fullPath);
					return true;
				}
				return false;
			}
			catch
			{
				// Có thể log lỗi tại đây
				return false;
			}
		}

        public static string ToSafeFileName(string fileName)
        {
            string normalized = fileName.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();
            foreach (var c in normalized)
            {
                var uc = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c);
                if (uc != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }
            string noDiacritics = sb.ToString().Normalize(NormalizationForm.FormC);

            noDiacritics = noDiacritics.ToLowerInvariant();

            noDiacritics = Regex.Replace(noDiacritics, @"\s+", "-");

            noDiacritics = Regex.Replace(noDiacritics, @"[^a-z0-9\-_\.]", "");

            return noDiacritics;
        }

    }
}