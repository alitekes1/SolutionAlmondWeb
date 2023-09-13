using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlmondWeb.Entities
{
    public class AlmondUserTable : MyEntityBase
    {
        [MaxLength(15, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır."), MinLength(4, ErrorMessage = "{0} alanı en az {1} karakter olmalıdır."), Required(ErrorMessage = "{0} alanı zorunludur.")]
        public string Username { get; set; }
        [MaxLength(25, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır."), MinLength(2, ErrorMessage = "{0} alanı en az {1} karakter olmalıdır."), Required(ErrorMessage = "{0} alanı zorunludur.")]
        public string Name { get; set; }
        [MaxLength(25, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır."), MinLength(2, ErrorMessage = "{0} alanı en az {1} karakter olmalıdır."), Required(ErrorMessage = "{0} alanı zorunludur.")]
        public string Surname { get; set; }
        [StringLength(50), Required(ErrorMessage = "{0} alanı zorunludur.")]
        public string Email { get; set; }
        [MaxLength(16, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır."), MinLength(8, ErrorMessage = "{0} alanı en az {1} karakter olmalıdır."), Required(ErrorMessage = "{0} alanı zorunludur.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        public bool isActive { get; set; }
        public bool isAdmin { get; set; }
        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        public Guid ActivateGuid { get; set; }
        public virtual List<AlmondDataTable> DataTables { get; set; }//bir user ın birden çok datası olacağı için list e şeklinde tanımlıyoruz ve bu liste virtual olmak zorunda.
        public virtual List<ListTable> ListTables { get; set; }//bir user ın birden fazla listesi olacağı için list şeklinde tanımlıyoruz ve bu liste virtual olmak zorunda.
        public virtual List<SharedListTable> ProfileListTables { get; set; }
    }
}
