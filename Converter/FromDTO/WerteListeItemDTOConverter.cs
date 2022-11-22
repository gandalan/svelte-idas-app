using Gandalan.IDAS.WebApi.DTO;
using UIPflege.DB;

namespace Converter.FromDTO
{
    public class WerteListeItemDTOConverter
    {
        public static void CreateOrUpdateFromDTO(UIPflegeContext db, WerteListe werteListe, WerteListeItemDTO dto)
        {
            var item = werteListe.Items.FirstOrDefault(k => k.WerteListeItemGuid == dto.WerteListeItemGuid);
            if (item == null)
            {
                item = new WerteListeItem();
                db.WerteListeItems.Add(item);
                werteListe.Items.Add(item);
            }

            item.BelegBlattText = dto.BelegBlattText;
            item.AngebotsText = dto.AngebotsText;
            item.Beschreibung = dto.Beschreibung;
            item.ChangedDate = dto.ChangedDate;
            item.Version = dto.Version;
            item.Kuerzel = dto.Kuerzel;
            item.Reihenfolge = dto.Reihenfolge;
            item.GueltigAb = dto.GueltigAb;
            item.GueltigBis = dto.GueltigBis;
        }



        public static void CleanUpFromDTO(UIPflegeContext db, WerteListe werteListe, WerteListeDTO dto)
        {
            var loeschen = new List<WerteListeItem>();
            werteListe.Items.ForEach(existierendeZuordnung =>
            {
                if (!dto.Items.Any(k => k.WerteListeItemGuid == existierendeZuordnung.WerteListeItemGuid))
                {
                    loeschen.Add(existierendeZuordnung);
                }
            });

            foreach (var loeschItem in loeschen)
                werteListe.Items.Remove(loeschItem);
        }
    }
}
