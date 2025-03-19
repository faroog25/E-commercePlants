using System.ComponentModel.DataAnnotations.Schema;

namespace E_commercePlants.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public int Quantity{get;set;}
        [Column(TypeName = "decimal(8,2)")]
        public decimal Price { get; set; }
        public string Image {get;set;}
        public int OrderId{get;set;}
    }
}