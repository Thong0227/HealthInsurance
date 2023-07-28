using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace HealthInsurance.Models
{
    public class Policy
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Name must be between 6 and 100 characters")]
        public string Name { get; set; } = "";

        [StringLength(500,ErrorMessage = "Description must be not over 500 characters")]
        [AllowHtml]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Emi is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Emi must be a positive value")]
        public decimal Emi { get; set; }

        [Required(ErrorMessage = "Amount is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Amount must be a positive value")]
        public decimal Amount { get; set; }

        public string? Image { get; set; }

        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; } = "";

        [ForeignKey("Hospital")]
        public int HospitalId { get; set; }

        public Hospital? Hospital { get; set; }

        public ICollection<PolicyOnEmp>? PolicyOnEmps { get; set; }
    }
}
