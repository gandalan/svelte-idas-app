using Gandalan.IDAS.WebApi.Client.DTOs.UI;
using Gandalan.IDAS.WebApi.DTO;
using UIPflege.DB;

namespace COnverter.FromDTO
{
    public class UIKonfiguratorFeldDTOConverter
    {
        public static void CreateOrUpdateFromDTO(UIPflegeContext db, UIDefinition uiDefinition, UIKonfiguratorFeldDTO dto)
        {
            var item = uiDefinition.KonfiguratorFelder.FirstOrDefault(k => k.UIKonfiguratorFeldGuid == dto.UIKonfiguratorFeldGuid);
            if (item == null)
            {
                item = new UIKonfiguratorFeld();
                db.KonfiguratorFelder.Add(item);
                uiDefinition.KonfiguratorFelder.Add(item);
            }

            item.UIKonfiguratorFeldGuid = dto.UIKonfiguratorFeldGuid;
            item.Reihenfolge = dto.Reihenfolge;
            item.EingabeLevel = dto.EingabeLevel;

            item.Caption = dto.Caption;
            item.Tag = dto.Tag;
            item.Kuerzel = dto.Kuerzel;
            item.WerteListeName = dto.WerteListeName;
            item.VorgabeWert = dto.VorgabeWert;
            item.BelegBlattText = dto.BelegBlattText;
            item.AngebotsText = dto.AngebotsText;

            item.ProfilId = dto.ProfilId;
            item.GehoertZuProfilId = dto.GehoertZuProfilId;
            item.GueltigAb = dto.GueltigAb;
            item.GueltigBis = dto.GueltigBis;
            item.Version = dto.Version;
            item.ChangedDate = dto.ChangedDate;

        }

        public static void CleanUpFromDTO(UIPflegeContext db, UIDefinition uiDefinition, UIDefinitionDTO dto)
        {
            var loeschen = new List<UIKonfiguratorFeld>();
            uiDefinition.KonfiguratorFelder.ForEach(existierendeZuordnung =>
            {
                if (!dto.KonfiguratorFelder.Any(k => k.UIKonfiguratorFeldGuid == existierendeZuordnung.UIKonfiguratorFeldGuid))
                {
                    loeschen.Add(existierendeZuordnung);
                }
            });

            foreach (var loeschItem in loeschen)
                uiDefinition.KonfiguratorFelder.Remove(loeschItem);
        }
    }
}