using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AlmondWeb.BusinessLayer.ViewModels
{
    public class ProfileModal
    {
        public int ownerId { get; set; }
        [MaxLength(50, ErrorMessage = "{0} maksimum {1} karakter olabilir."), DisplayName("Okul")]
        public string school { get; set; }
        [MaxLength(35, ErrorMessage = "{0} alanı en fazla {1} karakterden oluşmalıdır."), MinLength(3, ErrorMessage = "{0} alanı en az {1} karakterden oluşmalıdır."), DisplayName("Meslek"), Required(ErrorMessage = "{0} alanı gereklidir.")]
        public string job { get; set; }
        [MaxLength(50, ErrorMessage = "{0} maksimum {1} karakter olabilir."), DisplayName("Github Adresi")]
        public string githubUrl { get; set; }
        [MaxLength(75, ErrorMessage = "{0} maksimum {1} karakter olabilir."), DisplayName("Linkedin Adresi")]
        public string linkedinUrl { get; set; }
        [MaxLength(75, ErrorMessage = "{0} maksimum {1} karakter olabilir."), DisplayName("Bağlantı Adresi")]
        public string websiteUrl { get; set; }
        [MaxLength(250, ErrorMessage = "{0} maksimum {1} karakter olabilir."), DisplayName("Hakkımda")]
        public string aboutmeText { get; set; }
        [DisplayName("Profil Fotoğrafı")]
        public string profileImageUrl { get; set; }
    }
}
