#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationThesis_CarServices.Models.Entity
{
    public class Review
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int review_id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        [Range(0, 5, ErrorMessage = "")]
        public int rating { get; set; }
        [MaxLength(100)]
        public string content { get; set; }
        public bool is_approved { get; set; }

        // public int user_id { get; set; }
        public virtual User User { get; set; }
        // public int garage_id { get; set; }
        public virtual Garage Garage { get; set; }
    }
}