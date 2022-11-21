using System.Text.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace UIPflege.DB
{
    public class UIDefinition
    {
        public long UIDefinitionId { get; set; }
        public string? Kategorie { get; set; }
        public string? BezeichnungKurz { get; set; }
        public string? BezeichnungLang { get; set; }
        public string? BildHorizontal { get; set; }
        public string? BildVertikal { get; set; }
        public string? Bild3D { get; set; }

        [InverseProperty("UIDefinition")]
        [JsonIgnore]
        public virtual List<Variante> Varianten { get; set; }

        public virtual List<UIEingabeFeld> EingabeFelder { get; set; }
        public virtual List<UIKonfiguratorFeld> KonfiguratorFelder { get; set; }
        public long Version { get; set; }
        public DateTime ChangedDate { get; set; }
        public Guid UIDefinitionGuid { get; set; }

        public bool IsDirty { get; set; } = false;

        public UIDefinition()
        {
            this.EingabeFelder = new List<UIEingabeFeld>();
            this.Varianten = new List<Variante>();
            this.KonfiguratorFelder = new List<UIKonfiguratorFeld>();
        }

        public override string ToString()
        {
            return BezeichnungKurz;
        }
    }
}
