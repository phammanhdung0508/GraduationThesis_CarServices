#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarServices.Models.Entity
{
    public class Role
    {
        public int role_id { get; set; }
        public string role_name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}