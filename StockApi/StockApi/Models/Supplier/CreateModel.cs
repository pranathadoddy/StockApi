using System.ComponentModel.DataAnnotations;

namespace StockApi.Models.Supplier
{
    public class CreateModel
    {
        [Required]
        public string Name {  get; set; }
    }
}
