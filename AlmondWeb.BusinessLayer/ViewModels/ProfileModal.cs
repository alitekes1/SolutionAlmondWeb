using AlmondWeb.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmondWeb.BusinessLayer.ViewModels
{
    public class ProfileModal
    {
        public int ownerId { get; set; }
        [StringLength(maximumLength: 50, MinimumLength = 3), DisplayName("Okul")]
        public string school { get; set; }
        [StringLength(maximumLength: 50, MinimumLength = 3), DisplayName("Meslek"), Required]
        public string job { get; set; }
        [StringLength(maximumLength: 50, MinimumLength = 3), DisplayName("Github Adresi")]
        public string githubUrl { get; set; }
        [StringLength(maximumLength: 50, MinimumLength = 3), DisplayName("Almond Profil Adresi")]
        public string almondUrl { get; set; }
        [StringLength(maximumLength: 50, MinimumLength = 3), DisplayName("Linkedin Adresi")]
        public string linkedinUrl { get; set; }
        [StringLength(maximumLength: 50, MinimumLength = 3), DisplayName("Bağlantı Adresi")]
        public string websiteUrl { get; set; }
        [Required, StringLength(maximumLength: 250, MinimumLength = 10), DisplayName("Hakkımda")]
        public string aboutmeText { get; set; }
        [DisplayName("Profil Fotoğrafı"), Required(ErrorMessage = "{0} alanı gereklidir.")]
        public string profileImageUrl { get; set; }
    }
}
