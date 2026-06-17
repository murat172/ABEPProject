using System.Threading.Tasks;
using ABEP.Data;
using ABEP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ABEP.Controllers.Admin
{


    public class HeroController : Controller
    {
        private readonly AppDbContext _context;

        public HeroController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var hero = await _context.Heroes.FirstOrDefaultAsync() ?? new HeroModel
            {
                Eyebrow     = "",
                TitleLine1  = "",
                TitleLine2  = "",
                TitleLine3  = "",
                SubTitle    = "",
                Btn1Text    = "",
                Btn1Url     = "",
                Btn2Text    = "",
                Btn2Url     = "",
                UpdatedBy   = "—",
                UpdatedAt   = DateTime.Now,
                IsPublished = false
            };
            return View("_HeroAdmin", hero); 
        }

        [HttpPost]
        public async Task<IActionResult> Save(HeroModel model)
        {
            model.UpdatedAt = DateTime.Now;
            model.UpdatedBy = "Admin";

            var existing = await _context.Heroes.FirstOrDefaultAsync();

            if (existing == null)
            {
                _context.Heroes.Add(model);
            }
            else
            {
                existing.TitleLine1      = model.TitleLine1;
                existing.TitleLine2      = model.TitleLine2;
                existing.TitleLine3      = model.TitleLine3;
                existing.SubTitle        = model.SubTitle;
                existing.Btn1Text        = model.Btn1Text;
                existing.Btn1Url         = model.Btn1Url;
                existing.Btn2Text        = model.Btn2Text;
                existing.Btn2Url         = model.Btn2Url;
                existing.UpdatedAt       = model.UpdatedAt;
                existing.UpdatedBy       = model.UpdatedBy;
                existing.IsPublished     = model.IsPublished;
                existing.OverlayOpacity  = model.OverlayOpacity;
                existing.Eyebrow         = model.Eyebrow;
                existing.ImagePath       = model.ImagePath; 
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Admin");
        }
    }
}