﻿using System.Web;
using System.Web.Optimization;

namespace AlmondWeb.WebApp
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootsrap.min.css"
                      ));

            bundles.Add(new StyleBundle("~/Content/profileCss").Include(
                "~/Content/AlmondCss/ProfileLists.css",
                "~/Content/AlmondCss/content.css",
                "~/Content/AlmondCss/Listcheckbox.css",
                "~/Content/AlmondCss/PrivateProfile.css"
                ));

            bundles.Add(new StyleBundle("~/allCss").Include(
                "~/Content/AlmondCss/index.css",
                "~/Content/AlmondCss/navbar.css",
                "~/Content/AlmondCss/footer.css",
                "~/Content/AlmondCss/shortcuts.css",
                "~/Content/AlmondCss/AllButtons.css",
                "~/Content/AlmondCss/ListCheckbox.css"
                ));
            bundles.Add(new ScriptBundle("~/allJs").Include(
                "~/Scripts/jquery-3.7.0.min.js",
                "~/Scripts/toastr.js"
                ));
            BundleTable.EnableOptimizations = true;//yüklerken dosya optimazsyonu için   
        }
    }
}
