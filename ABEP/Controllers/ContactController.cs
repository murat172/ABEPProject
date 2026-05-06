using ABEP.Data;
using ABEP.Models;
using Microsoft.AspNetCore.Mvc;

namespace ABEP.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;
        public ContactController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(new ContactModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(ContactModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value!.Errors.Count > 0)
                    .ToDictionary(
                        k => k.Key,
                        v => v.Value!.Errors.Select(e => e.ErrorMessage).FirstOrDefault()
                    );
                return Json(new { success = false, errors });
            }

            _context.Contacts.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }
    }
}