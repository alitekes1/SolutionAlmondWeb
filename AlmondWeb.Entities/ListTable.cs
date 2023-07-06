using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmondWeb.Entities
{
    public class ListTable : MyEntityBase
    {
        [Required, StringLength(60)]
        public string listName { get; set; }
        public virtual List<AlmondDataTable> DataTables { get; set; }//çokları liste halinde tanımlıyoruz
        public virtual AlmondUserTable Owner { get; set; }
    }
}
