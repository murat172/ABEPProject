using ABEP.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ABEP.Data
{
    // IdentityDbContext<ApplicationUser> olarak değiştiriyoruz
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<HeroModel> Heroes { get; set; }
        public DbSet<HowToPrepareModel> HowToPrepares { get; set; }
        public DbSet<EmergencyModel> Emergencies { get; set; }
        public DbSet<EmergencyKitItemModel> EmergencyKitItems { get; set; }
        public DbSet<EmergencyPhoneModel> EmergencyPhones { get; set; }
        public DbSet<ContactModel> Contacts { get; set; }
        public DbSet<ExperiencesModel> Experiences { get; set; }
        public DbSet<DisasterModel> Disasters { get; set; }
    }
}