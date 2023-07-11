using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;

namespace AlmondWeb.BusinessLayer.ViewModels
{
    public class RegisterModel
    {
        [DisplayName("Ad"), Required, MinLength(2, ErrorMessage = "{0} en az {1} karakterden oluşmalı"), MaxLength(20, ErrorMessage = "{0} en fazla {1} karakterden oluşabilir."), DataType(DataType.Text)]
        public string name { get; set; }

        [DisplayName("Soyad"), Required, MinLength(2, ErrorMessage = "{0} en az {1} karakterden oluşmalı"), MaxLength(25, ErrorMessage = "{0} en fazla {1} karakterden oluşabilir."), DataType(DataType.Text)]
        public string surname { get; set; }

        [DisplayName("E-mail"), Required, MinLength(2, ErrorMessage = "{0} en az {1} karakterden oluşmalı"), MaxLength(50, ErrorMessage = "{0} en fazla {1} karakterden oluşabilir."), DataType(DataType.EmailAddress,ErrorMessage="Geçerli bir mail adresi giriniz.")]
        public string email { get; set; }

        [DisplayName("Şifre"), Required, MinLength(8, ErrorMessage = "{0} en az {1} karakterden oluşmalı"), MaxLength(16, ErrorMessage = "{0} en fazla {1} karakterden oluşabilir."), DataType(DataType.Password)]
        public string password { get; set; }

        [DisplayName("Şifre(Tekrar)"), Required, MinLength(8, ErrorMessage = "{0} en az {1} karakterden oluşmalı"), MaxLength(16, ErrorMessage = "{0} en fazla {1} karakterden oluşabilir."), DataType(DataType.Password), Compare(nameof(password), ErrorMessage = "Girilen şifreler uyuşmuyor!")]
        public string password2 { get; set; }
    }
}