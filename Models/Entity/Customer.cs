#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationThesis_CarServices.Models.Entity
{
    public class Customer
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }
        [MaxLength(100)]
        public string CustomerAddress { get; set; }
        [MaxLength(100)]
        public string CustomerWard { get; set; }
        [MaxLength(100)]
        public string CustomerDistrict { get; set; }
        [MaxLength(100)]
        public string CustomerCity { get; set; }
        public Nullable<double> CustomerLatitude { get; set; } = 0;
        public Nullable<double> CustomerLongitude { get; set; } = 0;

        /*-------------------------------------------------*/
        public virtual User User { get; set; }

        /*-------------------------------------------------*/
        public virtual ICollection<Car> Cars { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}