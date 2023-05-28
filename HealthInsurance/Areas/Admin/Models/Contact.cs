using System.ComponentModel.DataAnnotations;

namespace HealthInsurance.Areas.Admin.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FullName { get; set; }

        [StringLength(30)]
        public string Email { get; set; }

        [Required]
        [StringLength(30)]
        public string? Phone { get; set; }

        [StringLength(50)]
        public string? Subject { get; set; }

        [StringLength(500)]
        public string? Message { get; set; }
    }
}
