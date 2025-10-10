using Hinet.Model.Entities;
using System.Collections.Generic;

namespace Hinet.Service.ModuleService.DTO
{
    public class ModuleMenuDTO : Module
    {
        public List<Operation> ListOperation { get; set; }
    }

    #region Object hứng module cho Mobile

    public class MobileModule
    {
        public string name { get; set; }
        public int order { get; set; }
        public List<MobileOperation> listFeature { get; set; }
    }

    public class MobileOperation
    {
        public string name { get; set; }
        public string url { get; set; }
        public string image { get; set; }
    }

    #endregion Object hứng module cho Mobile
}