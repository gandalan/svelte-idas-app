using Gandalan.IDAS.WebApi.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UIPflege.DB;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;
using Gandalan.IDAS.IDASWebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Converter.FromDTO;

namespace Gandalan.IDAS.IDASWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VarianteController : Controller
    {
        private readonly UIPflegeContext _context;

        public VarianteController(UIPflegeContext context)
        {
            _context = context;
        }

        [HttpGet]
        //[Authorize]
        [Route("GetAllVarianten")]
        public async Task<ActionResult> GetAll(bool includeUI = false, bool onlyDirty = false)
        {
            List<Variante> result = new List<Variante>();
            var list = _context.Varianten.Include(i => i.KonfigSatz.Eintraege);
            if (includeUI)
                result = list.Include(i => i.UIDefinition.EingabeFelder).Include(i => i.UIDefinition.KonfiguratorFelder).ToList();
            else
                result = list.ToList();

            if (onlyDirty)
                result = result.Where(i => i.IsDirty).ToList();

            return Json(result);
        }

        [HttpGet]
        //[Authorize]
        [Route("GetAllVariantenNamen")]
        public async Task<ActionResult> GetAllNamen()
        {
            List<string> result = _context.Varianten.Select(i => i.Name).ToList();

            return Json(result);
        }


        [HttpGet]
        [Route("GetVarianteByName")]
        public async Task<ActionResult<Variante>> GetVarianteByName(string name, bool includeUI = false)
        {
            Variante result;
            if (includeUI)
            {
                result = _context.Varianten.Include(i => i.KonfigSatz.Eintraege)
                                           .Include(i => i.UIDefinition.EingabeFelder)
                                           .Include(i => i.UIDefinition.KonfiguratorFelder)
                                           .FirstOrDefault(i => i.Name == name);
            }
            else
            {
                result = _context.Varianten.Include(i => i.KonfigSatz.Eintraege).FirstOrDefault(i => i.Name == name);
            }
            return Json(result);
        }

        [HttpGet("api/Variante/{id:guid}")]
        public async Task<IActionResult> Get([FromForm] Guid id)
        {
            var item = await _context.Varianten.FirstAsync(i => i.VarianteGuid == id);
            return Json(item);
        }

        [HttpPost]
        [HttpPut]
        [Route("AddOrUpdateVariante")]
        public async Task<ActionResult<Variante>> AddOrUpdateVariante([FromBody] VarianteDTO dto)
        {
            Variante v = VarianteDTOConverter.CreateOrUpdateFromDTO(_context, dto);

            await _context.SaveChangesAsync();
            return Ok(v);
        }
    }
}
