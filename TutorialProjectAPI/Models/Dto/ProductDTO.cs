using System.ComponentModel.DataAnnotations;

namespace TutorialProjectAPI.Models.Dto
{
    public record ProductDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        public double Price { get; set; }

    }
}
