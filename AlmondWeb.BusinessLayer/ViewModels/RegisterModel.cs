using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AlmondWeb.BusinessLayer.ViewModels
{
    public class RegisterModel
    {
        [DisplayName("Kullanıcı Ad"), Required(ErrorMessage = "{0} alanı zorunludur."), MinLength(4, ErrorMessage = "{0} en az {1} karakterden oluşmalı"), MaxLength(15, ErrorMessage = "{0} en fazla {1} karakterden oluşabilir."), DataType(DataType.Text), RegularExpression("^[a-z0-9]*$", ErrorMessage = "Büyük harfler ve ü-ç-ğ-ş-!-?-, işaretlerine izin verilmez.)")]
        public string username { get; set; }

        [DisplayName("Ad"), Required(ErrorMessage = "{0} alanı zorunludur."), MinLength(2, ErrorMessage = "{0} en az {1} karakterden oluşmalı"), MaxLength(25, ErrorMessage = "{0} en fazla {1} karakterden oluşabilir."), DataType(DataType.Text)]
        public string name { get; set; }

        [DisplayName("Soyad"), Required(ErrorMessage = "{0} alanı zorunludur."), MinLength(2, ErrorMessage = "{0} en az {1} karakterden oluşmalı"), MaxLength(25, ErrorMessage = "{0} en fazla {1} karakterden oluşabilir."), DataType(DataType.Text)]
        public string surname { get; set; }

        [DisplayName("E-mail"), Required(ErrorMessage = "{0} alanı zorunludur."), MinLength(2, ErrorMessage = "{0} en az {1} karakterden oluşmalı"), MaxLength(50, ErrorMessage = "{0} en fazla {1} karakterden oluşabilir."), DataType(DataType.EmailAddress, ErrorMessage = "Geçerli bir mail adresi giriniz.")]
        public string email { get; set; }

        [DisplayName("Şifre"), Required(ErrorMessage = "{0} alanı zorunludur."), MinLength(8, ErrorMessage = "{0} en az {1} karakterden oluşmalı"), MaxLength(16, ErrorMessage = "{0} en fazla {1} karakterden oluşabilir."), DataType(DataType.Password)]
        public string password { get; set; }

        [DisplayName("Şifre(Tekrar)"), Required(ErrorMessage = "{0} alanı zorunludur."), MinLength(8, ErrorMessage = "{0} en az {1} karakterden oluşmalı"), MaxLength(16, ErrorMessage = "{0} en fazla {1} karakterden oluşabilir."), DataType(DataType.Password), Compare(nameof(password), ErrorMessage = "Girilen şifreler uyuşmuyor!")]
        public string password2 { get; set; }
    }
}