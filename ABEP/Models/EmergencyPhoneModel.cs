namespace ABEP.Models
{
    public class EmergencyPhoneModel
    {
        public int Id { get; set; }
        public string PhoneName { get; set; } = null!; 
        public string Phone { get; set; } = null!;    
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string UpdatedBy { get; set; } = "Admin";
    }
}