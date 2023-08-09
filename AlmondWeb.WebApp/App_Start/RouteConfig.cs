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
                name: "listeyonetimi",
                url: "Liste-Yonetimi",
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
                name: "hesabim",
                url: "Hesabim",//TODO:databaseden username gelecek.
                defaults: new { controller = "User", action = "PrivateProfile", id = UrlParameter.Optional }
            );
            //TODO: user işlemleri bittikten sonra ilgli tanımlamalar yapılacak.
            routes.MapRoute(
                name: "updateprofile2",
                url: "User/ProfileUpdate/{id}",
                defaults: new { controller = "User", action = "ProfileUpdate", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "profileexplore",
                url: "Liste-Kesfet",
                defaults: new { controller = "User", action = "PublicAllList", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "publicprofile",
                url: "Profilim",
                defaults: new { controller = "User", action = "PublicProfile", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "privateProfile",
                url: "Hesabim",
                defaults: new { controller = "User", action = "PrivateProfile", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "adminpage",
                url: "Admin54",
                defaults: new { controller = "Admin", action = "AllData", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "alldata",
                url: "TumListeler54",
                defaults: new { controller = "Admin", action = "AllList", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "allmesssages",
                url: "TumMesajlar54",
                defaults: new { controller = "Admin", action = "AllMessages", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "statictis",
                url: "Istatistikler54",
                defaults: new { controller = "Admin", action = "AlmondWebStatistic", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "alluser",
                url: "TumKullanicilar54",
                defaults: new { controller = "Admin", action = "AllUser", id = UrlParameter.Optional }
            );
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
