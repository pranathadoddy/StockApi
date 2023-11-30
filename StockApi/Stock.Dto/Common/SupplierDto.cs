using Framework.Dto;

namespace Stock.Dto.Common
{
    public class SupplierDto : AuditableDto<int>
    {
        public string Name { get; set; } = null!;
    }
}
