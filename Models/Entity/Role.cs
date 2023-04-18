#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationThesis_CarServices.Models.Entity
{
    public class Role
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int role_id { get; set; }
        [MaxLength(10)]
        public string role_name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}