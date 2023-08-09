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
        [StringLength(maximumLength: 50, MinimumLength = 3), DisplayName("Meslek")]
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
        public string profileImageUrl { get; set; }
        public virtual AlmondUserTable Owner { get; set; }
        public virtual List<SharedListTable> Profil { get; set; }
    }
}
