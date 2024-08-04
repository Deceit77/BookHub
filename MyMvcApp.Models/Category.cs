using System.ComponentModel.DataAnnotations;

namespace MyMvcApp.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

         [Required]
        [MaxLength(30)]
        public string? Name { get; set; }

        [Range(1, 100)]
        public int DisplayOrder { get; set; }
    }
}
