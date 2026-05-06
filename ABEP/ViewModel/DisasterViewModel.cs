namespace ABEP.Models
{
    public class DisasterIndexViewModel
    {
        public DisasterModel? Featured { get; set; }
        public List<DisasterModel> Disasters { get; set; } = new();
    }

    public class DisasterDetailsViewModel
    {
        public DisasterModel Disaster { get; set; } = new();
        public List<DisasterModel> Related { get; set; } = new();  // Aynı kategoriden diğerleri (max 3)
    }
}