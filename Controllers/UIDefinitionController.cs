using Gandalan.IDAS.WebApi.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UIPflege.DB;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;
using Gandalan.IDAS.IDASWebApp.Services;
using Microsoft.AspNetCore.Authorization;
using System.Drawing;

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
            List<string> list = _context.UIDefinitionen.Select(i => i.BezeichnungKurz).ToList() ?? new List<string>();
            return Json(list);
        }
        [HttpGet]
        [Route("GetUIDefinitionByName")]
        public async Task<ActionResult<UIDefinition>> GetByName(string name)
        {
            UIDefinition uidef = _context.UIDefinitionen.Include(i => i.EingabeFelder).Include(i => i.KonfiguratorFelder).FirstOrDefault(i => i.BezeichnungKurz == name);
            uidef = SortListsByReihenfolge(uidef);
            return Json(uidef);
        }

        [HttpGet("api/Variante/{id:guid}")]
        public async Task<IActionResult> Get([FromForm] Guid id)
        {
            var item = await _context.Varianten.FirstAsync(i => i.VarianteGuid == id);
            return Json(item);
        }
        //modify
        [HttpPost]
        //input
        [HttpPut]
        [Route("internal/AddOrUpdateUIDefinition")]
        public async Task<ActionResult<UIDefinition>> AddOrUpdateUIDefinition([FromBody] UIDefinition uidefinition)
        {
            //AsNoTracker
            //UIDefinition uidef = _context.UIDefinitionen.Include(i => i.EingabeFelder).Include(i => i.KonfiguratorFelder).FirstOrDefault(i => i.UIDefinitionGuid == guid);
            //_context.ChangeTracker.Clear();
            if (uidefinition.UIDefinitionId == 0)
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
                        feld.ChangedDate = DateTime.UtcNow;
                        _context.Entry(feld).State = EntityState.Modified;
                    }

                }
                foreach (var feld in uidefinition.KonfiguratorFelder)
                {
                    if (feld.UIKonfiguratorFeldId == 0)
                        _context.Entry(feld).State = EntityState.Added;
                    if (feld.IsDirty)
                    {
                        feld.ChangedDate = DateTime.UtcNow;
                        _context.Entry(feld).State = EntityState.Modified;
                    }
                }
            }

            uidefinition.ChangedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            uidefinition = SortListsByReihenfolge(uidefinition);
            return Ok(uidefinition);
        }

        [HttpPut]
        [Route("internal/CreateUIDefinition")]
        public async Task<ActionResult<UIDefinition>> CreateNewUIDefinition(string newUIDefName, string? vorhandeneUIDefName = null)
        {
            if (_context.UIDefinitionen.FirstOrDefault(i => i.BezeichnungKurz == newUIDefName) != null)
            {
                return Conflict();
            }
            UIDefinition newUI = new UIDefinition();
            _context.UIDefinitionen.Add(newUI);
            newUI.BezeichnungKurz = newUIDefName;
            newUI.UIDefinitionGuid = Guid.NewGuid();
            newUI.Version = 1;
            newUI.ChangedDate = DateTime.UtcNow;
            if (!String.IsNullOrEmpty(vorhandeneUIDefName))
            {
                var dbUiDef = _context.UIDefinitionen.Include(i => i.EingabeFelder).Include(i => i.KonfiguratorFelder).FirstOrDefault(i => i.BezeichnungKurz == vorhandeneUIDefName);
                if (dbUiDef != null)
                {
                    newUI.Kategorie = dbUiDef.Kategorie;
                    newUI.BezeichnungLang = dbUiDef.BezeichnungLang + "_KOPIE";
                    newUI.BildHorizontal = dbUiDef.BildHorizontal;
                    newUI.BildVertikal = dbUiDef.BildVertikal;
                    newUI.Bild3D = dbUiDef.Bild3D;

                    newUI.IsDirty = true;

                    if (dbUiDef.EingabeFelder != null)
                    {
                        newUI.EingabeFelder = new List<UIEingabeFeld>();
                        foreach (UIEingabeFeld originalFeld in dbUiDef.EingabeFelder)
                        {
                            UIEingabeFeld feld = new UIEingabeFeld();
                            _context.EingabeFelder.Add(feld);
                            feld.BelegBlattText = originalFeld.BelegBlattText;
                            feld.AngebotsText = originalFeld.AngebotsText;
                            feld.Caption = originalFeld.Caption;
                            feld.IstKonfiguratorFeld = originalFeld.IstKonfiguratorFeld;

                            feld.Reihenfolge = originalFeld.Reihenfolge;
                            feld.Tag = originalFeld.Tag;
                            feld.Regel = originalFeld.Regel;
                            feld.MinWert = originalFeld.MinWert;
                            feld.MaxWert = originalFeld.MaxWert;
                            feld.VorgabeWert = originalFeld.VorgabeWert;
                            feld.HilfeText = originalFeld.HilfeText;
                            feld.FehlerText = originalFeld.FehlerText;
                            feld.WerteListeName = originalFeld.WerteListeName;
                            feld.PreisFeldAnzeigen = originalFeld.PreisFeldAnzeigen;
                            feld.MindestBreite = originalFeld.MindestBreite;
                            feld.UIEingabeFeldGuid = Guid.NewGuid();
                            feld.EingabeLevel = originalFeld.EingabeLevel;
                            feld.ZusatzFeldGruppeId = originalFeld.ZusatzFeldGruppeId;
                            feld.GehoertZuZusatzFeldGruppeId = originalFeld.GehoertZuZusatzFeldGruppeId;
                            feld.GueltigAb = originalFeld.GueltigAb;
                            feld.GueltigBis = originalFeld.GueltigBis;
                            feld.ChangedDate = DateTime.UtcNow;

                            feld.IsDirty = true;
                            newUI.EingabeFelder.Add(feld);
                        }
                    }

                    if (dbUiDef.KonfiguratorFelder != null)
                    {
                        newUI.KonfiguratorFelder = new List<UIKonfiguratorFeld>();
                        foreach (UIKonfiguratorFeld originalFeld in dbUiDef.KonfiguratorFelder)
                        {
                            UIKonfiguratorFeld feld = new UIKonfiguratorFeld();
                            _context.KonfiguratorFelder.Add(feld);
                            feld.BelegBlattText = originalFeld.BelegBlattText;
                            feld.AngebotsText = originalFeld.AngebotsText;
                            feld.Caption = originalFeld.Caption;

                            feld.Reihenfolge = originalFeld.Reihenfolge;
                            feld.Tag = originalFeld.Tag;
                            feld.Kuerzel = originalFeld.Kuerzel;
                            feld.VorgabeWert = originalFeld.VorgabeWert;
                            feld.WerteListeName = originalFeld.WerteListeName;
                            feld.UIKonfiguratorFeldGuid = Guid.NewGuid();
                            feld.EingabeLevel = originalFeld.EingabeLevel;
                            feld.ProfilId = originalFeld.ProfilId;
                            feld.GehoertZuProfilId = originalFeld.GehoertZuProfilId;
                            feld.GueltigAb = originalFeld.GueltigAb;
                            feld.GueltigBis = originalFeld.GueltigBis;
                            feld.Version = originalFeld.Version;
                            feld.ChangedDate = DateTime.UtcNow;

                            feld.IsDirty = true;
                            newUI.KonfiguratorFelder.Add(feld);
                        }
                    }
                }
            }

            await _context.SaveChangesAsync();
            newUI = SortListsByReihenfolge(newUI);
            return Ok(newUI);
        }




        [HttpPost]
        [HttpPut]
        [Route("internal/CreateUIFeld")]
        public async Task<ActionResult<UIDefinition>> CreateUIFeld(Guid uiDefGuid)
        {
            UIDefinition uidef = _context.UIDefinitionen.Include(i => i.EingabeFelder).Include(i => i.KonfiguratorFelder).FirstOrDefault(i => i.UIDefinitionGuid == uiDefGuid);
            if (uidef == null) return NotFound();

            UIEingabeFeld newFeld = new UIEingabeFeld();
            newFeld.UIEingabeFeldGuid = Guid.NewGuid();
            newFeld.ChangedDate = DateTime.UtcNow;
            newFeld.Reihenfolge = 0;
            newFeld.EingabeLevel = 1;
            newFeld.IsDirty = true;

            _context.EingabeFelder.Add(newFeld);
            uidef.EingabeFelder.Add(newFeld);
            await _context.SaveChangesAsync();

            uidef = SortListsByReihenfolge(uidef);
            return Ok(uidef);
        }

        [HttpPut]
        [Route("internal/CopyUIFeld")]
        public async Task<ActionResult<UIDefinition>> CopyUIFeld(string destUIDefName, Guid uiFeldGuid)
        {
            UIDefinition uidef = _context.UIDefinitionen.Include(i => i.EingabeFelder).Include(i => i.KonfiguratorFelder).FirstOrDefault(i => i.BezeichnungKurz == destUIDefName);
            UIEingabeFeld sourceUIFeld = _context.EingabeFelder.FirstOrDefault(i => i.UIEingabeFeldGuid == uiFeldGuid);
            if (uidef == null || sourceUIFeld == null) return NotFound();


            UIEingabeFeld feld = new UIEingabeFeld();
            _context.EingabeFelder.Add(feld);
            feld.BelegBlattText = sourceUIFeld.BelegBlattText;
            feld.AngebotsText = sourceUIFeld.AngebotsText;
            feld.Caption = sourceUIFeld.Caption;
            feld.IstKonfiguratorFeld = sourceUIFeld.IstKonfiguratorFeld;

            feld.Reihenfolge = sourceUIFeld.Reihenfolge;
            feld.Tag = sourceUIFeld.Tag;
            feld.Regel = sourceUIFeld.Regel;
            feld.MinWert = sourceUIFeld.MinWert;
            feld.MaxWert = sourceUIFeld.MaxWert;
            feld.VorgabeWert = sourceUIFeld.VorgabeWert;
            feld.HilfeText = sourceUIFeld.HilfeText;
            feld.FehlerText = sourceUIFeld.FehlerText;
            feld.WerteListeName = sourceUIFeld.WerteListeName;
            feld.PreisFeldAnzeigen = sourceUIFeld.PreisFeldAnzeigen;
            feld.MindestBreite = sourceUIFeld.MindestBreite;
            feld.UIEingabeFeldGuid = Guid.NewGuid();
            feld.EingabeLevel = sourceUIFeld.EingabeLevel;
            feld.ZusatzFeldGruppeId = sourceUIFeld.ZusatzFeldGruppeId;
            feld.GehoertZuZusatzFeldGruppeId = sourceUIFeld.GehoertZuZusatzFeldGruppeId;
            feld.GueltigAb = sourceUIFeld.GueltigAb;
            feld.GueltigBis = sourceUIFeld.GueltigBis;
            feld.ChangedDate = DateTime.UtcNow;

            feld.IsDirty = true;
            uidef.EingabeFelder.Add(feld);

            await _context.SaveChangesAsync();
            uidef = SortListsByReihenfolge(uidef);
            return Ok(true);
        }

        [HttpDelete]
        [Route("internal/DeleteUIFeld")]
        public async Task<ActionResult<UIDefinition>> DeleteUIFeld(Guid guid)
        {
            UIDefinition uidef = _context.UIDefinitionen.Include(i => i.EingabeFelder).Include(i => i.KonfiguratorFelder).FirstOrDefault(i => i.EingabeFelder.Any(f => f.UIEingabeFeldGuid == guid));
            if (uidef == null) return NotFound();

            UIEingabeFeld uiFeld = uidef.EingabeFelder.FirstOrDefault(i => i.UIEingabeFeldGuid == guid);
            _context.Entry(uiFeld).State = EntityState.Deleted;


            await _context.SaveChangesAsync();

            uidef = SortListsByReihenfolge(uidef);
            return Ok(uidef);
        }


        private UIDefinition SortListsByReihenfolge(UIDefinition uidef)
        {
            uidef.EingabeFelder = uidef.EingabeFelder.OrderBy(i => i.Reihenfolge).ToList();
            uidef.KonfiguratorFelder = uidef.KonfiguratorFelder.OrderBy(i => i.Reihenfolge).ToList();

            return uidef;
        }





    }
}
