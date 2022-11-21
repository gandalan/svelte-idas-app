


using Gandalan.IDAS.WebApi.DTO;
using UIPflege.DB;

namespace Converter.ToDTO
{
    public class KonfigSatzDTOConverter
    {
        public static KonfigSatzDTO GetDTO(KonfigSatz k)
        {
            if (k == null)
            {
                return null;
            }

            KonfigSatzDTO dto = new KonfigSatzDTO();
            dto.ChangedDate = k.ChangedDate;
            dto.Eintraege = GetDTOKonfigSatzEintraege(k);
            dto.KonfigSatzGuid = k.KonfigSatzGuid;
            dto.Version = k.Version;

            return dto;
        }

        public static List<KonfigSatzEintragDTO> GetDTOKonfigSatzEintraege(KonfigSatz k)
        {
            if (k == null)
            {
                return null;
            }

            List<KonfigSatzEintragDTO> result = new List<KonfigSatzEintragDTO>();
            foreach (KonfigSatzEintrag e in k.Eintraege)
                result.Add(new KonfigSatzEintragDTO()
                {
                    DatenTyp = e.DatenTyp,
                    KonfigName = e.KonfigName,
                    KonfigSatzEintragGuid = e.KonfigSatzEintragGuid,
                    UnterkomponenteName = e.UnterkomponenteName,
                    Wert = e.Wert,
                    ChangedDate = e.ChangedDate
                });
            return result;

        }
    }
}