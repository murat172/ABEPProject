using ABEP.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ABEP.Controllers.Admin.Emergency
{
    public class EmergencyController : Controller
    {
        private readonly AppDbContext _context;
        public EmergencyController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult EmergencyPage()
        {
            var emergencyPage = _context.Emergencies.FirstOrDefault() ?? new Models.EmergencyModel()
            {
                Eyebrow = "",
                Title1 = "",
                Description1 = "",
                Title2 = "",
                Description2 = "",
                UpdatedAt = DateTime.Now,
                UpdatedBy = "Admin"
            };
            return View("_HeroAdmin", emergencyPage); 
        }

        [HttpPost]
        public async Task<IActionResult> EmergencyPageSave(Models.EmergencyModel model)
        {
            model.UpdatedAt = DateTime.Now;
            model.UpdatedBy = "Admin";

            var existing = await _context.Emergencies.FirstOrDefaultAsync();

            if(existing == null)
            {
                _context.Emergencies.Add(model);
            }
            else
            {
                existing.Eyebrow = model.Eyebrow;
                existing.Title1 = model.Title1;
                existing.Description1 = model.Description1;
                existing.Title2 = model.Title2;
                existing.Description2 = model.Description2;
                existing.UpdatedAt = model.UpdatedAt;
                existing.UpdatedBy = model.UpdatedBy;
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        public async Task<IActionResult> EmergencyKitList()
        {
            var emergencyKitList = await _context.EmergencyKitItems.ToListAsync();
            return View(emergencyKitList);
        }

        [HttpPost]
        public async Task<IActionResult> EmergencyKitSave(Models.EmergencyKitItemModel model)
        {
            model.UpdatedAt = DateTime.Now;
            model.UpdatedBy = "Admin";
            _context.EmergencyKitItems.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Admin");
        }

        [HttpPost]
        public async Task<IActionResult> EmergencyKitUpdate(Models.EmergencyKitItemModel model)
        {
            var Item = await _context.EmergencyKitItems.FindAsync(model.Id);
            if(Item == null) return NotFound();

            Item.Icon = model.Icon;
            Item.Item = model.Item;
            Item.UpdatedAt = DateTime.Now;
            Item.UpdatedBy = "Admin";
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Admin");
        }

        [HttpPost]
        public async Task<IActionResult> EmergencyKitDelete(int id)
        {
            var Item = await _context.EmergencyKitItems.FindAsync(id);
            if(Item == null) return NotFound();
            _context.EmergencyKitItems.Remove(Item);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Admin");
        }


        [HttpGet]
        public async Task<IActionResult> EmergencyPhoneList()
        {
            var emergencyPhoneList = await _context.EmergencyPhones.ToListAsync();
            return View(emergencyPhoneList);
        }

        [HttpPost]
        public async Task<IActionResult> EmergencyPhoneSave(Models.EmergencyPhoneModel model)
        {
            model.UpdatedAt = DateTime.Now;
            model.UpdatedBy = "Admin";
            _context.EmergencyPhones.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Admin");
        }

        [HttpPost]
        public async Task<IActionResult> EmergencyPhoneUpdate(Models.EmergencyPhoneModel model)
        {
            var Item = await _context.EmergencyPhones.FindAsync(model.Id);
            if(Item == null) return NotFound();

            Item.PhoneName = model.PhoneName;
            Item.Phone = model.Phone;
            Item.UpdatedAt = DateTime.Now;
            Item.UpdatedBy = "Admin";
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Admin");
        }

        [HttpPost]
        public async Task<IActionResult> EmergencyPhoneDelete(int id)
        {
            var Phone = await _context.EmergencyPhones.FindAsync(id);
            if(Phone == null) return NotFound();
            _context.EmergencyPhones.Remove(Phone);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Admin");
        }
    }
}