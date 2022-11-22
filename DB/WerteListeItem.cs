using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace UIPflege.DB
{
    [Table("WerteListeItem")]
    public class WerteListeItem
    {
        public long WerteListeItemId { get; set; }
        public long WerteListeId { get; set; }

        [ForeignKey("WerteListeId")]
        [JsonIgnore]
        public virtual WerteListe WerteListe { get; set; }
        public Guid WerteListeItemGuid { get; set; }

        public int Reihenfolge { get; set; }
        public string? Beschreibung { get; set; }
        public string Kuerzel { get; set; }
        public string? BelegBlattText { get; set; }
        public string? AngebotsText { get; set; }

        public DateTime? GueltigAb { get; set; }
        public DateTime? GueltigBis { get; set; }

        public long Version { get; set; }
        public DateTime ChangedDate { get; set; }

        public bool IsDirty { get; set; } = false;


        public WerteListeItem()
        {
            WerteListeItemGuid = Guid.NewGuid();
        }
    }
}