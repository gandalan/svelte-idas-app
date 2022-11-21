using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;

namespace UIPflege.DB
{
    public class UIEingabeFeld
    {
        [Key, Column(Order = 0), DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long UIEingabeFeldId { get; set; }
        public long UIDefinitionId { get; set; }

        [ForeignKey("UIDefinitionId")]
        [JsonIgnore]
        public virtual UIDefinition? UIDefinition { get; set; }
        public int Reihenfolge { get; set; }
        public string? Caption { get; set; }
        public string? Tag { get; set; }
        public double MinWert { get; set; }
        public bool MinWertWeichPruefen { get; set; }
        public double MaxWert { get; set; }
        public bool MaxWertWeichPruefen { get; set; }
        public string? VorgabeWert { get; set; }
        public string? HilfeText { get; set; }
        public string? FehlerText { get; set; }
        public string? WarnText { get; set; }
        public string? BelegBlattText { get; set; }
        public string? AngebotsText { get; set; }
        public string? WerteListeName { get; set; }
        public bool PreisFeldAnzeigen { get; set; }
        public int MindestBreite { get; set; }
        public Guid UIEingabeFeldGuid { get; set; }
        public int EingabeLevel { get; set; }

        public int? ZusatzFeldGruppeId { get; set; }
        public int? GehoertZuZusatzFeldGruppeId { get; set; }

        public DateTime? GueltigAb { get; set; }
        public DateTime? GueltigBis { get; set; }
        public long Version { get; set; }
        public DateTime ChangedDate { get; set; }
        public bool IstKonfiguratorFeld { get; set; }

        public bool IsDirty { get; set; } = false;

    }
}
