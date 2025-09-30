namespace Hinet.API2.Models
{
    public class CreateGroupUserVM
    {
        public long Id { get; set; }
        public long IdUserGroup { get; set; }
        public string TypeUser { get; set; }
        public long IdUser { get; set; }
    }
}