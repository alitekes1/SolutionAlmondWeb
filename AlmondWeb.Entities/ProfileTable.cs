using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlmondWeb.Entities
{
    public class ProfileTable
    {
        [Key, ForeignKey(nameof(Owner))]
        public int Id { get; set; }
        [StringLength(maximumLength: 50, MinimumLength = 3), DisplayName("Okul")]
        public string school { get; set; }
        [MaxLength(35, ErrorMessage = "{0} alanı en fazla {1} karakterden oluşmalıdır."), MinLength(3, ErrorMessage = "{0} alanı en az {1} karakterden oluşmalıdır."), DisplayName("Meslek"), Required(ErrorMessage = "{0} alanı gereklidir.")]
        public string job { get; set; }
        [StringLength(maximumLength: 50, MinimumLength = 10)]
        public string githubUrl { get; set; }
        public string almondUrl { get; set; }
        [StringLength(maximumLength: 75, MinimumLength = 15)]
        public string linkedinUrl { get; set; }
        [StringLength(maximumLength: 75, MinimumLength = 5)]
        public string websiteUrl { get; set; }
        [StringLength(maximumLength: 250, MinimumLength = 10), DisplayName("Hakkımda")]
        public string aboutmeText { get; set; }
        public string profileImageUrl { get; set; } = "https://i.imgur.com/iZhMGsd.jpg";
        public virtual AlmondUserTable Owner { get; set; }
        public virtual List<SharedListTable> Profil { get; set; }
    }
}
