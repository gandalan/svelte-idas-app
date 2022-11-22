using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.RegularExpressions;
using UIPflege.DB;

namespace UIPflege.DB
{
    public class Variante
    {
        public long VarianteId { get; set; }
        [Required]
        public string Name { get; set; }
        public Guid VarianteGuid { get; set; }

        [ForeignKey("KonfigSatzId")]
        public virtual KonfigSatz? KonfigSatz { get; set; }
        public Guid KonfigSatzGuid { get; set; }

        [ForeignKey("UIDefinitionId")]
        public virtual UIDefinition? UIDefinition { get; set; }
        public Guid UIDefinitionGuid { get; set; }

        public int Version { get; set; }
        public DateTime ChangedDate { get; set; }

        public bool IsDirty { get; set; } = false;
    }
}