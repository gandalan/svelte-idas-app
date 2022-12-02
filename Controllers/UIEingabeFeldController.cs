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
    public class UIEingabeFeldController : Controller
    {
        private readonly UIPflegeContext _context;

        public UIEingabeFeldController(UIPflegeContext context)
        {
            _context = context;
        }

        
        [HttpPost]
        [HttpPut]
        [Route("")]
        public async Task<ActionResult<UIDefinition>> AddOrUpdateUIDefinition(Guid guid, [FromBody] UIDefinition uidefinition)
        {
            UIDefinition uidef = _context.UIDefinitionen.Include(i => i.EingabeFelder).Include(i => i.KonfiguratorFelder).FirstOrDefault(i => i.UIDefinitionGuid == guid);
            _context.ChangeTracker.Clear();
            if (uidef == null)
                _context.Entry(uidefinition).State = EntityState.Added;
            else
            {
                if (uidefinition.IsDirty)
                    _context.Entry(uidefinition).State = EntityState.Modified;

                foreach (var feld in uidefinition.EingabeFelder)
                {
                    if (feld.UIEingabeFeldId == 0)
                        _context.Entry(feld).State = EntityState.Added;
                    else if (feld.IsDirty)
                    {
                        _context.Entry(feld).State = EntityState.Modified;
                    }

                }
                foreach (var feld in uidefinition.KonfiguratorFelder)
                {
                    if (feld.UIKonfiguratorFeldId == 0)
                        _context.Entry(feld).State = EntityState.Added;
                    if (feld.IsDirty)
                        _context.Entry(feld).State = EntityState.Modified;
                }
            }

            await _context.SaveChangesAsync();
            return Ok(uidefinition);
        }
    }
}
