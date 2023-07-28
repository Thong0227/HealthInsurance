using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BCryptNet = BCrypt.Net.BCrypt;
using Microsoft.AspNetCore.Identity;
//using HealthInsurance.Migrations;

namespace HealthInsurance.Models
{
    public class Employee
    {
       
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Username must be between 6 and 100 characters")]
        public string UserName { get; set; } = "";
        [Required(ErrorMessage = "Password is required")]
        [StringLength(200, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 200 characters")]
        public string Password { get; set; } = "";
        [Required(ErrorMessage = "FullName is required")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "FullName must be between 6 and 50 characters")]
        public string FullName { get; set; } = "";

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Username is required")]
        [StringLength(200, MinimumLength = 10, ErrorMessage = "Username must be between 10 and 200 characters")]
        public string Address { get; set; } = "";

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime JoinDate { get; set; }

        [Required]
        public bool PolicyStatus { get; set; }
        [Required]
        public string UserRole { get; set; } = "";
        public ICollection<PolicyOnEmp>? PolicyOnEmps { get; set; }
    }
}
