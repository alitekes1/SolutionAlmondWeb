using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmondWeb.Entities
{
    public class ProfileTable
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
    }
}
