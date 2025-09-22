namespace Hinet.Service.APIModel
{
    public class TheoDoiGiaiNganChuDauTuAM
    {
        public int TongSoDuAnCoKeHoach { get; set; }
        public int TongSoDuAnDaGiaiNgan { get; set; }
        public int TongSoDuAnDaThucHien { get; set; }

        public decimal TyLeGiaiNgan
        {
            get
            {
                if (TongSoDuAnCoKeHoach > 0)
                {
                    return TongSoDuAnDaGiaiNgan / TongSoDuAnCoKeHoach;
                }
                return 0;
            }
        }

        public decimal TyLeThucHien
        {
            get
            {
                if (TongSoDuAnCoKeHoach > 0)
                {
                    return TongSoDuAnDaThucHien / TongSoDuAnCoKeHoach;
                }
                return 0;
            }
        }

        public decimal GiaTriGiaiNgan { get; set; }
        public string TinhHinhTyLeGiaiNgan { get; set; }
        public decimal ThongKeGiaTriThucHien { get; set; }
    }
}