using Hinet.Model.Entities;
using System.Collections.Generic;

namespace Hinet.Web.Areas.DepartmentArea.Models
{
    public class DepartmentDetailVM
    {
        public Department department { get; set; }
        public List<Module> modules { get; set; }
    }
}