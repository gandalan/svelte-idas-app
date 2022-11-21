using Converter.FromDTO;
using Gandalan.IDAS.WebApi.DTO;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using UIPflege.DB;

namespace Gandalan.IDAS.IDASWebApp.Services
{
    public class SyncService
    {
        public async Task SyncAllFromIDAS(UIPflegeContext db, List<VarianteDTO> idasVarianten)
        {
            IDASService serive = new IDASService();
            List<Variante> variantenListe = new List<Variante>();

            foreach (var idasV in idasVarianten)
            {
                var v = VarianteDTOConverter.CreateOrUpdateFromDTO(db, idasV);
                variantenListe.Add(v);
            }

        }
    }
}
