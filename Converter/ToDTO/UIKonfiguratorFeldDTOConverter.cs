using Gandalan.IDAS.WebApi.Client.DTOs.UI;
using UIPflege.DB;

namespace Converter.ToDTO
{
    public class UIKonfiguratorFeldDTOConverter
    {
        public static UIKonfiguratorFeldDTO GetDTO(UIKonfiguratorFeld feld)
        {
            if (feld == null)
            {
                return null;
            }

            UIKonfiguratorFeldDTO dto = new UIKonfiguratorFeldDTO();
            dto.UIKonfiguratorFeldGuid = feld.UIKonfiguratorFeldGuid;
            dto.Reihenfolge = feld.Reihenfolge;
            dto.EingabeLevel = feld.EingabeLevel;
            dto.Caption = feld.Caption;
            dto.Tag = feld.Tag;
            dto.Kuerzel = feld.Kuerzel;
            dto.WerteListeName = feld.WerteListeName;
            dto.VorgabeWert = feld.VorgabeWert;
            dto.BelegBlattText = feld.BelegBlattText;
            dto.AngebotsText = feld.AngebotsText;
            dto.ProfilId = feld.ProfilId;
            dto.GehoertZuProfilId = feld.GehoertZuProfilId;
            dto.GueltigAb = feld.GueltigAb;
            dto.GueltigBis = feld.GueltigBis;
            dto.Version = feld.Version;
            dto.ChangedDate = feld.ChangedDate;
            return dto;
        }

    }
}
