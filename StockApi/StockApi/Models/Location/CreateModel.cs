using System.ComponentModel.DataAnnotations;

namespace StockApi.Models.Location
{
    public class CreateModel
    {
        [Required]
        public string Name { get; set; }
    }
}
