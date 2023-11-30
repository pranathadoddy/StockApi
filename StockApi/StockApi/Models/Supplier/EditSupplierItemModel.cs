using System.ComponentModel.DataAnnotations;

namespace StockApi.Models.Supplier
{
    public class EditSupplierItemModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
