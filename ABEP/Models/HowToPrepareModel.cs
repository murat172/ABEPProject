namespace ABEP.Models
{
    public class HowToPrepareModel
    {
        public int Id { get; set; }
        public string Eyebrow { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string? StepItem1Title { get; set; }
        public string? StepItem1Description { get; set; }

        public string? StepItem2Title { get; set; }
        public string? StepItem2Description { get; set; }

        public string? StepItem3Title { get; set; }
        public string? StepItem3Description { get; set; }

        public string? StepItem4Title { get; set; }
        public string? StepItem4Description { get; set; }

        public string ImageUrl { get; set; }=null!;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public string UpdatedBy { get; set; } = "Admin";
        
    }
}