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
    public class SyncController : Controller
    {
        private readonly UIPflegeContext _context;

        public SyncController(UIPflegeContext context)
        {
            _context = context;
        }

        [HttpGet]
        //[Authorize]
        [Route("GetAllDirty")]
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
    }
}
