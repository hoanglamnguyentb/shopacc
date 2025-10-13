using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Model.Entities
{
    public class GianHang : AuditableEntity<int>
    {
        public string Name { get; set; }
        public string MoTa { get; set; }
        public string TrangThai { get; set; }
        public int STT { get; set; }
        public string ViTriHienThi { get; set; }
        public string Slug { get; set; }
        public string AnhBia { get; set; }
    }
}
