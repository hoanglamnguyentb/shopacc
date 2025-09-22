namespace Hinet.Service.Constant
{
    public class HSSKConstant
    {
        public const int GIOITINH_NAM = 1;
        public const int GIOITINH_NU = 2;

        /// <summary>
        /// Mã nhóm danh mục phân loại sức khỏe
        /// </summary>
        public const string PHANLOAI_SUCKHOE = "PHANLOAI_SUCKHOE";

        /// <summary>
        /// Mã nhóm danh mục kết quả xét nghiệm
        /// </summary>
        public const string KEQUA_XETNGHIEM = "KEQUA_XETNGHIEM";

        public const string AM_TINH = "AM_TINH";
        public const string DUONG_TINH = "DUONG_TINH";

        public const string MA_BENH = "MA_BENH";

        public const string LOAI_1_DM = "Loại 1";
        public const string LOAI_2_DM = "Loại 2";
        public const string LOAI_3_DM = "Loại 3";
        public const string LOAI_4_DM = "Loại 4";
        public const string LOAI_5_DM = "Loại 5";

        public const string LOAI_1_CODE = "LOAI_1";
        public const string LOAI_2_CODE = "LOAI_2";
        public const string LOAI_3_CODE = "LOAI_3";
        public const string LOAI_4_CODE = "LOAI_4";
        public const string LOAI_5_CODE = "LOAI_5";

        public const int LOAI_1 = 1;
        public const int LOAI_2 = 2;
        public const int LOAI_3 = 3;
        public const int LOAI_4 = 4;
        public const int LOAI_5 = 5;

        public static int GetPhanLoaiCode(string code)
        {
            if (!string.IsNullOrEmpty(code))
            {
                code = code.ToLower();
                if (code == LOAI_1_CODE.ToLower())
                {
                    return LOAI_1;
                }
                if (code == LOAI_2_CODE.ToLower())
                {
                    return LOAI_2;
                }
                if (code == LOAI_3_CODE.ToLower())
                {
                    return LOAI_3;
                }
                if (code == LOAI_4_CODE.ToLower())
                {
                    return LOAI_4;
                }
                if (code == LOAI_5_CODE.ToLower())
                {
                    return LOAI_5;
                }
                return 0;
            }
            return 0;
        }

        public static int GetPhanLoai(string phanLoai)
        {
            if (!string.IsNullOrEmpty(phanLoai))
            {
                phanLoai = phanLoai.ToLower();
                if (phanLoai == LOAI_1_DM.ToLower())
                {
                    return LOAI_1;
                }
                if (phanLoai == LOAI_2_DM.ToLower())
                {
                    return LOAI_2;
                }
                if (phanLoai == LOAI_3_DM.ToLower())
                {
                    return LOAI_3;
                }
                if (phanLoai == LOAI_4_DM.ToLower())
                {
                    return LOAI_4;
                }
                if (phanLoai == LOAI_5_DM.ToLower())
                {
                    return LOAI_5;
                }
                return 0;
            }
            return 0;
        }
    }
}