using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AlmondWeb.WebApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                name: "mainpage",
                url: "Anasayfa",
                defaults: new { controller = "Home", action = "MainPage", id = UrlParameter.Optional }
            );
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "addData",
                url: "Veri-Ekle",
                defaults: new { controller = "Home", action = "AddData", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "DeleteData",
                url: "Veri-Sil",
                defaults: new { controller = "Home", action = "DeleteData", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "UpdateData",
                url: "Veri-Guncelle",
                defaults: new { controller = "Home", action = "UpdateData", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "listeislemleri",
                url: "Liste-Islemleri",
                defaults: new { controller = "Home", action = "ListOperations", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "verilerim",
                url: "Verilerim",
                defaults: new { controller = "Home", action = "AllData", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "login",
                url: "Giris-Yap",
                defaults: new { controller = "Home", action = "Login", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "kayitol",
                url: "Kayit-Ol",
                defaults: new { controller = "Home", action = "Register", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "contact",
                url: "Iletisim",
                defaults: new { controller = "Home", action = "Contact", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "RegisterSuccess",
                url: "Kayit-Basarili",
                defaults: new { controller = "Home", action = "RegisterSuccess", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "kullanici",
                url: "Kullanici",//TODO:databaseden username gelecek.
                defaults: new { controller = "User", action = "Index", id = UrlParameter.Optional }
            );
            
            //TODO: user işlemleri bittikten sonra ilgli tanımlamalar yapılacak.

            //routes.MapRoute(
            //    name: "UpdateData",
            //    url: "Veri-Guncelle",
            //    defaults: new { controller = "Home", action = "UpdateData", id = UrlParameter.Optional }
            //);
            routes.MapRoute(
                name: "User",
                url: "User/{action}/{username}",
                defaults: new { controller = "User", action = "Index", username = UrlParameter.Optional }
            );
            //en altta olması tavsiye edilir.
            routes.MapRoute(
            name: "Default",
            url: "{controller}/{action}/{id}",
            defaults: new { controller = "Home", action = "MainPage", id = UrlParameter.Optional }
        );
        }
    }
}
