using System.ComponentModel.DataAnnotations;

namespace ABEP.Models
{
    public class DisasterModel
    {
        public int Id { get; set; }

        // ── Temel Bilgiler ──────────────────────────────────────────
        [Required] public string Name { get; set; } = "";          
        [Required] public string Slug { get; set; } = "";          
        public string Category { get; set; } = "";                 
        public string RiskLevel { get; set; } = "";                
        public string Icon { get; set; } = "";                   
        public string IconColor { get; set; } = "red";            
        public string ImageUrl { get; set; } = "";
        public bool IsFeatured { get; set; } = false;
        public bool IsPublished { get; set; } = false;
        public int ModuleCount { get; set; } = 0;
        public int ViewCount { get; set; } = 0;

        // ── Index kartı ────────────────────────────────────────────
        public string ShortDescription { get; set; } = "";       

        // ── Hero (Detay sayfası üst bölüm) ──────────────────────
        public string HeroImageUrl { get; set; } = "";            
        public string Description { get; set; } = "";             

        // ── Nedir Bölümü ───────────────────────────────────────
        public string WhatIsTitle { get; set; } = "";             
        public string WhatIsLead { get; set; } = "";
        public string WhatIsBody { get; set; } = "";
        public string WhatIsCallout { get; set; } = "";          
        public string VideoUrl { get; set; } = "";
        public string VideoThumbnailUrl { get; set; } = "";
        public string VideoCaption { get; set; } = "";

        // ── Önce Bölümü ──────────────────────────────────────────
        public string BeforeTitle { get; set; } = "";
        public string BeforeIntro { get; set; } = "";
        public string BeforeChecklist { get; set; } = "";         

        // ── Sırasında Bölümü ─────────────────────────────────────
        public string DuringTitle { get; set; } = "";
        public string DuringWarning { get; set; } = "";          
        public string DuringPhase1Label { get; set; } = "";     
        public string DuringPhase1Title { get; set; } = "";     
        public string DuringPhase1Items { get; set; } = "";     
        public string DuringPhase2Label { get; set; } = "";
        public string DuringPhase2Title { get; set; } = "";
        public string DuringPhase2Items { get; set; } = "";     
        public string DuringPhase3Label { get; set; } = "";
        public string DuringPhase3Title { get; set; } = "";
        public string DuringPhase3Items { get; set; } = "";     
        public string DuringExtra { get; set; } = "";            

        // ── Sonrasında Bölümü ────────────────────────────────────
        public string AfterTitle { get; set; } = "";
        public string AfterIntro { get; set; } = "";
        public string AfterChecklist { get; set; } = "";         
        public string AfterCallout { get; set; } = "";        public string UpdatedBy { get; set; } = "Admin";
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}