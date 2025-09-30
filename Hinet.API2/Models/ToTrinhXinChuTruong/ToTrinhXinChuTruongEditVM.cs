using Hinet.API2.Core;
using System;

namespace Hinet.API2.Models.ToTrinhXinChuTruong
{
    ///<Summary>
    /// Yêu cầu giải ngân Edit
    ///</Summary>
    public class ToTrinhXinChuTruongEditVM
    {
        ///<Summary>
        /// Id tờ trình xin chủ trương
        ///</Summary>
        public long Id { get; set; }

        /// <summary>
        /// Số hiệu
        /// </summary>
        public string SoHieu { get; set; }

        /// <summary>
        /// Tiêu đề
        /// </summary>
        public string TieuDe { get; set; }

        /// <summary>
        /// Nội dung
        /// </summary>
        public string NoiDung { get; set; }

        /// <summary>
        /// Ngày trình
        /// </summary>
        public DateTime? NgayTrinh { get; set; }

        ///<Summary>
        /// Tài liệu đính kèm
        ///</Summary>
        public FileDataFromClient TaiLieuDinhKemData { get; set; }
    }
}