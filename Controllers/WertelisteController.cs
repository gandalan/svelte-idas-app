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
    public class WertelisteController : Controller
    {
        private readonly UIPflegeContext _context;

        public WertelisteController(UIPflegeContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetAllWertelisten")]
        public async Task<ActionResult<WerteListe>> GetAll(bool onlyDirty = false)
        {
            List<WerteListe> list = _context.WerteListen.Include(i => i.Items).ToList();
            if (onlyDirty)
                list = list.Where(i => i.IsDirty).ToList();

            return Json(list);
        }
        [HttpGet]
        [Route("GetAllWertelistenNamen")]
        public async Task<ActionResult<string>> GetAllNamen(bool onlyDirty = false)
        {
            List<string> list = _context.WerteListen.Select(i => i.Name).ToList() ?? new List<string>();
            list = list.OrderBy(i => i).ToList();
            return Json(list);
        }
        [HttpGet]
        [Route("GetWertelisteByName")]
        public async Task<ActionResult<UIDefinition>> GetByName(string name)
        {
            WerteListe werteListe = _context.WerteListen.Include(i => i.Items).FirstOrDefault(i => i.Name == name);
            werteListe.Items = werteListe.Items.OrderBy(i => i.Reihenfolge).ToList();
            return Json(werteListe);
        }

        [HttpPut]
        [HttpPut]
        [Route("AddOrUpdateWerteliste")]
        public async Task<ActionResult<WerteListe>> AddOrUpdateWerteliste([FromBody] WerteListeDTO dto)
        {
            WerteListe wL = WerteListeDTOConverter.CreateOrUpdateFromDTO(_context, dto);

            await _context.SaveChangesAsync();
            return Ok(wL);
        }

    }
}
