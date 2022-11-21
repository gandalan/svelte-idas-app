using Gandalan.IDAS.WebApi.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UIPflege.DB;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;
using Gandalan.IDAS.IDASWebApp.Services;
using Microsoft.AspNetCore.Authorization;

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
        public async Task<ActionResult<Variante>> GetAll(bool onlyDirty = false)
        {
            List<Variante> list = _context.Varianten.Include(i => i.KonfigSatz.Eintraege).Include(i => i.UIDefinition.EingabeFelder).Include(i => i.UIDefinition.KonfiguratorFelder).ToList();
            if (onlyDirty)
                list = list.Where(i => i.IsDirty).ToList();

            return Json(list);
        }

        [HttpGet]
        [Authorize]
        [Route("GetAllVariantenNamen")]
        public async Task<ActionResult<Variante>> GetAllNamen(bool onlyDirty = false)
        {
            var list = _context.Varianten.Include(i => i.KonfigSatz.Eintraege).Include(i => i.UIDefinition.EingabeFelder).Include(i => i.UIDefinition.KonfiguratorFelder).Where(i => i.IsDirty == onlyDirty).ToList();
            return Json(list);
        }

        [HttpGet("api/Variante/{id:guid}")]
        public async Task<IActionResult> Get([FromForm] Guid id)
        {
            var item = await _context.Varianten.FirstAsync(i => i.VarianteGuid == id);
            return Json(item);
        }

        [HttpPost]
        public async Task<ActionResult<Variante>> AddVariante([FromBody] Variante dto)
        {
            if (!_context.Varianten.Any(i => i.VarianteGuid == dto.VarianteGuid))
                _context.Varianten.Add(dto);

            await _context.SaveChangesAsync();
            return Ok(dto);
        }
    }
}
