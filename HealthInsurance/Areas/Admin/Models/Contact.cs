using System.ComponentModel.DataAnnotations;

namespace HealthInsurance.Areas.Admin.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "FullName is required")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "FullName must be between 6 and 100 characters")]
        public string FullName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^\+?\d{10,13}$", ErrorMessage = "Invalid phone number")]
        public string? Phone { get; set; }

        [StringLength(50, ErrorMessage = "Subject cannot exceed 200 characters")]
        public string? Subject { get; set; }

        [StringLength(500, ErrorMessage = "Message cannot exceed 200 characters")]
        public string? Message { get; set; }
    }
}
