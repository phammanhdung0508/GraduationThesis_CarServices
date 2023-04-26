using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationThesis_CarServices.Randomly
{
    public class RandomConfiguration
    {
        public static string[] Paymethod = new string[] { "Momo", "Cast", "Visa"};

        public static string[] Districts = new string[] { "Quận 1", "Quận 3", "Quận 4", "Quận 5",
            "Quận 6", "Quận 7", "Quận 8", "Quận 10", "Quận 11", "Quận 12", "Quận Bình Thạnh",
            "Quận Gò Vấp", "Quận Phú Nhuận", "Quận Tân Bình", "Quận Tân Phú"};

        public static string[]? check(string district)
        {
            switch (district)
            {
                case "Quận 1":
                    string[] quan1 = new string[] { "Phường Bến Nghé", "Phường Bến Thành", "Phường Cầu Kho",
        "Phường Cầu Ông Lãnh", "Phường Cô Giang", "Phường Đa Kao", "Phường Nguyễn Cư Trinh", "Phường Nguyễn Cư Trinh",
        "Phường Phạm Ngũ Lão", "Phường Tân Định"};
                    return quan1;
                case "Quận 3":
                    string[] quan3 = new string[] { "Phường 01", "Phường 02", "Phường 03",
        "Phường 04", "Phường 05", "Phường Võ Thị Sáu", "Phường 09", "Phường 10",
        "Phường 11", "Phường 12", "Phường 13", "Phường 14"};
                    return quan3;
                case "Quận 4":
                    string[] quan4 = new string[] { "Phường 01", "Phường 02", "Phường 03",
        "Phường 04", "Phường 06", "Phường 08", "Phường 09", "Phường 10",
        "Phường 13", "Phường 14", "Phường 15", "Phường 16"};
                    return quan4;
                case "Quận 5":
                    string[] quan5 = new string[] { "Phường 01", "Phường 02", "Phường 03",
        "Phường 04", "Phường 05", "Phường 06", "Phường 07", "Phường 08",
        "Phường 09", "Phường 10", "Phường 11", "Phường 12", "Phường 13", "Phường 14"};
                    return quan5;
                case "Quận 6":
                    string[] quan6 = new string[] { "Phường 01", "Phường 02", "Phường 03",
        "Phường 04", "Phường 05", "Phường 06", "Phường 07", "Phường 08",
        "Phường 09", "Phường 10", "Phường 11", "Phường 12", "Phường 13", "Phường 14"};
                    return quan6;
                case "Quận 7":
                    string[] quan7 = new string[] { "Phường Bình Thuận", "Phường Phú Mỹ", "Phường Phú Thuận",
        "Phường Tân Hưng", "Phường Tân Kiểng", "Phường Tân Phong", "Phường Tân Phú", "Phường Tân Quy",
        "Phường Tân Thuận Đông", "Phường Tân Thuận Đông"};
                    return quan7;
                case "Quận 8":
                    string[] quan8 = new string[] { "Phường 01", "Phường 02", "Phường 03",
        "Phường 04", "Phường 05", "Phường 06", "Phường 07", "Phường 08",
        "Phường 09", "Phường 10", "Phường 11", "Phường 12", "Phường 13", "Phường 14"};
                    return quan8;
                case "Quận 10":
                    string[] quan10 = new string[] { "Phường 01", "Phường 02", "Phường 03",
        "Phường 04", "Phường 05", "Phường 06", "Phường 07", "Phường 08",
        "Phường 09", "Phường 10", "Phường 11", "Phường 12", "Phường 13", "Phường 14"};
                    return quan10;
                case "Quận 11":
                    string[] quan11 = new string[] { "Phường 01", "Phường 02", "Phường 03",
        "Phường 04", "Phường 05", "Phường 06", "Phường 07", "Phường 08",
        "Phường 09", "Phường 10", "Phường 11", "Phường 12", "Phường 13", "Phường 14"};
                    return quan11;
                case "Quận 12":
                    string[] quan12 = new string[] { "Phường An Phú Đông", "Phường Đông Hưng Thuận", "Phường Hiệp Thành",
        "Phường Tân Chánh Hiệp", "Phường Tân Hưng Thuận", "Phường Tân Thới Hiệp", "Phường Tân Thới Nhất", "Phường Tân Thới Nhất",
        "Phường Thạnh Xuân", "Phường Thạnh Xuân", "Phường Thạnh Xuân"};
                    return quan12;
                case "Quận Bình Thạnh":
                    string[] quanbinhthanh = new string[] { "Phường 01", "Phường 02", "Phường 03",
        "Phường 04", "Phường 05", "Phường 06", "Phường 07", "Phường 08",
        "Phường 09", "Phường 10", "Phường 11", "Phường 12", "Phường 13", "Phường 14", "Phường 15",
        "Phường 17"};
                    return quanbinhthanh;
                case "Quận Gò Vấp":
                    string[] quangovap = new string[] { "Phường 01", "Phường 02", "Phường 03",
        "Phường 04", "Phường 05", "Phường 06", "Phường 07", "Phường 08",
        "Phường 09", "Phường 10", "Phường 11", "Phường 12", "Phường 13", "Phường 14", "Phường 15",
        "Phường 16", "Phường 17"};
                    return quangovap;
                case "Quận Phú Nhuận":
                    string[] quanphunhuan = new string[] { "Phường 01", "Phường 02", "Phường 03",
        "Phường 04", "Phường 05", "Phường 06", "Phường 07", "Phường 08",
        "Phường 09", "Phường 10", "Phường 11", "Phường 12", "Phường 13", "Phường 14", "Phường 15",
        "Phường 17"};
                    return quanphunhuan;
                case "Quận Tân Bình":
                    string[] quantanbinh = new string[] { "Phường 01", "Phường 02", "Phường 03",
        "Phường 04", "Phường 05", "Phường 06", "Phường 07", "Phường 08",
        "Phường 09", "Phường 10", "Phường 11", "Phường 12", "Phường 13", "Phường 14", "Phường 15"};
                    return quantanbinh;
                case "Quận Tân Phú":
                    string[] quantanphu = new string[] { "Phường Hiệp Tân", "Phường Hoà Thạnh", "Phường Phú Thạnh",
        "Phường Phú Thọ Hoà", "Phường Phú Trung", "Phường Sơn Kỳ", "Phường Tân Qúy", "Phường Tân Sơn Nhì",
        "Phường Tân Thành", "Phường Tân Thới Hoà", "Phường Tây Thạnh"};
                    return quantanphu;
                default:
                    return null;
            }
        }
    }
}