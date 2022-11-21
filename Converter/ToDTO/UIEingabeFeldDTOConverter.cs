using Gandalan.IDAS.WebApi.DTO;
using UIPflege.DB;

namespace Converter.ToDTO
{
    public class UIEingabeFeldDTOConverter
    {
        public static UIEingabeFeldDTO GetDTO(UIEingabeFeld feld)
        {
            if (feld == null)
            {
                return null;
            }

            UIEingabeFeldDTO dto = new UIEingabeFeldDTO();
            dto.Reihenfolge = feld.Reihenfolge;
            dto.Caption = feld.Caption;
            dto.Tag = feld.Tag;
            dto.Regel = feld.Regel.ToString();
            dto.MinWert = feld.MinWert;
            dto.MinWertWeichPruefen = feld.MinWertWeichPruefen;
            dto.MaxWert = feld.MaxWert;
            dto.MaxWertWeichPruefen = feld.MaxWertWeichPruefen;
            dto.VorgabeWert = feld.VorgabeWert;
            dto.HilfeText = feld.HilfeText;
            dto.FehlerText = feld.FehlerText;
            dto.WarnText = feld.WarnText;
            dto.WerteListeName = feld.WerteListeName;
            dto.PreisFeldAnzeigen = feld.PreisFeldAnzeigen;
            dto.MindestBreite = feld.MindestBreite;
            dto.Version = feld.Version;
            dto.ChangedDate = feld.ChangedDate;
            dto.UIEingabeFeldGuid = feld.UIEingabeFeldGuid;
            dto.BelegBlattText = feld.BelegBlattText;
            dto.AngebotsText = feld.AngebotsText;
            dto.EingabeLevel = feld.EingabeLevel;
            dto.ZusatzFeldGruppeId = feld.ZusatzFeldGruppeId;
            dto.GehoertZuZusatzFeldGruppeId = feld.GehoertZuZusatzFeldGruppeId;
            dto.GueltigAb = feld.GueltigAb;
            dto.GueltigBis = feld.GueltigBis;
            dto.IstKonfiguratorFeld = feld.IstKonfiguratorFeld;
            return dto;
        }

    }
}
