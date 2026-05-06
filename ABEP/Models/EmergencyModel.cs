namespace ABEP.Models
{
    public class EmergencyModel
    {
        public int Id { get; set; }
        public string Eyebrow { get; set; } = null!;
        public string Title1 { get; set; } = null!;
        public string Description1 { get; set; } = null!;
        public string Title2 { get; set; } = null!;
        public string Description2 { get; set; } = null!;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string UpdatedBy { get; set; } = "Admin";
    }
}