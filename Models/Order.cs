using System.ComponentModel.DataAnnotations.Schema;

namespace E_commercePlants.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public bool Shipped { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public decimal GrandTotal { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}