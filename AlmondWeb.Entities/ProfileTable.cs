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
        [StringLength(maximumLength: 50, MinimumLength = 3), DisplayName("Meslek"), Required]
        public string job { get; set; }
        public string githubUrl { get; set; }
        public string almondUrl { get; set; }
        public string linkedinUrl { get; set; }
        [Required, StringLength(maximumLength: 250, MinimumLength = 10), DisplayName("Hakkımda")]
        public string aboutmeText { get; set; }
        public string profileImageUrl { get; set; }
        //[ForeignKey(nameof(Owner))]
        //public int OwnerId { get; set; }
        public virtual AlmondUserTable Owner { get; set; }
    }
}
