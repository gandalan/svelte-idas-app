using Gandalan.IDAS.WebApi.Client.DTOs.UI;
using Gandalan.IDAS.WebApi.DTO;
using UIPflege.DB;

namespace Converter.ToDTO
{
    public class UIDefinitionDTOConverter
    {
         public static UIDefinitionDTO GetDTO(UIDefinition def, int maxLevel = 1)
        {
            if (def == null)
            {
                return null;
            }

            UIDefinitionDTO dto = new UIDefinitionDTO();
            dto.Kategorie = def.Kategorie;
            dto.BezeichnungKurz = def.BezeichnungKurz;
            dto.BezeichnungLang = def.BezeichnungLang;
            dto.BildHorizontal = def.BildHorizontal;
            dto.BildVertikal = def.BildVertikal;
            dto.Bild3D = def.Bild3D;
            dto.Version = def.Version;
            dto.ChangedDate = def.ChangedDate;
            dto.UIDefinitionGuid = def.UIDefinitionGuid;
            var felder = def.EingabeFelder.Where(f => f.EingabeLevel <= maxLevel);
            foreach (var f in felder)
            {
                dto.EingabeFelder.Add(UIEingabeFeldDTOConverter.GetDTO(f));
            }
            foreach(var kf in def.KonfiguratorFelder)
            {
                dto.KonfiguratorFelder.Add(UIKonfiguratorFeldDTOConverter.GetDTO(kf));
            }
            return dto;
        }
    }
}