using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmondWeb.Entities
{
    public class AlmondUserTable : MyEntityBase
    {
        [StringLength(20), Required]
        public string Name { get; set; }
        [StringLength(25), Required]
        public string Surname { get; set; }
        [StringLength(50), Required]
        public string Email { get; set; }
        [StringLength(16), Required]
        public string Password { get; set; }
        [Required]
        public bool isActive { get; set; }
        public bool isAdmin { get; set; }
        [Required]
        public Guid ActivateGuid { get; set; }
        public virtual List<AlmondDataTable> DataTables { get; set; }
        public virtual List<ListTable> ListTables { get; set; }

    }
}
