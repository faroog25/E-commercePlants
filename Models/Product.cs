using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_commercePlants.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required, MinLength(2, ErrorMessage = "Minimum length is 2")]
        public string Name { get; set; }
        public string Slug { get; set; }
        [Required, MinLength(8, ErrorMessage = "Minimum length is 8")]
        public string Description { get; set; }
        [Required]
        [Column(TypeName= "decimal(8,2)")]
        public decimal Price { get; set; }
        [Required,Range(1,int.MaxValue,ErrorMessage ="you must choose a category!")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public string Image { get; set; } = "noimage.png";
        [NotMapped]
        [FileExtensions]
        public IFormFile ImageUpload { get; set; }

    }
}
