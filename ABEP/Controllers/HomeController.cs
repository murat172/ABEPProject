using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ABEP.Models;
using ABEP.Data;
using Microsoft.AspNetCore.Identity;

namespace ABEP.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var userManager = HttpContext.RequestServices
                              .GetRequiredService<UserManager<ApplicationUser>>();

        var hero = await _context.Heroes.FirstOrDefaultAsync() ?? new HeroModel
        {
            Eyebrow = "", TitleLine1 = "", TitleLine2 = "", TitleLine3 = "",
            SubTitle = "", Btn1Text = "", Btn1Url = "", Btn2Text = "", Btn2Url = ""
        };

        var howToPrepare = await _context.HowToPrepares.FirstOrDefaultAsync() ?? new HowToPrepareModel
        {
            Eyebrow = "", Title = "", ImageUrl = "",
            StepItem1Title = "", StepItem1Description = "",
            StepItem2Title = "", StepItem2Description = "",
            StepItem3Title = "", StepItem3Description = "",
            StepItem4Title = "", StepItem4Description = ""
        };

        var emergency = await _context.Emergencies.FirstOrDefaultAsync() ?? new EmergencyModel
        {
            Eyebrow = "", Title1 = "", Description1 = "", Title2 = "", Description2 = ""
        };

        // ── Hero istatistikleri ──
        var userCount         = await userManager.Users.CountAsync();
        var disasterCount     = await _context.Disasters.CountAsync();
        const int catCount    = 5; 

        ViewData["Hero"]              = hero;
        ViewData["HowToPrepare"]      = howToPrepare;
        ViewData["KitItems"]          = await _context.EmergencyKitItems.ToListAsync();
        ViewData["Phones"]            = await _context.EmergencyPhones.ToListAsync();
        ViewData["Top6Disasters"]     = await _context.Disasters
                                            .OrderByDescending(d => d.UpdatedAt)
                                            .Take(6)
                                            .ToListAsync();
        ViewData["StatUserCount"]     = userCount;
        ViewData["StatDisasterCount"] = disasterCount;
        ViewData["StatCategoryCount"] = catCount;

        return View(emergency);
    }
}