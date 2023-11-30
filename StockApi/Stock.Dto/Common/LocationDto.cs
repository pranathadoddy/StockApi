using Framework.Dto;

namespace Stock.Dto.Common
{
    public class LocationDto : AuditableDto<int>
    {
        public string Name { get; set; } = null!;
    }
}
