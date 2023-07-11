using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace AlmondWeb.BusinessLayer.ViewModels
{
    public class LoginModal
    {

        [DisplayName("E-mail"), Required, MinLength(2, ErrorMessage = "{0} en az {1} karakterden oluşmalı"), MaxLength(50, ErrorMessage = "{0} en fazla {1} karakterden oluşabilir."), DataType(DataType.EmailAddress, ErrorMessage = "Geçerli bir mail adresi giriniz.")]
        public string email { get; set; }

        [DisplayName("Şifre"), Required, MinLength(8, ErrorMessage = "{0} en az {1} karakterden oluşmalı"), MaxLength(16, ErrorMessage = "{0} en fazla {1} karakterden oluşabilir."), DataType(DataType.Password)]
        public string password { get; set; }

    }
}