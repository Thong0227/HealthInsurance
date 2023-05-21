using System.ComponentModel.DataAnnotations;

namespace HealthInsurance.Models
{
    public class Hospital
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        [Required]
        [StringLength(30)]
        public string? Phone { get; set; }

        [Required]
        [StringLength(200)]
        public string? Location { get; set; }

        public ICollection<Policy>? Policies { get; set; }
    }
}
