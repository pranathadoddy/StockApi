using System;
using System.Collections.Generic;

namespace Stock.DataAccess.Application
{
    public partial class MLocation
    {
        public MLocation()
        {
            MItemLocations = new HashSet<MItemLocation>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDateTime { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDateTime { get; set; }

        public virtual ICollection<MItemLocation> MItemLocations { get; set; }
    }
}
