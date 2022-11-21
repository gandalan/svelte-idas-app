using Gandalan.IDAS.WebApi.DTO;
using UIPflege.DB;

namespace COnverter.FromDTO
{
    public class UIDefinitionDTOConverter
    {
        public static UIDefinition CreateOrUpdateFromDTO(UIPflegeContext db, UIDefinitionDTO dto)
        {
            UIDefinition uiDefinition = db.UIDefinitionen.FirstOrDefault(
                           u => u.UIDefinitionGuid == dto.UIDefinitionGuid ||
                           (u.UIDefinitionGuid == Guid.Empty && u.BezeichnungKurz == dto.BezeichnungKurz));

            if (uiDefinition == null)
            {
                uiDefinition = new UIDefinition();
                db.UIDefinitionen.Add(uiDefinition);
            }

            uiDefinition.Kategorie = dto.Kategorie;
            uiDefinition.BezeichnungKurz = dto.BezeichnungKurz;
            uiDefinition.BezeichnungLang = dto.BezeichnungLang;
            uiDefinition.BildHorizontal = dto.BildHorizontal;
            uiDefinition.BildVertikal = dto.BildVertikal;
            uiDefinition.Bild3D = dto.Bild3D;
            uiDefinition.Version = dto.Version;
            uiDefinition.ChangedDate = dto.ChangedDate;
            uiDefinition.UIDefinitionGuid = dto.UIDefinitionGuid;

            if (dto.EingabeFelder != null)
            {
                UIEingabeFeldDTOConverter.CleanUpFromDTO(db, uiDefinition, dto);

                foreach (var item in dto.EingabeFelder) UIEingabeFeldDTOConverter.CreateOrUpdateFromDTO(db, uiDefinition, item);
            }
            if (dto.KonfiguratorFelder != null)
            {
                UIKonfiguratorFeldDTOConverter.CleanUpFromDTO(db, uiDefinition, dto);

                foreach (var item in dto.KonfiguratorFelder) UIKonfiguratorFeldDTOConverter.CreateOrUpdateFromDTO(db, uiDefinition, item);
            }


            return uiDefinition;
        }
    }
}