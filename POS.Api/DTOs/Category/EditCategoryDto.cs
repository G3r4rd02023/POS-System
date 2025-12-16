using System.ComponentModel.DataAnnotations;

namespace POS.Shared.DTOs.Category
{
    public class EditCategoryDto
    {
        [Required(ErrorMessage = "The Name field is required.")]
        [MaxLength(100, ErrorMessage = "The Name field must be at most {1} characters.")]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500, ErrorMessage = "The Description field must be at most {1} characters.")]
        public string Description { get; set; } = string.Empty;
    }
}
