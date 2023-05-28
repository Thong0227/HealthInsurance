using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace HealthInsurance.Areas.Admin.Models
{
    public class News
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Title { get; set; }

        [StringLength(500)]
        [AllowHtml]
        public string? Description { get; set; }

        public string? Content { get; set; }

        public string Image { get; set; }
    }
}
