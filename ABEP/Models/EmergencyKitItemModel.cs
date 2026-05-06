namespace ABEP.Models
{
    public class EmergencyKitItemModel
    {
        public int Id { get; set; }
        public string Icon { get; set; } = null!;        
        public string Item { get; set; } = null!;       
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string UpdatedBy { get; set; } = "Admin";
    }
}