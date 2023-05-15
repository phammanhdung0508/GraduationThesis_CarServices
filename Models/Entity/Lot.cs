#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GraduationThesis_CarServices.Enum;

namespace GraduationThesis_CarServices.Models.Entity
{
    public class Lot
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int LotId { get; set; }
        public string LotNumber { get; set; }
        [Column(TypeName = "tinyint")]
        public LotStatus LotStatus { get; set; }

        /*-------------------------------------------------*/
        public Nullable<int> GarageId { get; set; }
        public virtual Garage Garage { get; set; }
    }
}