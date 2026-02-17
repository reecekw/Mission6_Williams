using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mission6_Williams.Models
{
    public class Movie
    {
        [Key]
        [Required]
        public int MovieId { get; set; }

        // Foreign Key Relationship
        [Required]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [Range(1888, int.MaxValue, ErrorMessage = "Year must be 1888 or later.")] // Mission 7 requirement
        public int Year { get; set; }

        [Required]
        public string Director { get; set; }

        [Required]
        public string Rating { get; set; }

        [Required]
        public bool Edited { get; set; } // Now required for Mission 7

        public string? LentTo { get; set; }

        [Required]
        public bool CopiedToPlex { get; set; } // New field for Mission 7

        [StringLength(25)]
        public string? Notes { get; set; }
    }
}