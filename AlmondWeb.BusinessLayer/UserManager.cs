using AlmondWeb.BusinessLayer.ViewModels;
using AlmondWeb.Entities;
using System;

namespace AlmondWeb.BusinessLayer
{
    public class UserManager : BaseManager<AlmondUserTable>
    {
        public ErrorResult<AlmondUserTable> RegisterUser(RegisterModel modal)//gerekli kontroller ve kayıt işlemi gerçekleşecek.
        {
            AlmondUserTable user = FindwithExpression(x => x.Email == modal.email);
            AlmondUserTable checkUsername = FindwithExpression(x => x.Username == modal.username);
            ErrorResult<AlmondUserTable> errorResult = new ErrorResult<AlmondUserTable>();
            if (user != null)
            {
                if (user.Email == modal.email)
                {
                    errorResult.errorList.Add("Girilen Email adresi kayıtlıdır.");
                }
                return errorResult;
            }
            if (checkUsername != null)
            {
                errorResult.errorList.Add("Girilen Kullanıcı Adı kayıtlıdır.");
                return errorResult;
            }
            else//kayıt işlemi gerçekleşecek.
            {
                int isSave = Insert(new AlmondUserTable
                {
                    Username = modal.username,
                    Email = modal.email,
                    Password = modal.password,
                    Name = modal.name,
                    Surname = modal.surname,
                    isActive = true,
                    isAdmin = false,
                    ActivateGuid = Guid.NewGuid()
                });
                if (isSave > 0)
                {
                    errorResult.resultModel = FindwithExpression(x => x.Email == modal.email && x.Password == modal.password);//kayıt edilen kişiyi aldık
                }
                return errorResult;
            }

        }
        public ErrorResult<AlmondUserTable> LoginUser(LoginModel modal)
        {
            AlmondUserTable user = FindwithExpression(x => x.Email == modal.email);
            ErrorResult<AlmondUserTable> errorResult = new ErrorResult<AlmondUserTable>();
            if (user != null)//kayıtlıysa eğer
            {
                if (user.Password == modal.password)
                {
                    //kayıtlı kişiyi contraller a geri gönderiyoruz.
                    if (user.isActive == false)
                    {
                        errorResult.errorList.Add("Hesap aktive edilmemiş.");//TODO:link veya kod ile doğrulamaya göre ifade değiştirilecek
                        return errorResult;
                    }
                    else
                    {
                        errorResult.resultModel = user;
                    }
                }
                else
                {
                    errorResult.errorList.Add("Şifre Hatalı!");
                }
            }
            else
            {
                errorResult.errorList.Add("Kayıtlı bir Email adresi giriniz.");
            }
            return errorResult;
        }

    }
}

