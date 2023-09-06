using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace AlmondWeb.BusinessLayer.ViewModels
{
    public class ProfileModal
    {
        public int ownerId { get; set; }
        [StringLength(maximumLength: 50, MinimumLength = 3), DisplayName("Okul")]
        public string school { get; set; }
        [MaxLength(23, ErrorMessage = "{0} alanı en fazla {1} karakterden oluşmalıdır."), MinLength(3), DisplayName("Meslek"), Required(ErrorMessage = "{0} alanı gereklidir.")]
        public string job { get; set; }
        [StringLength(maximumLength: 50, MinimumLength = 10), DisplayName("Github Adresi")]
        public string githubUrl { get; set; }
        [StringLength(maximumLength: 75, MinimumLength = 15), DisplayName("Linkedin Adresi")]
        public string linkedinUrl { get; set; }
        [StringLength(maximumLength: 75, MinimumLength = 5), DisplayName("Bağlantı Adresi")]
        public string websiteUrl { get; set; }
        [Required, StringLength(maximumLength: 250, MinimumLength = 10), DisplayName("Hakkımda")]
        public string aboutmeText { get; set; }
        [DisplayName("Profil Fotoğrafı")]
        public string profileImageUrl { get; set; }
    }
}
