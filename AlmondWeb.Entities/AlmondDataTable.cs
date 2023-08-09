using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmondWeb.Entities
{
    public class AlmondDataTable : MyEntityBase
    {
        [Required(ErrorMessage = "Soru alanı boş bırakılamaz!"), StringLength(250, ErrorMessage = "{0} alanı uzunluğu en fazla 250 olan bir dize olmalıdır. ")]
        public string question { get; set; }
        [Required(ErrorMessage = "Cevap alanı boş bırakılamaz!"), StringLength(250, ErrorMessage = "{0} alanı uzunluğu en fazla 250 olan bir dize olmalıdır.")]
        public string answer { get; set; }
        [Required]
        public int puan { get; set; }
        public virtual AlmondUserTable Owner { get; set; }//bir tarafaında olduğu için 
        public virtual ListTable List { get; set; }
    }
}
