using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlmondWeb.Entities
{
    public class ContactTable
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(maximumLength: 50, MinimumLength = 3), DisplayName("Mail Adresi")]
        public string contactMail { get; set; }

        [StringLength(maximumLength: 50, MinimumLength = 3), DisplayName("Konu")]
        public string contactType { get; set; }

        [StringLength(maximumLength: 1000, MinimumLength = 10), DisplayName("Mesaj")]
        public string message { get; set; }

    }
}
