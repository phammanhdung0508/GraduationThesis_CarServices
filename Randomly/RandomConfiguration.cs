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

        public class Coordinates{
            public double Latitude {get; set;}
            public double Longitude { get; set; }
        }

        public static List<Coordinates> Location = new List<Coordinates>{
            new Coordinates{Latitude=10.763931646166899, Longitude=106.69564615504434},
            new Coordinates{Latitude=10.765357983335225, Longitude=106.67540191456098},
            new Coordinates{Latitude=10.745556918903318, Longitude=106.68952712823149},
            new Coordinates{Latitude=10.753202537046564, Longitude=106.71243370657636},
            new Coordinates{Latitude=10.770846334985222, Longitude=106.67838516123628},
            new Coordinates{Latitude=10.758837040486794, Longitude=106.67807461014627},
            new Coordinates{Latitude=10.771472907894179, Longitude=106.68604786007458},
            new Coordinates{Latitude=10.75664665679407, Longitude=106.70010757315116},
            new Coordinates{Latitude=10.753192970720434, Longitude=106.71245825706636},
            new Coordinates{Latitude=10.754790795344721, Longitude=106.66072405510118},
            new Coordinates{Latitude=10.754372579836698, Longitude=106.6738731901177},
            new Coordinates{Latitude=10.75887258385469, Longitude=106.67805637713498},
            new Coordinates{Latitude=10.76068396559795, Longitude=106.6827757752482},
            new Coordinates{Latitude=10.764712983167666, Longitude=106.68116218105035},
            new Coordinates{Latitude=10.745572022846487, Longitude=106.6270524477263},
            new Coordinates{Latitude=10.739311649701769, Longitude=106.62344204785116},
            new Coordinates{Latitude=10.732255336778813, Longitude=106.62501618972078},
            new Coordinates{Latitude=10.745749183168783, Longitude=106.62491780586315},
            new Coordinates{Latitude=10.756668659179981, Longitude=106.62503962906366},
            new Coordinates{Latitude=10.73132027408366, Longitude=106.70024536416858},
            new Coordinates{Latitude=10.738295740959048, Longitude=106.72743149396123},
            new Coordinates{Latitude=10.727699818257554, Longitude=106.73705857706113},
            new Coordinates{Latitude=10.713318564751694, Longitude=106.74350818181838},
            new Coordinates{Latitude=10.841384334152652, Longitude=106.65709150138284},
            new Coordinates{Latitude=10.82631923315733, Longitude=106.73578317034169},
            new Coordinates{Latitude=10.763931646166899, Longitude=106.69564615504434}
        };
    }
}