using Gandalan.IDAS.WebApi.DTO;
using UIPflege.DB;

namespace Converter.FromDTO
{
    public class WerteListeDTOConverter
    {
        public static WerteListe CreateOrUpdateFromDTO(UIPflegeContext db, WerteListeDTO dto)
        {
            WerteListe werteListe = db.WerteListen.FirstOrDefault(u => u.WerteListeGuid == dto.WerteListeGuid ||
                (u.WerteListeGuid == Guid.Empty && u.Name == dto.Name));
            if (werteListe == null)
            {
                werteListe = new WerteListe();
                db.WerteListen.Add(werteListe);
            }

            werteListe.Name = dto.Name;
            werteListe.Version = dto.Version;
            werteListe.ChangedDate = dto.ChangedDate;
            werteListe.WerteListeGuid = dto.WerteListeGuid;
            werteListe.GueltigAb = dto.GueltigAb;

            if (werteListe.Items != null)
            {
                WerteListeItemDTOConverter.CleanUpFromDTO(db, werteListe, dto);

                foreach (var item in dto.Items) WerteListeItemDTOConverter.CreateOrUpdateFromDTO(db, werteListe, item);
            }

            return werteListe;
        }
    }
}
