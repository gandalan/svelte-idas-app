using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace UIPflege.DB
{
    [Table("WerteListe")]
    public class WerteListe
    {
        public long WerteListeId { get; set; }
        public Guid WerteListeGuid { get; set; }
        public string Name { get; set; }
        public virtual List<WerteListeItem> Items { get; set; }
        public long Version { get; set; }
        public DateTime ChangedDate { get; set; }
        public DateTime GueltigAb { get; set; }

        public bool IsDirty { get; set; } = false;

        [NotMapped]
        public string AnzeigeText { get { return string.Format("{0} (gültig ab {1})", Name, GueltigAb.ToShortDateString()); } }

        public WerteListe()
        {
            this.Items = new List<WerteListeItem>();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}