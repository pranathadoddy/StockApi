﻿using System;
using System.Collections.Generic;

namespace Stock.DataAccess.Application
{
    public partial class MItem
    {
        public MItem()
        {
            MItemLocations = new HashSet<MItemLocation>();
            MSupplierItems = new HashSet<MSupplierItem>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal SellingPrice { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDateTime { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDateTime { get; set; }

        public virtual ICollection<MItemLocation> MItemLocations { get; set; }
        public virtual ICollection<MSupplierItem> MSupplierItems { get; set; }
    }
}
