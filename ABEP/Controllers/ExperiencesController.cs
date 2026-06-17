using ABEP.Data;
using ABEP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ABEP.Controllers
{
    public class ExperiencesController : Controller
    {
        private readonly AppDbContext _context;
        public ExperiencesController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Experiences"] = await _context.Experiences.ToListAsync();
            return View(new ExperiencesModel());
        }

        [HttpPost]
        [Authorize]  
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Experiences(ExperiencesModel model)
        {
            _context.Experiences.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Experiences");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Experiences.FindAsync(id);
            if (item != null)
            {
                _context.Experiences.Remove(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Experiences");
        }
    }
}