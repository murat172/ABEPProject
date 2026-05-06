using System.ComponentModel.DataAnnotations;

namespace ABEP.Models
{
    public class ContactModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ad Soyad zorunludur.")]
        public string FullName { get; set; } = null!;

        [Required(ErrorMessage = "E-posta zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta giriniz.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Konu zorunludur.")]
        public string Subject { get; set; } = null!;

        [Required(ErrorMessage = "Mesaj zorunludur.")]
        public string Message { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}