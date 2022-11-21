using Gandalan.IDAS.WebApi.DTO;


namespace Converter.ToDTO;
public class VarianteDTOConverter
{
    public static VarianteDTO GetDTO(Variante v, bool includeUIDefs, int maxLevel = 1)
    {
        if (v == null)
        {
            return null;
        }

        VarianteDTO vdto = new VarianteDTO();
        vdto.Name = v.Name;
        vdto.VarianteGuid = v.VarianteGuid;

        vdto.ChangedDate = v.ChangedDate;
        vdto.Version = v.Version;

        vdto.UIDefinitionGuid = v.UIDefinition?.UIDefinitionGuid ?? Guid.Empty;
        vdto.KonfigSatzGuid = v.KonfigSatz?.KonfigSatzGuid ?? Guid.Empty;


        if (v.KonfigSatz != null)
            vdto.KonfigSatz = KonfigSatzDTOConverter.GetDTOKonfigSatzEintraege(v.KonfigSatz);

        if (includeUIDefs && v.UIDefinition != null)
            vdto.UIDefinition = UIDefinitionDTOConverter.GetDTO(v.UIDefinition, maxLevel);
        return vdto;
    }
}