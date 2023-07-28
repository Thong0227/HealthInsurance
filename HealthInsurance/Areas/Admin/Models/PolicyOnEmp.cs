using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HealthInsurance.Models
{
    public class PolicyOnEmp
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Policy")]
        public int PolicyId { get; set; }

        public Policy? Policy { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        
        public Employee? Employee { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "EmiSubmitted must be a positive value")]
        public decimal? EmiSubmitted { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        public bool PolicyStatus { get; set; }
    }
}
