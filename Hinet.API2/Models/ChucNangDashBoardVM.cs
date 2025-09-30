using System.Collections.Generic;

namespace Hinet.API2.Models
{
    public class ChucNangDashBoardVM
    {
        public int SttWiget { get; set; }
        public string TitleWiget { get; set; }
        public bool LinkViewAll { get; set; } = false;
        public int HeightSlide { get; set; }
        public long SlideTocDo { get; set; }
        public List<LstItemWiget> LstItemWiget { get; set; }
        public int CountItem { get; set; }
        public string BlockType { get; set; }
    }

    public class LstItemWiget
    {
        public bool IsHienThiChuaLogin { get; set; }
        public bool IsWebview { get; set; }
        public bool IsUpdate { get; set; }
        public string ImgItem { get; set; }
        public string TitleItem { get; set; }
        public string LinkDanhSach { get; set; }
        public string LinkItem { get; set; }
        public int SttItem { get; set; }
        //public long? IdDoanhNghiep { get; set; }
    }
}