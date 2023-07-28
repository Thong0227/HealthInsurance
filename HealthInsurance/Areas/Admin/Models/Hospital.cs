using System.ComponentModel.DataAnnotations;

namespace HealthInsurance.Models
{
    public class Hospital
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Name must be between 6 and 100 characters")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^\+?\d{10,13}$", ErrorMessage = "Invalid phone number")]
        public string Phone { get; set; } = "";

        [Required(ErrorMessage = "Location is required")]
        [StringLength(200, MinimumLength = 6, ErrorMessage = "Location must be between 6 and 200 characters")]
        public string Location { get; set; } = "";

        public ICollection<Policy>? Policies { get; set; }
    }
}
