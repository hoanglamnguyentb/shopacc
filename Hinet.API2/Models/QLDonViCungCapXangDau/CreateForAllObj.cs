namespace Hinet.API2.Models.QLDonViCungCapXangDau
{
    public class CreateForAllObj<T>
    {
        public CreateForAllObj(T obj, string typeObj)
        {
            this.obj = obj;
            TypeObj = typeObj;
        }

        public T obj { get; set; }
        public string TypeObj { get; set; }
    }

    public class DemoDto
    {
        public DemoDto(int id, string name, string typeObj)
        {
            Id = id;
            Name = name;
            TypeObj = typeObj;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string TypeObj { get; set; }
    }

    public class Demo
    {
    }
}