using ABEP.Data;
using ABEP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ABEP.Controllers.Admin
{
    public class HowToPrepareController : Controller
    {
        private readonly AppDbContext _context;
        public HowToPrepareController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var howToPrepare = _context.HowToPrepares.FirstOrDefault() ?? new HowToPrepareModel()
                {
                    Eyebrow = "",
                    Title = "",
                    StepItem1Title = "",
                    StepItem1Description = "",
                    StepItem2Title = "",
                    StepItem2Description = "",
                    StepItem3Title = "",
                    StepItem3Description = "",
                    StepItem4Title = "",
                    StepItem4Description = "",
                    ImageUrl = "",
                    UpdatedAt = DateTime.Now,
                };
                return View("_HowToPrepareAdmin", howToPrepare);
            }
    
        [HttpPost]

        public async Task<IActionResult> Save(HowToPrepareModel model)
        {
            model.UpdatedAt = DateTime.Now;
            model.UpdatedBy = "Admin";

            var existing = await _context.HowToPrepares.FirstOrDefaultAsync();

            if(existing == null)
            {
                _context.HowToPrepares.Add(model);
            }
            else
            {
                existing.Eyebrow = model.Eyebrow;
                existing.Title = model.Title;
                existing.StepItem1Title = model.StepItem1Title;
                existing.StepItem1Description = model.StepItem1Description;
                existing.StepItem2Title = model.StepItem2Title;
                existing.StepItem2Description = model.StepItem2Description;
                existing.StepItem3Title = model.StepItem3Title;
                existing.StepItem3Description = model.StepItem3Description;
                existing.StepItem4Title = model.StepItem4Title;
                existing.StepItem4Description = model.StepItem4Description;
                existing.ImageUrl = model.ImageUrl;
                existing.UpdatedAt = model.UpdatedAt;
                existing.UpdatedBy = model.UpdatedBy;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Admin");
        }
    }
}
