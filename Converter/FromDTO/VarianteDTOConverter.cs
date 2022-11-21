
using COnverter.FromDTO;
using Gandalan.IDAS.WebApi.DTO;
using UIPflege.DB;

namespace Converter.FromDTO
{
    public class VarianteDTOConverter
    {
        public static Variante CreateOrUpdateFromDTO(UIPflegeContext db, VarianteDTO dto)
        {

            Variante variante = db.Varianten.FirstOrDefault(f => f.VarianteGuid == dto.VarianteGuid ||
                            (f.VarianteGuid == Guid.Empty && f.Name == dto.Name));

            if (variante == null)
            {
                variante = new Variante();
                db.Varianten.Add(variante);
            }

            variante.Name = dto.Name;
            variante.VarianteGuid = dto.VarianteGuid;

            variante.KonfigSatzGuid = dto.KonfigSatzGuid;
            variante.KonfigSatz = KonfigSatzDTOConverter.CreateOrUpdateFromDTO(db, dto);

            variante.UIDefinitionGuid = dto.UIDefinitionGuid;
            //Raus aus Variante?
            //UIDefintion muss getrennt behandet werden
            if (dto.UIDefinition != null)
                variante.UIDefinition = UIDefinitionDTOConverter.CreateOrUpdateFromDTO(db, dto.UIDefinition);

            return variante;
        }
    }
}