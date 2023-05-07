using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HealthInsurance.Models
{
    public class Policy
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public decimal Emi { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public string Image { get; set; }

        [ForeignKey("Hospital")]
        public int HospitalId { get; set; }

        public Hospital Hospital { get; set; }

        public ICollection<PolicyOnEmp> PolicyOnEmps { get; set; }
    }
}
