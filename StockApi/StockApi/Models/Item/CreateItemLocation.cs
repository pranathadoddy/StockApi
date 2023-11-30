using System.ComponentModel.DataAnnotations;

namespace StockApi.Models.Item
{
    public class CreateItemLocation
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int Stock {  get; set; }

    }
}
