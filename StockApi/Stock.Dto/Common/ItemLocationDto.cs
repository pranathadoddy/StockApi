using Framework.Dto;

namespace Stock.Dto.Common
{
    public class ItemLocationDto : AuditableDto<int>
    {
        public int? IdItem { get; set; }
        public int? IdLocation { get; set; }

        public string ItemName { get; set; }

        public string LocationName { get; set; }    

        public int Stock { get; set; }
    }
}
