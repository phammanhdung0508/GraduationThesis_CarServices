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
            public string? Address { get; set; }
            public string? Ward { get; set; }
            public string? District { get; set; }
            public string? City { get; set; } 
            public double Latitude {get; set;}
            public double Longitude { get; set; }
        }

        public static List<Coordinates> Location = new List<Coordinates>{
            new Coordinates{Address="586-300 Đ. Nguyễn Hữu Thọ", Ward="Phường Tân Hưng", District="Quận 7", City="Thành phố Hồ Chí Minh",
                Latitude=10.73168914204616, Longitude=106.70024433725652},

            new Coordinates{Address="503 Nguyễn Thị Thập", Ward="Phường Tân Quy", District="Quận 7", City="Thành phố Hồ Chí Minh",
                Latitude=10.738659510040932, Longitude=106.71129519557827},

            new Coordinates{Address="12 Trần Trọng Cung", Ward="Phường Tân Thuận Đông", District="Quận 7", City="Thành phố Hồ Chí Minh",
                Latitude=10.743307033696517, Longitude=106.73450715714962},

            new Coordinates{Address="221 Đ. Trần Bình Trọng", Ward="Phường 3", District="Quận 5", City="Thành phố Hồ Chí Minh",
                Latitude=10.759439875644452, Longitude=106.68029816775544},

            new Coordinates{Address="189 Huỳnh Mẫn Đạt", Ward="Phường 8", District="Quận 5", City="Thành phố Hồ Chí Minh",
                Latitude=10.757359962870533, Longitude=106.67581773821539},

            new Coordinates{Address="702 Đ. Số 7", Ward="Phường An Phú", District="Quận 2", City="Thành phố Hồ Chí Minh",
                Latitude=10.799069217729246, Longitude=106.73562710263168},

            new Coordinates{Address="4 Đ. 12", Ward="Phường Bình An", District="Quận 2", City="Thành phố Hồ Chí Minh",
                Latitude=10.792479378284, Longitude=106.73123221851327},

            new Coordinates{Address="71 Nguyễn Văn Lượng", Ward="Phường 10", District="Quận Gò Vấp", City="Thành phố Hồ Chí Minh",
                Latitude=10.835207705112682, Longitude=106.6720760637316},

            new Coordinates{Address="183, Lê Văn Thọ", Ward="Phường 8", District="Quận Gò Vấp", City="Thành phố Hồ Chí Minh",
                Latitude=10.841321738425458, Longitude=106.65707258451964},

            new Coordinates{Address="26 Đường số 21", Ward="Phường 8", District="Quận Gò Vấp", City="Thành phố Hồ Chí Minh",
                Latitude=10.841387111058902, Longitude=106.65122515765769},

            new Coordinates{Address="90 Tân Cảng", Ward="Phường 25", District="Quận Bình Thạnh", City="Thành phố Hồ Chí Minh",
                Latitude=10.801927512862166, Longitude=106.72229777898752},

            new Coordinates{Address="1 Đ. D2", Ward="Phường 25", District="Quận Bình Thạnh", City="Thành phố Hồ Chí Minh",
                Latitude=10.804033806101337, Longitude=106.71477232640146},

            new Coordinates{Address="922 Đ. Phạm Văn Đồng", Ward="Hiệp Bình Chánh", District="Quận Thủ Đức", City="Thành phố Hồ Chí Minh",
                Latitude=10.826798838672856, Longitude=106.71881104013613},

            new Coordinates{Address="37 Đường Số 13", Ward="Hiệp Bình Chánh", District="Quận Thủ Đức", City="Thành phố Hồ Chí Minh",
                Latitude=10.82917875719101, Longitude=106.73048435557894},

            new Coordinates{Address="533 Đ. Kha Vạn Cân", Ward="Linh Đông", District="Quận Thủ Đức", City="Thành phố Hồ Chí Minh",
                Latitude=10.846209915188018, Longitude=106.75062756251548}
        };
    }
}