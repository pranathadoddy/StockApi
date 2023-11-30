using System;
using System.Collections.Generic;

namespace Stock.DataAccess.Application
{
    public partial class MItemLocation
    {
        public int Id { get; set; }
        public int? IdItem { get; set; }
        public int? IdLocation { get; set; }
        public int Stock { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDateTime { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDateTime { get; set; }

        public virtual MItem? IdItemNavigation { get; set; }
        public virtual MLocation? IdLocationNavigation { get; set; }
    }
}
