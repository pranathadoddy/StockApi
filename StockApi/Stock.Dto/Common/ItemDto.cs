using Framework.Dto;

namespace Stock.Dto.Common
{
    public class ItemDto : AuditableDto<int>
    {
        public string Name { get; set; }

        public decimal SellingPrice { get; set; }
    }
}
