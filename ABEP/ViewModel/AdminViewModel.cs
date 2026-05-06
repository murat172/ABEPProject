namespace ABEP.Models
{
    public class AdminViewModel
    {
        public HeroModel? hero { get; set; }
        public HowToPrepareModel? howToPrepare { get; set; }
        public EmergencyModel? emergencyPage { get; set; }
        public List<EmergencyKitItemModel> emergencyKitItems { get; set; } = new();
        public List<EmergencyPhoneModel> emergencyPhones { get; set; } = new();
        public List<ContactModel> contacts { get; set; } = new();
        public List<DisasterModel> disasters { get; set; } = new();

        // ── Dashboard İstatistikleri ──
        public int TotalUsers       { get; set; }
        public int TotalExperiences { get; set; }
        public int NewMessages      { get; set; }
        public int PublishedContent { get; set; }
    }
}