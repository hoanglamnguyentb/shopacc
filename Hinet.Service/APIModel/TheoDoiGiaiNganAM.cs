namespace Hinet.Service.APIModel
{
    public class TheoDoiGiaiNganAM
    {
        public int TongSoDonViDuocGiaiNgan { get; set; }
        public int TongSoDuAnDuocGiaiNgan { get; set; }
        public decimal TongGiaTriThucHien { get; set; }
        public decimal TongGiaTriGiaiNgan { get; set; }

        public decimal TyLeGiaiNgan
        {
            get
            {
                if (TongGiaTriThucHien > 0)
                {
                    return TongGiaTriGiaiNgan / TongGiaTriThucHien;
                }
                return 0;
            }
        }

        public int TuongQuanGiaiThucHien { get; set; }
    }
}