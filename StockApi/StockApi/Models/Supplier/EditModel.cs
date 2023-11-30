using System.ComponentModel.DataAnnotations;

namespace StockApi.Models.Supplier
{
    public class EditModel
    {
        [Required]
        public string Name { get; set; }
    }
}
