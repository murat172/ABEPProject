using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ABEP.Data;
using ABEP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace ABEP.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;

        public AdminController(UserManager<ApplicationUser> userManager,
                               RoleManager<IdentityRole> roleManager,
                               AppDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            var hero = await _context.Heroes.FirstOrDefaultAsync() ?? new HeroModel
            {
                Eyebrow = "", TitleLine1 = "", TitleLine2 = "", TitleLine3 = "",
                SubTitle = "", Btn1Text = "", Btn1Url = "", Btn2Text = "", Btn2Url = "",
                UpdatedBy = "—", UpdatedAt = DateTime.Now, IsPublished = false
            };

            var howToPrepare = await _context.HowToPrepares.FirstOrDefaultAsync() ?? new HowToPrepareModel
            {
                Eyebrow = "", Title = "", ImageUrl = "",
                StepItem1Title = "", StepItem1Description = "",
                StepItem2Title = "", StepItem2Description = "",
                StepItem3Title = "", StepItem3Description = "",
                StepItem4Title = "", StepItem4Description = "",
                UpdatedBy = "—", UpdatedAt = DateTime.Now
            };

            var emergencyPage = await _context.Emergencies.FirstOrDefaultAsync() ?? new EmergencyModel
            {
                Eyebrow = "", Title1 = "", Description1 = "",
                Title2 = "", Description2 = "",
                UpdatedAt = DateTime.Now, UpdatedBy = "Admin"
            };

            // ── Dashboard İstatistikleri ──
            var totalUsers       = await _userManager.Users.CountAsync();
            var totalExperiences = await _context.Experiences.CountAsync();
            var newMessages      = await _context.Contacts.CountAsync();

            var publishedContent = await _context.Disasters.CountAsync();

            var vm = new AdminViewModel
            {
                hero             = hero,
                howToPrepare     = howToPrepare,
                emergencyPage    = emergencyPage,
                emergencyKitItems = await _context.EmergencyKitItems.ToListAsync(),
                emergencyPhones  = await _context.EmergencyPhones.ToListAsync(),
                contacts         = await _context.Contacts
                                    .OrderByDescending(c => c.CreatedAt)
                                    .ToListAsync(),
                disasters        = await _context.Disasters
                                    .OrderByDescending(d => d.UpdatedAt)
                                    .ToListAsync(),

                // İstatistikler
                TotalUsers       = totalUsers,
                TotalExperiences = totalExperiences,
                NewMessages      = newMessages,
                PublishedContent = publishedContent
            };

            return View(vm);
        }

        
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            var list = new List<object>();
 
            foreach (var u in users)
            {
                var roles = await _userManager.GetRolesAsync(u);
                list.Add(new
                {
                    id       = u.Id,
                    fullName = u.FullName,
                    email    = u.Email,
                    role     = roles.FirstOrDefault() ?? "User",
                    date     = u.RegisteredAt.ToString("dd.MM.yyyy")
                });
            }
            return Json(list);
        }
 
        // ── KULLANICI ROLÜNÜ DEĞİŞTİR ──
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeRole(string userId, string newRole)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Json(new { success = false, message = "Kullanıcı bulunamadı." });
 
            
            if (!await _roleManager.RoleExistsAsync(newRole))
                return Json(new { success = false, message = "Rol mevcut değil." });
 
            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);
 
            await _userManager.AddToRoleAsync(user, newRole);
 
            return Json(new { success = true, message = $"Rol '{newRole}' olarak güncellendi." });
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Json(new { success = false, message = "Kullanıcı bulunamadı." });
 
            // Kendi kendini silemesin
            var currentUserId = _userManager.GetUserId(User);
            if (user.Id == currentUserId)
                return Json(new { success = false, message = "Kendinizi silemezsiniz." });
 
            await _userManager.DeleteAsync(user);
            return Json(new { success = true });
        }
 
        // ── ADMİN KULLANICI LİSTESİ (JSON) ──
        [HttpGet]
        public async Task<IActionResult> GetAdmins()
        {
            var admins = await _userManager.GetUsersInRoleAsync("Admin");
            var list = admins.Select(u => new
            {
                id       = u.Id,
                fullName = u.FullName,
                email    = u.Email,
                date     = u.RegisteredAt.ToString("dd.MM.yyyy")
            });
            return Json(list);
        }
    }
}