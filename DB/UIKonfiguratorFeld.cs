using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace UIPflege.DB
{
    public class UIKonfiguratorFeld
    {
        [Key, Column(Order = 0), DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long UIKonfiguratorFeldId { get; set; }
        public long UIDefinitionId { get; set; }
        [ForeignKey("UIDefinitionId")]
        [JsonIgnore]
        public virtual UIDefinition? UIDefinition { get; set; }
        //Baugruppe; Element; Profil
        public int EingabeLevel { get; set; }
        public int Reihenfolge { get; set; }
        public string? Caption { get; set; }
        public string? Tag { get; set; }
        public string? Kuerzel { get; set; }
        public string? WerteListeName { get; set; }
        public string? VorgabeWert { get; set; }
        public string? BelegBlattText { get; set; }
        public string? AngebotsText { get; set; }
        public int? ProfilId { get; set; }
        public int? GehoertZuProfilId { get; set; }
        public DateTime? GueltigAb { get; set; }
        public DateTime? GueltigBis { get; set; }
        public long Version { get; set; }
        public DateTime ChangedDate { get; set; }
        public Guid UIKonfiguratorFeldGuid { get; set; }

        public bool IsDirty { get; set; } = false;

    }
}
