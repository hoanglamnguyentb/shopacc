using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class MessageErrorsConstant
    {
        [DisplayName("Website bị từ chối")]
        public int WebsiteStatusBiTuChoi { get; set; } = 4;

        [DisplayName("Cập nhật không thành công")]
        public string UpdateFail { get; set; } = "UpdateFail";

        [DisplayName("Một số trường thông tin còn thiếu")]
        public string Missing { get; set; } = "Missing";
    }
}