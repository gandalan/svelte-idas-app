using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UIPflege.DB
{
	public class KonfigSatzEintrag
	{
		[Key, Column(Order = 0), DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
		public long KonfigSatzEintragId { get; set; }
		public long KonfigSatzId { get; set; }
		public string? UnterkomponenteName { get; set; }
		public string? KonfigName { get; set; }
		public string? Wert { get; set; }
		public string? DatenTyp { get; set; }
		public long Version { get; set; }
        public DateTime ChangedDate { get; set; }
        public Guid KonfigSatzEintragGuid { get; set; }

		public bool IsDirty { get; set; } = false;
    }
}