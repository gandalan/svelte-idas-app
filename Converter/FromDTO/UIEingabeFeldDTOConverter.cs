using Gandalan.IDAS.WebApi.DTO;
using UIPflege.DB;

namespace COnverter.FromDTO
{
    public class UIEingabeFeldDTOConverter
    {
        public static void CreateOrUpdateFromDTO(UIPflegeContext db, UIDefinition uiDefinition, UIEingabeFeldDTO dto)
        {
            if (!Enum.TryParse<UIEingabeFeldRegel>(dto.Regel, out var regel))
            {
                throw new ArgumentException($"Cannot parse enum: {nameof(UIEingabeFeldRegel)}: '{dto.Regel}'");
            }

            if (dto.ZusatzFeldGruppeId != null && dto.GehoertZuZusatzFeldGruppeId != null)
                throw new Exception("Ein UIEingabeFeld darf nicht eine Gruppe erstellen und zu einer Gruppe gehören.");
            var item = uiDefinition.EingabeFelder.FirstOrDefault(k => k.UIEingabeFeldGuid == dto.UIEingabeFeldGuid);
            if (item == null)
            {
                item = new UIEingabeFeld();
                db.EingabeFelder.Add(item);
                uiDefinition.EingabeFelder.Add(item);
            }


            item.UIEingabeFeldGuid = dto.UIEingabeFeldGuid;
            item.BelegBlattText = dto.BelegBlattText;
            item.AngebotsText = dto.AngebotsText;
            item.Caption = dto.Caption;
            item.ChangedDate = dto.ChangedDate;
            item.EingabeLevel = dto.EingabeLevel;
            item.FehlerText = dto.FehlerText;
            item.HilfeText = dto.HilfeText;
            item.WarnText = dto.WarnText;
            item.MaxWert = dto.MaxWert;
            item.MaxWertWeichPruefen = dto.MaxWertWeichPruefen;
            item.MindestBreite = dto.MindestBreite;
            item.MinWert = dto.MinWert;
            item.MinWertWeichPruefen = dto.MinWertWeichPruefen;
            item.PreisFeldAnzeigen = dto.PreisFeldAnzeigen;
            item.Reihenfolge = dto.Reihenfolge;
            item.ZusatzFeldGruppeId = dto.ZusatzFeldGruppeId;
            item.GehoertZuZusatzFeldGruppeId = dto.GehoertZuZusatzFeldGruppeId;
            item.Tag = dto.Tag;
            item.Version = dto.Version;
            item.VorgabeWert = dto.VorgabeWert;
            item.WerteListeName = dto.WerteListeName;
            item.GueltigAb = dto.GueltigAb;
            item.GueltigBis = dto.GueltigBis;
            item.IstKonfiguratorFeld = dto.IstKonfiguratorFeld;
        }

        public static void CleanUpFromDTO(UIPflegeContext db, UIDefinition uiDefinition, UIDefinitionDTO dto)
        {
            var loeschen = new List<UIEingabeFeld>();
            uiDefinition.EingabeFelder.ForEach(existierendeZuordnung =>
            {
                if (!dto.EingabeFelder.Any(k => k.UIEingabeFeldGuid == existierendeZuordnung.UIEingabeFeldGuid))
                {
                    loeschen.Add(existierendeZuordnung);
                }
            });

            foreach (var loeschItem in loeschen)
                uiDefinition.EingabeFelder.Remove(loeschItem);
        }

    }
}