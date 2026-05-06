using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ABEP.Data;
using ABEP.Models;

namespace ABEP.Controllers
{
    public class DisasterController : Controller
    {
        private readonly AppDbContext _context;

        public DisasterController(AppDbContext context)
        {
            _context = context;
        }

        // ── Afet Türleri Listesi ────────────────────────────────────
        public async Task<IActionResult> Index()
        {
            var all = await _context.Disasters
                .Where(d => d.IsPublished)
                .OrderByDescending(d => d.IsFeatured)
                .ThenByDescending(d => d.ViewCount)
                .ToListAsync();

            var vm = new DisasterIndexViewModel
            {
                Featured = all.FirstOrDefault(d => d.IsFeatured),
                Disasters = all.Where(d => !d.IsFeatured).ToList()
            };

            return View(vm);
        }

        // ── Afet Detay Sayfası ──────────────────────────────────────
        public async Task<IActionResult> Details(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
                return NotFound();

            var disaster = await _context.Disasters
                .FirstOrDefaultAsync(d => d.Slug == slug && d.IsPublished);

            if (disaster == null)
                return NotFound();

            // Görüntülenme sayısını artır
            disaster.ViewCount++;
            await _context.SaveChangesAsync();

            // Aynı kategoriden ilgili afetler (en fazla 3, kendisi hariç)
            var related = await _context.Disasters
                .Where(d => d.IsPublished && d.Category == disaster.Category && d.Id != disaster.Id)
                .Take(3)
                .ToListAsync();

            var vm = new DisasterDetailsViewModel
            {
                Disaster = disaster,
                Related = related
            };

            return View(vm);
        }

        // ────────────────────────────────────────────────────────────
        // ADMIN CRUD — Admin panelinden çağrılır
        // ────────────────────────────────────────────────────────────

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Create([FromBody] DisasterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            model.UpdatedAt = DateTime.Now;
            model.UpdatedBy = User.Identity?.Name ?? "Admin";
            _context.Disasters.Add(model);
            await _context.SaveChangesAsync();
            return Ok(new { id = model.Id });
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Update([FromBody] DisasterModel model)
        {
            var existing = await _context.Disasters.FindAsync(model.Id);
            if (existing == null) return NotFound();

            // Alan kopyalama
            existing.Name             = model.Name;
            existing.Slug             = model.Slug;
            existing.Category         = model.Category;
            existing.RiskLevel        = model.RiskLevel;
            existing.Icon             = model.Icon;
            existing.IconColor        = model.IconColor;
            existing.ImageUrl         = model.ImageUrl;
            existing.HeroImageUrl     = model.HeroImageUrl;
            existing.IsFeatured       = model.IsFeatured;
            existing.IsPublished      = model.IsPublished;
            existing.ModuleCount      = model.ModuleCount;
            existing.ShortDescription = model.ShortDescription;
            existing.Description      = model.Description;

            existing.WhatIsTitle    = model.WhatIsTitle;
            existing.WhatIsLead     = model.WhatIsLead;
            existing.WhatIsBody     = model.WhatIsBody;
            existing.WhatIsCallout  = model.WhatIsCallout;
            existing.VideoUrl       = model.VideoUrl;
            existing.VideoThumbnailUrl = model.VideoThumbnailUrl;
            existing.VideoCaption   = model.VideoCaption;

            existing.BeforeTitle     = model.BeforeTitle;
            existing.BeforeIntro     = model.BeforeIntro;
            existing.BeforeChecklist = model.BeforeChecklist;

            existing.DuringTitle      = model.DuringTitle;
            existing.DuringWarning    = model.DuringWarning;
            existing.DuringPhase1Label = model.DuringPhase1Label;
            existing.DuringPhase1Title = model.DuringPhase1Title;
            existing.DuringPhase1Items = model.DuringPhase1Items;
            existing.DuringPhase2Label = model.DuringPhase2Label;
            existing.DuringPhase2Title = model.DuringPhase2Title;
            existing.DuringPhase2Items = model.DuringPhase2Items;
            existing.DuringPhase3Label = model.DuringPhase3Label;
            existing.DuringPhase3Title = model.DuringPhase3Title;
            existing.DuringPhase3Items = model.DuringPhase3Items;
            existing.DuringExtra      = model.DuringExtra;

            existing.AfterTitle     = model.AfterTitle;
            existing.AfterIntro     = model.AfterIntro;
            existing.AfterChecklist = model.AfterChecklist;
            existing.AfterCallout   = model.AfterCallout;

            existing.UpdatedAt = DateTime.Now;
            existing.UpdatedBy = User.Identity?.Name ?? "Admin";

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var disaster = await _context.Disasters.FindAsync(id);
            if (disaster == null) return NotFound();

            _context.Disasters.Remove(disaster);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> TogglePublish(int id)
        {
            var disaster = await _context.Disasters.FindAsync(id);
            if (disaster == null) return NotFound();

            disaster.IsPublished = !disaster.IsPublished;
            disaster.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return Ok(new { isPublished = disaster.IsPublished });
        }

        // Admin paneli için tüm afetleri JSON döner
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _context.Disasters
                .OrderByDescending(d => d.UpdatedAt)
                .Select(d => new {
                    d.Id, d.Name, d.Category, d.RiskLevel, d.Icon, d.IconColor,
                    d.ModuleCount, d.ViewCount, d.IsPublished, d.IsFeatured,
                    d.UpdatedBy, d.UpdatedAt
                })
                .ToListAsync();

            return Json(list);
        }

        // Düzenleme modalı için tek afet JSON döner
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var disaster = await _context.Disasters.FindAsync(id);
            if (disaster == null) return NotFound();
            return Json(disaster);
        }
    }
}