using Gandalan.IDAS.WebApi.DTO;
using UIPflege.DB;

namespace COnverter.FromDTO
{
    public class KonfigSatzDTOConverter
    {
        public static KonfigSatz CreateOrUpdateFromDTO(UIPflegeContext db, VarianteDTO dto)
        {
            KonfigSatz konfigSatz = db.KonfigSaetze.FirstOrDefault(f => f.KonfigSatzGuid == dto.KonfigSatzGuid);

            if (konfigSatz == null)
            {
                konfigSatz = new KonfigSatz();
                db.KonfigSaetze.Add(konfigSatz);
            }

            konfigSatz.KonfigSatzGuid = dto.KonfigSatzGuid;
            konfigSatz.ChangedDate = dto.ChangedDate;
            konfigSatz.Version = dto.Version;

            KonfigSatzEintragDTOConverter.CleanUpFromDTO(db, konfigSatz, dto);

            foreach (var item in dto.KonfigSatz) KonfigSatzEintragDTOConverter.CreateOrUpdateFromDTO(db, konfigSatz, item);


            return konfigSatz;
        }


    }
}