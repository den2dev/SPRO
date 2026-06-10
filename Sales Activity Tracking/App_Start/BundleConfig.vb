Imports System.Web.Optimization

Public Module BundleConfig
    ' For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
    Public Sub RegisterBundles(ByVal bundles As BundleCollection)

        'bundles.Add(New ScriptBundle("~/bundles/jquery").Include(
        '            "~/Scripts/jquery-{version}.js"))

        'bundles.Add(New ScriptBundle("~/bundles/jqueryval").Include(
        '            "~/Scripts/jquery.validate*"))

        'bundles.Add(New ScriptBundle("~/bundles/modernizr").Include(
        '            "~/Scripts/modernizr-*"))

        'bundles.Add(New Bundle("~/bundles/bootstrap").Include(
        '          "~/Scripts/bootstrap.js"))


        'bundles.Add(New StyleBundle("~/Content/css").Include(
        '          "~/Content/bootstrap.css",
        '          "~/Content/site.css")


        bundles.Add(New ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-2.2.3.js"))

        bundles.Add(New ScriptBundle("~/bundles/jquerymobile").Include("~/Scripts/jquery.mobile-1.4.5.min.js"))

        bundles.Add(New ScriptBundle("~/bundles/jqueryval").Include("~/Scripts/jquery.validate*"))

        bundles.Add(New ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-*"))

        bundles.Add(New Bundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap.js"))

        bundles.Add(New StyleBundle("~/Content/jquerymobile").Include("~/Content/jquery.mobile-1.4.5.min.css"))

        bundles.Add(New StyleBundle("~/Content/css").Include(
            "~/Content/bootstrap.css",
            "~/Content/site.css",
            "~/Content/PDAStyle.css",
            "~/Content/Common.css"))

    End Sub
End Module

