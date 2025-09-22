using Hinet.Service.Common;
using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class TypeChamCongConstant
    {
        [DisplayName("Đi làm")]
        [Color(BgColor = "#4CAF50")]
        public static string DiLam { get; set; } = "X";

        [DisplayName("Làm đêm")]
        [Color(BgColor = "#4CAF50")]
        public static string LamDem { get; set; } = "Đ";

        [DisplayName("Làm ngoài giờ")]
        [Color(BgColor = "#4CAF50")]
        public static string DiLamNgoaiGio { get; set; } = "OT";

        //[DisplayName("Làm thứ 7")]
        //[Color(BgColor = "#4CAF50")]
        //public static string L7 { get; set; } = "L7";
        [DisplayName("Phép")]
        [Color(BgColor = "#4CAF50")]
        public static string Phep { get; set; } = "P";

        [DisplayName("Trực khoa")]
        [Color(BgColor = "#4CAF50")]
        public static string TrucKhoa { get; set; } = "TK";

        [DisplayName("Trực hồi sức")]
        [Color(BgColor = "#4CAF50")]
        public static string TrucHoiSuc { get; set; } = "THS";

        [DisplayName("Trực thường trú")]
        [Color(BgColor = "#4CAF50")]
        public static string TrucThuongTru { get; set; } = "TTT";

        [DisplayName("Nghỉ bù")]
        [Color(BgColor = "#4CAF50")]
        public static string NghiBu { get; set; } = "NB";

        [DisplayName("Ốm")]
        [Color(BgColor = "#4CAF50")]
        public static string Om { get; set; } = "Ô";

        [DisplayName("Nghỉ không lý do")]
        [Color(BgColor = "#f44336")]
        public static string NghiKLyDo { get; set; } = "NX";

        [DisplayName("Nghỉ có lý do")]
        [Color(BgColor = "#f44336")]
        public static string NghiCoLyDo { get; set; } = "N";

        [DisplayName("Nghỉ không lương")]
        [Color(BgColor = "#4CAF50")]
        public static string NghiKLuong { get; set; } = "KL";

        [DisplayName("Nghỉ việc riêng")]
        [Color(BgColor = "#4CAF50")]
        public static string NghiViecRieng { get; set; } = "VR";

        [DisplayName("Thai sản")]
        [Color(BgColor = "#4CAF50")]
        public static string NghiDe { get; set; } = "TS";

        [DisplayName("Khám Thai")]
        [Color(BgColor = "#f1c404CAF50f")]
        public static string KhamThai { get; set; } = "KT";

        [DisplayName("Học")]
        [Color(BgColor = "#4CAF50")]
        public static string Hoc { get; set; } = "H";

        [DisplayName("Hội thảo")]
        [Color(BgColor = "#4CAF50")]
        public static string HoiThao { get; set; } = "HT";

        [DisplayName("Quân sự")]
        [Color(BgColor = "#4CAF50")]
        public static string QuanSu { get; set; } = "QS";

        [DisplayName("Con Ốm")]
        [Color(BgColor = "#4CAF50")]
        public static string ConOm { get; set; } = "CÔ";

        [DisplayName("Đi Khám tuyến")]
        [Color(BgColor = "#4CAF50")]
        public static string KhamTuyen { get; set; } = "ĐT";

        [DisplayName("Khám gói")]
        [Color(BgColor = "#4CAF50")]
        public static string KhamGoi { get; set; } = "KG";

        [DisplayName("Phòng dịch")]
        [Color(BgColor = "#4CAF50")]
        public static string PhongDich { get; set; } = "PD";

        [DisplayName("Công tác")]
        [Color(BgColor = "#4CAF50")]
        public static string CongTac { get; set; } = "CT";

        [DisplayName("Nghỉ mát")]
        [Color(BgColor = "#4CAF50")]
        public static string NghiMat { get; set; } = "NM";

        [DisplayName("Nghỉ Lễ tết")]
        [Color(BgColor = "#4CAF50")]
        public static string NghiLeTet { get; set; } = "L";

        [DisplayName("Nghỉ bù T7 CN")]
        [Color(BgColor = "#4CAF50")]
        public static string NghiBuT7CN { get; set; } = "b";
    }

    public class TypeNghiPhepConstant
    {
        [DisplayName("Phép")]
        [Color(BgColor = "#f1c40f")]
        public static string Phep { get; set; } = "P";

        [DisplayName("Ốm")]
        [Color(BgColor = "#f1c40f")]
        public static string Om { get; set; } = "Ô";

        [DisplayName("Nghỉ không lý do")]
        [Color(BgColor = "#f1c40f")]
        public static string NghiKLyDo { get; set; } = "NX";

        [DisplayName("Nghỉ có lý do")]
        [Color(BgColor = "#f44336")]
        public static string NghiCoLyDo { get; set; } = "N";

        [DisplayName("Nghỉ không lương")]
        [Color(BgColor = "#f1c40f")]
        public static string NghiKLuong { get; set; } = "KL";

        [DisplayName("Nghỉ việc riêng")]
        [Color(BgColor = "#f1c40f")]
        public static string NghiViecRieng { get; set; } = "VR";

        [DisplayName("Nghỉ đẻ")]
        [Color(BgColor = "#f1c40f")]
        public static string NghiDe { get; set; } = "TS";

        [DisplayName("Khám Thai")]
        [Color(BgColor = "#f1c40f")]
        public static string KhamThai { get; set; } = "KT";

        [DisplayName("Học")]
        [Color(BgColor = "#f1c40f")]
        public static string Hoc { get; set; } = "H";

        [DisplayName("Hội thảo")]
        [Color(BgColor = "#f1c40f")]
        public static string HoiThao { get; set; } = "HT";

        [DisplayName("Quân sự")]
        [Color(BgColor = "#f1c40f")]
        public static string QuanSu { get; set; } = "QS";

        [DisplayName("Con Ốm")]
        [Color(BgColor = "#f1c40f")]
        public static string ConOm { get; set; } = "CÔ";

        [DisplayName("Đi Khám tuyến")]
        [Color(BgColor = "#f1c40f")]
        public static string KhamTuyen { get; set; } = "ĐT";

        [DisplayName("Khám gói")]
        [Color(BgColor = "#4CAF50")]
        public static string KhamGoi { get; set; } = "KG";

        [DisplayName("Nghỉ bù")]
        [Color(BgColor = "#f1c40f")]
        public static string NghiBu { get; set; } = "NB";

        [DisplayName("Công tác")]
        [Color(BgColor = "#f1c40f")]
        public static string CongTac { get; set; } = "CT";

        [DisplayName("Nghỉ mát")]
        [Color(BgColor = "#f1c40f")]
        public static string NghiMat { get; set; } = "NM";

        [DisplayName("Nghỉ Lễ Tết")]
        [Color(BgColor = "#4CAF50")]
        public static string NghiLeTet { get; set; } = "L";

        [DisplayName("Nghỉ bù T7 CN")]
        [Color(BgColor = "#4CAF50")]
        public static string NghiBuT7CN { get; set; } = "b";
    }
}