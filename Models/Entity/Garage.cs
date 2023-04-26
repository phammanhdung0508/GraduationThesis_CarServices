#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationThesis_CarServices.Models.Entity
{
    public class Garage
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int GarageId { get; set; }
        [MaxLength(20)]
        public string GarageName { get; set; }
        [MaxLength(20)]
        public string GarageContactInformation { get; set; }
        [MaxLength(100)]
        public string GarageAbout { get; set; }
        [MaxLength(20)]
        public string GarageAddress { get; set; }
        [MaxLength(20)]
        public string GarageWard { get; set; }
        [MaxLength(20)]
        public string GarageDistrict { get; set; }
        [MaxLength(20)]
        public string GarageCity { get; set; }
        [MaxLength(20)]
        public string FromTo { get; set; }
        [MaxLength(6)]
        public string OpenAt { get; set; }
        [MaxLength(6)]
        public string CloseAt { get; set; }
        public bool GarageStatus { get; set; }
        public Nullable<DateTime> CreatedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }

        /*-------------------------------------------------*/
        public int UserId { get; set; }
        public virtual User User { get; set; }

        /*-------------------------------------------------*/
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<ServiceGarage> ServiceGarages { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}