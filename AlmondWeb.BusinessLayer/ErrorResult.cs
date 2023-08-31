using System.Collections.Generic;

namespace AlmondWeb.BusinessLayer
{
    public class ErrorResult<T> where T : class
    {
        public T resultModel { get; set; }//eğer hata oluşmamışsa gelen modeli geri döndüreceğiz.
        public List<string> errorList { get; set; }//eğer hata oluşmuşsa hataları listeye ekleyeceğiz ve böylelkle hatalara ulaşmış olacağız.
        public ErrorResult()
        {
            errorList = new List<string>();
        }
    }
}
