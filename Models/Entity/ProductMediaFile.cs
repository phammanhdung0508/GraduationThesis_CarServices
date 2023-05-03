#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationThesis_CarServices.Models.Entity
{
    public class ProductMediaFile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ProductMediaFileId { get; set; }

        /*-------------------------------------------------*/
        public Nullable<int> ProductId { get; set; }
        public virtual Product Product { get; set; }
        public Nullable<int> MediaFileId { get; set; }
        public virtual MediaFile MediaFile { get; set; }
    }
}