using System.ComponentModel.DataAnnotations;

namespace Mission6_Williams.Models
{
    public class Category
    {
        [Key]
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; }
    }
}