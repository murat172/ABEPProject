namespace ABEP.Models
{
    public class HeroModel
    {
        public int Id { get; set; }
        public string Eyebrow { get; set; } = null!;
        public string TitleLine1 { get; set; } = null!;
        public string TitleLine2 { get; set; } = null!;
        public string TitleLine3 { get; set; } = null!;
        public string SubTitle { get; set; } = null!;
        public string Btn1Text { get; set; } = null!;
        public string Btn1Url { get; set; } = null!;
        public string Btn2Text { get; set; } = null!;
        public string Btn2Url { get; set; } = null!;
        public string? ImagePath { get; set; }
        public int OverlayOpacity { get; set; } = 40;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string UpdatedBy { get; set; } = "Admin";
        public bool IsPublished { get; set; } = true;

    }
}