using System.Web;
using System.Web.Optimization;

namespace PERA
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = true;

             bundles.UseCdn = true;   //enable CDN support

            //add link to jquery on the CDN
             var jqueryCdnPath = "http://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js";

            bundles.Add(new ScriptBundle("~/bundles/jquery",
                        jqueryCdnPath).Include(
                "~/Scripts/jquery-{version}.js"));


            bundles.Add(new ScriptBundle("~/bundles/pera-app").Include(
                "~/Scripts/pera-app/pera.js",
                "~/Scripts/pera-app/pera.config.js",
                "~/Scripts/pera-app/dashboard/dashboard.module.js",
                "~/Scripts/pera-app/dashboard/controllers/expense.controller.js",
                "~/Scripts/pera-app/dashboard/controllers/map.controller.js",
                "~/Scripts/pera-app/tabs/tabs.module.js",
                "~/Scripts/pera-app/tabs/controllers/tab.controller.js",
                "~/Scripts/pera-app/pera.config.js",
                "~/Scripts/pera-app/garages/garages.module.js",
                "~/Scripts/pera-app/garages/services/garages.service.js",
                "~/Scripts/pera-app/garages/controllers/garages.controller.js",
                "~/Scripts/pera-app/teammembers/teammembers.module.js",
                "~/Scripts/pera-app/teammembers/services/teammembers.service.js",
                "~/Scripts/pera-app/teammembers/controllers/teammembers.controller.js",
                "~/Scripts/pera-app/utils/utils.module.js",
                "~/Scripts/pera-app/utils/services/snackbar.service.js",
                "~/Scripts/pera-app/tabs/tabs.module.js",
                "~/Scripts/pera-app/tabs/controllers/tab.controller.js",
                "~/Scripts/pera-app/fileupload/fileupload.module.js",
                "~/Scripts/pera-app/fileupload/services/invoiceform.service.js",
                "~/Scripts/pera-app/fileupload/controllers/fileupload.controller.js",
                "~/Scripts/pera-app/fileupload/controllers/invoiceform.controller.js"
                ));
        }
    }
}
