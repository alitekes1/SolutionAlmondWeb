using AlmondWeb.DataAccessLayer.RepositoryPattern;
using AlmondWeb.BusinessLayer.ViewModels;
using AlmondWeb.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.WebPages.Html;
using System.Security.Cryptography.X509Certificates;

namespace AlmondWeb.BusinessLayer
{
    public class UserManager : BaseManager<AlmondUserTable>
    {

        public ErrorResult<AlmondUserTable> RegisterUser(RegisterModel modal)//gerekli kontroller ve kayıt işlemi gerçekleşecek.
        {
            AlmondUserTable user = FindwithExpression(x => x.Email == modal.email && x.Password == modal.password);

            ErrorResult<AlmondUserTable> errorResult = new ErrorResult<AlmondUserTable>();
            if (user != null)//kayıt işlemi gerçekleşecek.
            {
                if (user.Email == modal.email)
                {
                    errorResult.errorList.Add("Girilen Email adresi kayıtlıdır.");
                }
                return errorResult;
            }
            else
            {
                int isSave = Insert(new AlmondUserTable
                {
                    Email = modal.email,
                    Password = modal.password,
                    Name = modal.name,
                    Surname = modal.surname,
                    isActive = true,
                    isAdmin = false,
                    ActivateGuid = Guid.NewGuid(),
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

