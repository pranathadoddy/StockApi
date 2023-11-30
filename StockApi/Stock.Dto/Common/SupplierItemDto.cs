using Framework.Dto;

namespace Stock.Dto.Common
{
    public class SupplierItemDto : AuditableDto<int>
    {
        public int IdSupplier { get; set; }
        public int IdItem { get; set; }

        public string? SupplierName { get; set; }

        public string? ItemName { get; set; }

        public decimal Price { get; set; }
    }
}
