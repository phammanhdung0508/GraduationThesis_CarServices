using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationThesis_CarServices.Randomly
{
    public class RandomConfiguration
    {
        public static string[] Districts = new string[] { "Quận 1", "Quận 3"};
        // public static string[] Districts = new string[] { "Quận 1", "Quận 3", "Quận 4", "Quận 5",
        //     "Quận 6", "Quận 7", "Quận 8", "Quận 10", "Quận 11", "Quận 12", "Quận Bình Tân", "Quận Bình Tân",
        //     "Quận Gò Vấp", "Quận Phú Nhuận", "Quận Tân Bình", "Quận Tân Phú"};

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
                    string[] quan3 = new string[] { "Phường 01", "Phường 01", "Phường 01",
        "Phường 01", "Phường 01", "Phường Võ Thị Sáu", "Phường 09", "Phường 10",
        "Phường 11", "Phường 12", "Phường 12", "Phường 12"};
                    return quan3;
                default:
                    return null;
            }
        }
    }
}