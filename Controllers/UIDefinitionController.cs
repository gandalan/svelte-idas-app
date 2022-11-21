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
    public class UIDefinitionController : Controller
    {
        private readonly UIPflegeContext _context;

        public UIDefinitionController(UIPflegeContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetAllUIDefinition")]
        public async Task<ActionResult<UIDefinition>> GetAll(bool onlyDirty = false)
        {
            List<UIDefinition> list = _context.UIDefinitionen.Include(i => i.EingabeFelder).Include(i => i.KonfiguratorFelder).ToList();
            if (onlyDirty)
                list = list.Where(i => i.IsDirty).ToList();

            return Json(list);
        }
        [HttpGet]
        [Route("GetAllUIDefinitionNamen")]
        public async Task<ActionResult<string>> GetAllNamen(bool onlyDirty = false)
        {
            List<string> list = _context.UIDefinitionen.Include(i => i.EingabeFelder).Include(i => i.KonfiguratorFelder).Select(i => i.BezeichnungKurz).ToList() ?? new List<string>();
            return Json(list);
        }
        [HttpGet]
        [Route("GetUIDefinitionByName")]
        public async Task<ActionResult<UIDefinition>> GetByName(string name)
        {
            UIDefinition uidef = _context.UIDefinitionen.Include(i => i.EingabeFelder).Include(i => i.KonfiguratorFelder).FirstOrDefault(i => i.BezeichnungKurz == name);
            uidef.EingabeFelder = uidef.EingabeFelder.OrderBy(i => i.Reihenfolge).ToList();
            return Json(uidef);
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
