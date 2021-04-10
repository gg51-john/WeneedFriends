using System;
using System.Collections.Generic;

#nullable disable

namespace new_layout_core.Models
{
    public partial class TCity
    {
        public TCity()
        {
            TLocations = new HashSet<TLocation>();
        }

        public int FCityId { get; set; }
        public string FCityName { get; set; }

        public virtual ICollection<TLocation> TLocations { get; set; }
    }
}
