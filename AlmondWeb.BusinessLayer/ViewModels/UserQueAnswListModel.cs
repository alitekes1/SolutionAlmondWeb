using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AlmondWeb.BusinessLayer.ViewModels
{
    public class UserQueAnswListModel
    {
        [DisplayName("Soru"), Required, MaxLength(250, ErrorMessage = "{0} en fazla {1} karakterden oluşabilir."), DataType(DataType.Text)]
        public string question { get; set; }

        [DisplayName("Cevap"), Required, MaxLength(250, ErrorMessage = "{0} en fazla {1} karakterden oluşabilir.")]
        public string answer { get; set; }

        [DisplayName("Liste"), Required]
        public int list_Id { get; set; }

        [DisplayName("Güncellenekek veri")]
        public int? update_Id { get; set; }
        public int? delete_Id { get; set; }
        public int puan { get; set; }
    }
}
