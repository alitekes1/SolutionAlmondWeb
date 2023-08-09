using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AlmondWeb.Entities
{
    public class AlmondUserTable : MyEntityBase
    {
        [MaxLength(15), MinLength(8), Required]
        public string Username { get; set; }
        [StringLength(25), Required]
        public string Name { get; set; }
        [StringLength(25), Required]
        public string Surname { get; set; }
        [StringLength(50), Required]
        public string Email { get; set; }
        [MaxLength(16), MinLength(8), Required]
        public string Password { get; set; }
        [Required]
        public bool isActive { get; set; }
        public bool isAdmin { get; set; }
        [Required]
        public Guid ActivateGuid { get; set; }
        public virtual List<AlmondDataTable> DataTables { get; set; }//bir user ın birden çok datası olacağı için list e şeklinde tanımlıyoruz ve bu liste virtual olmak zorunda.
        public virtual List<ListTable> ListTables { get; set; }//bir user ın birden fazla listesi olacağı için list şeklinde tanımlıyoruz ve bu liste virtual olmak zorunda.
        public virtual List<SharedListTable> ProfileListTables { get; set; }
    }
}
