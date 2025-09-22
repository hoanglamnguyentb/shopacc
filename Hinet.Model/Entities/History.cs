using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hinet.Model.Entities
{
    [Table("History")]
    public class History : AuditableEntity<long>
    {
        //Tiêu đề lịch sử
        public string HistoryContent { get; set; }

        //Id của người tạo lịch sử
        public long? LogId { get; set; }

        //Id của hồ sơ được nhận lịch sử
        public long IdItem { get; set; }

        //Loại hồ sơ
        [StringLength(255)]
        public string TypeItem { get; set; }

        //Ghi chú
        [Column(TypeName = "nvarchar")]
        public string Note { get; set; }

        //Nội dung lịch sử
        [Column(TypeName = "ntext")]
        public string Comment { get; set; }

        //Trạng thái trước
        [StringLength(255)]
        public string StatusBegin { get; set; }

        //Trạng thái sau
        [StringLength(255)]
        public string StatusEnd { get; set; }

        //Tên của lịch sử
        [StringLength(255)]
        public string Name { get; set; }
    }
}