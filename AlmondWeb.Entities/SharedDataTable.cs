using System.ComponentModel.DataAnnotations;

namespace AlmondWeb.Entities
{
    public class SharedDataTable : MyEntityBase
    {
        [Required(ErrorMessage = "Soru alanı boş bırakılamaz!"), StringLength(250, ErrorMessage = "{0} alanı uzunluğu en fazla 250 olan bir dize olmalıdır. ")]
        public string question { get; set; }
        [Required(ErrorMessage = "Cevap alanı boş bırakılamaz!"), StringLength(250, ErrorMessage = "{0} alanı uzunluğu en fazla 250 olan bir dize olmalıdır.")]
        public string answer { get; set; }
        [Required]
        public int puan { get; set; }
        public virtual SharedListTable SharedList { get; set; }
    }
}
