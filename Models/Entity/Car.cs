#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationThesis_CarServices.Models.Entity
{
    public class Car
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int CarId { get; set; }
        [MaxLength(20)]
        public string CarModel { get; set; }
        [MaxLength(20)]
        public string CarBrand { get; set; }
        [MaxLength(10)]
        public string CarLicensePlate { get; set; }
        [Column(TypeName = "smallint")]
        public int CarYear { get; set; }
        [MaxLength(20)]
        public string CarBodyType { get; set; }
        [MaxLength(20)]
        public string CarFuelType { get; set; }
        [Column(TypeName = "tinyint")]
        public int CarStatus {get; set;}
        public Nullable<DateTime> CreatedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }

        /*-------------------------------------------------*/
        public int UserId { get; set; }
        public virtual User User { get; set; }

        /*-------------------------------------------------*/
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}