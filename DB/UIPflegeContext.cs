using Microsoft.EntityFrameworkCore;

namespace UIPflege.DB
{
    public class UIPflegeContext: DbContext
    {
        public DbSet<KonfigSatz> KonfigSaetze { get; set; }
        public DbSet<KonfigSatzEintrag> KonfigSatzEintraege { get; set; }
        public DbSet<Variante> Varianten { get; set; }
        public DbSet<UIDefinition> UIDefinitionen { get; set; }
        public DbSet<UIEingabeFeld> EingabeFelder { get; set; }
        public DbSet<UIKonfiguratorFeld> KonfiguratorFelder { get; set; }
        public DbSet<WerteListe> WerteListen { get; set; }
        public DbSet<WerteListeItem> WerteListeItems { get; set; }

        public UIPflegeContext(DbContextOptions<UIPflegeContext> options) : base(options)
        {
        }
    }
}
