using Gandalan.IDAS.WebApi.DTO;
using UIPflege.DB;

namespace COnverter.FromDTO
{
    public class KonfigSatzEintragDTOConverter
    {
        public static void CreateOrUpdateFromDTO(UIPflegeContext db, KonfigSatz konfigSatz, KonfigSatzEintragDTO dto)
        {
            var item = konfigSatz.Eintraege.FirstOrDefault(k => k.KonfigSatzEintragGuid == dto.KonfigSatzEintragGuid);
            if (item == null)
            {
                item = new KonfigSatzEintrag();
                db.KonfigSatzEintraege.Add(item);
                konfigSatz.Eintraege.Add(item);
            }
            item.DatenTyp = dto.DatenTyp;
            item.KonfigName = dto.KonfigName;
            item.KonfigSatzEintragGuid = dto.KonfigSatzEintragGuid;
            item.UnterkomponenteName = dto.UnterkomponenteName;
            item.Wert = dto.Wert;
            item.ChangedDate = dto.ChangedDate;
        }

        public static void CleanUpFromDTO(UIPflegeContext db, KonfigSatz konfigSatz, VarianteDTO dto)
        {
            var loeschen = new List<KonfigSatzEintrag>();
            foreach (var existierendeZuordnung in konfigSatz.Eintraege)
            {
                if (dto.KonfigSatz.All(k => k.KonfigSatzEintragGuid != existierendeZuordnung.KonfigSatzEintragGuid))
                {
                    loeschen.Add(existierendeZuordnung);
                }
            }
            foreach (var loeschItem in loeschen)
            {
                konfigSatz.Eintraege.Remove(loeschItem);
                db.KonfigSatzEintraege.Remove(loeschItem);
            }
        }
    }
}