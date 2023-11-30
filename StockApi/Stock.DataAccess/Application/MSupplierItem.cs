using System;
using System.Collections.Generic;

namespace Stock.DataAccess.Application
{
    public partial class MSupplierItem
    {
        public int Id { get; set; }
        public int? IdSupplier { get; set; }
        public int? IdItem { get; set; }
        public decimal Price { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDateTime { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDateTime { get; set; }

        public virtual MItem? IdItemNavigation { get; set; }
        public virtual MSupplier? IdSupplierNavigation { get; set; }
    }
}
