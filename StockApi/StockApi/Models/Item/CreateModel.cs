using System.ComponentModel.DataAnnotations;

namespace StockApi.Models.Item
{
    public class CreateModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public decimal SellingPrice { get; set; }
    }
}
