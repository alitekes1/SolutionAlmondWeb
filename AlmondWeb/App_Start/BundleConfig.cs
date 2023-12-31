﻿using System.Web.Optimization;

namespace AlmondWeb
{
    public class BundleConfig
    {
        // Paketleme hakkında daha fazla bilgi için lütfen https://go.microsoft.com/fwlink/?LinkId=301862 adresini ziyaret edin
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/AllJS").Include(
                        "~/Scripts/jquery-3.7.1.min.js",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js",
                        "~/Scripts/bootstrap.min.js",
                        "~/Scripts/toastr.min.js",
                        "~/Scripts/AlmondJs/RefreshTable_UserInfo.js"));

            bundles.Add(new StyleBundle("~/Content/profileCss").Include(
                        "~/Content/AlmondCss/ProfileLists.css",
                        "~/Content/AlmondCss/content.css",
                        "~/Content/AlmondCss/Listcheckbox.css",
                        "~/Content/AlmondCss/PrivateProfile.css"
                ));

            bundles.Add(new StyleBundle("~/LayoutCss").Include(
                        "~/Content/AlmondCss/navbar.css",
                        "~/Content/AlmondCss/footer.css",
                        "~/Content/AlmondCss/shortcuts.css",
                        "~/Content/AlmondCss/index.css"
                ));

            BundleTable.EnableOptimizations = true;
        }
    }
}
