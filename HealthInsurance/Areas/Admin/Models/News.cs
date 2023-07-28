using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace HealthInsurance.Areas.Admin.Models
{
    public class News
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Title must be between 6 and 100 characters")]
        public string Title { get; set; } = "";

        [StringLength(500, ErrorMessage = "Description must not be over 500 characters")]
        [AllowHtml]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; } = "";
 
        public string? Image { get; set; }
    }
}
