using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UIPflege.DB;

namespace UIPflege.DB
{
    public class KonfigSatz
    {
        public long KonfigSatzId { get; set; }
        public Guid KonfigSatzGuid { get; set; }
        public virtual ICollection<KonfigSatzEintrag> Eintraege { get; set; } = new ObservableCollection<KonfigSatzEintrag>();
        public DateTime ChangedDate { get; set; }
        public long Version { get; set; }

        public bool IsDirty { get; set; } = false;
    }
}
