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
        [StringLength(60)]
        public string listDescription { get; set; }
        [Required]
        public bool isPublic { get; set; }
        public virtual AlmondUserTable Owner { get; set; }//her listenin bir tane user ı olacağı için sanal bir property olarak tanımlıyoruz.
        public virtual List<AlmondDataTable> DataTables { get; set; }//çokları liste halinde tanımlıyoruz
        public virtual List<ProfileListTable> List { get; set; }//many to many ilişki için
    }
}
