using AlmondWeb.BusinessLayer.RepositoryPattern;
using AlmondWeb.BusinessLayer.ViewModels;
using AlmondWeb.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.WebPages.Html;

namespace AlmondWeb.BusinessLayer
{
    public class UserManager
    {
        private Repository<AlmondUserTable> repo_user = new Repository<AlmondUserTable>();
        public ErrorResult<AlmondUserTable> RegisterUser(RegisterModel modal)//gerekli kontroller ve kayıt işlemi gerçekleşecek.
        {
            Repository<AlmondUserTable> repo_data = new Repository<AlmondUserTable>();
            AlmondUserTable user = repo_data.Find(x => x.Email == modal.email && x.Password == modal.password);
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
                int isSave = repo_user.Insert(new AlmondUserTable
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
                    errorResult.resultModel = repo_data.Find(x => x.Email == modal.email && x.Password == modal.password);//kayıt edilen kişiyi aldık
                }
                return errorResult;
            }

        }
        public ErrorResult<AlmondUserTable> LoginUser(LoginModal modal)
        {
            Repository<AlmondUserTable> repo_data = new Repository<AlmondUserTable>();
            AlmondUserTable user = repo_data.Find(x => x.Email == modal.email);
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

