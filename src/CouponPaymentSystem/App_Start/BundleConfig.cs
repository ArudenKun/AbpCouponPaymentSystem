using System.Web.Optimization;
using BundleTransformer.Core.Bundles;

namespace CouponPaymentSystem;

public class BundleConfig
{
    public static bool IsDebug
    {
        get
        {
#if DEBUG
            return true;
#else
            return false;
#endif
        }
    }

    // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
    public static void RegisterBundles(BundleCollection bundles)
    {
        BundleTable.EnableOptimizations = !IsDebug;

        bundles.IgnoreList.Clear();

        bundles.Add(
            new CustomStyleBundle("~/bundles/css").Include(
                "~/Lib/bootstrap/css/bootstrap.css",
                "~/Lib/toastr/toastr.min.css"
            )
        );
        bundles.Add(
            new CustomStyleBundle("~/bundles/css/dt").Include("~/Lib/datatables/datatables.js")
        );
        bundles.Add(
            new CustomScriptBundle("~/bundles/js/base/head").Include("~/Lib/modernizr/modernizr.js")
        );
        bundles.Add(
            new CustomScriptBundle("~/bundles/js/base").Include(
                // JQUERY DEPENDENCIES
                "~/Lib/jquery/jquery.js",
                "~/Lib/jquery/jquery.unobtrusive-ajax.js",
                "~/Lib/jquery/jquery.validate.js",
                "~/Lib/jquery/jquery.validate.unobtrusive.js",
                "~/Lib/jquery/jquery.serializejson.js",
                "~/Lib/jquery/blockUI/jquery.blockUI.js",
                // DEPENDENCIES
                "~/Lib/bootstrap/js/bootstrap.bundle.js",
                "~/Lib/toastr/toastr.min.js",
                "~/Lib/sweetalert2/sweetalert2.all.js",
                "~/Lib/spin/spin.js",
                "~/Lib/spin/jquery.spin.js",
                "~/Lib/signalr/jquery.signalR.js",
                // ABP
                "~/Abp/Framework/scripts/abp.js",
                "~/Abp/Framework/scripts/libs/abp.jquery.js",
                "~/Abp/Framework/scripts/libs/abp.toastr.js",
                "~/Abp/Framework/scripts/libs/abp.blockUI.js",
                "~/Abp/Framework/scripts/libs/abp.spin.js",
                "~/Abp/Framework/scripts/libs/abp.sweet-alert2.js",
                "~/Scripts/main.js",
                "~/Scripts/helpers.js"
            )
        );

        bundles.Add(
            new CustomScriptBundle("~/bundles/js/dt").Include(
                "~/Lib/datatables/datatables.js",
                "~/Lib/Scripts/datatables.ajax.js"
            )
        );
    }
}
