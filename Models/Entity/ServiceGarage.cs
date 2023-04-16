#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarServices.Models.Entity
{
    public class ServiceGarage
    {
        public int service_garages_id { get; set; }
        public float lot_number { get; set; }
        public float lot_status { get; set; }

        public int garage_id { get; set; }
        public virtual Garage Garage { get; set; }
        public int service_id { get; set; }
        public virtual Service Service { get; set; }
    }
}