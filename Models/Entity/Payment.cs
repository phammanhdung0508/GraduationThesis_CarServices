using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationThesis_CarServices.Models.Entity
{
    public class Payment
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id {get; set;}
        public int BookingId {get; set;}
        public int CarId {get; set;}
        public string PaymentId {get; set;} = "N/A";
        public string PaymentContent {get; set;} = "N/A";
        public string PaymentCurrency {get; set;} = "VND";
        public string PaymentRefId {get; set;} = "N/A";
        public int? PaymentStatus {get; set;}
        [Column(TypeName = "decimal(10,3)")]
        public decimal? RequiredAmount {get; set;}
        public string? PaymentLanguage {get; set;} = "vn";
        public DateTime? PaymnetDate {get; set;}
        public DateTime? ExpireDate {get; set;}
    }
}