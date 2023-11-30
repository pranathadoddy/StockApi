using System.ComponentModel.DataAnnotations;

namespace StockApi.Models.Location
{
    public class EditModel
    {
        [Required]
        public string Name { get; set; }
    }
}
