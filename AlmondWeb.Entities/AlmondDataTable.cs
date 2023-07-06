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
        [Required, StringLength(250)]
        public string question { get; set; }
        [Required, StringLength(250)]
        public string answer { get; set; }
        [Required]
        public int puan { get; set; }
        public virtual AlmondUserTable Owner { get; set; }//bir tarafaında olduğu için 
        public virtual ListTable List { get; set; }

    }
}
