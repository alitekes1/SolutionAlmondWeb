﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AlmondWeb.BusinessLayer.ViewModels
{
    public class LoginModel
    {

        [DisplayName("E-mail"), Required(ErrorMessage = "{0} alanı zorunludur."), MinLength(2, ErrorMessage = "{0} en az {1} karakterden oluşmalı"), MaxLength(50, ErrorMessage = "{0} en fazla {1} karakterden oluşabilir."), DataType(DataType.EmailAddress, ErrorMessage = "Geçerli bir mail adresi giriniz.")]
        public string email { get; set; }

        [DisplayName("Şifre"), Required(ErrorMessage = "{0} alanı zorunludur."), MinLength(8, ErrorMessage = "{0} en az {1} karakterden oluşmalı"), MaxLength(16, ErrorMessage = "{0} en fazla {1} karakterden oluşabilir."), DataType(DataType.Password)]
        public string password { get; set; }

    }
}