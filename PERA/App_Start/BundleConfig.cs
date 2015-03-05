using System.Web;
using System.Web.Optimization;

namespace PERA
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            const string ANGULAR_APP_ROOT = "~/Scripts/pera-app/";
            BundleTable.EnableOptimizations = true;


             bundles.UseCdn = true;   //enable CDN support

            //add link to jquery on the CDN
             var jqueryCdnPath = "http://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js";

             var jqueryBundle = new ScriptBundle("~/bundles/jquery", jqueryCdnPath);
            //jqueryBundle.Include("~/Scripts/jquery-{version}.js");
            bundles.Add(jqueryBundle);

  

            var peraBundle = new ScriptBundle("~/bundles/pera-app");
            peraBundle
               .Include("~/Scripts/pera-app/pera.js")
               .Include("~/Scripts/pera-app/dashboard/dashboard.module.js")
               .Include("~/Scripts/pera-app/fileupload/fileupload.module.js")
                .Include("~/Scripts/pera-app/garages/garages.module.js")
                .Include("~/Scripts/pera-app/teammembers/teammembers.module.js")
                .Include("~/Scripts/pera-app/utils/utils.module.js")
                .Include("~/Scripts/pera-app/tabs/tab.module.js")
                .Include("~/Scripts/pera-app/fileupload/fileUpload.module.js")
                .Include("~/Scripts/pera-app/invoiceTeamMembers/invoiceTeamMembers.module.js")
                .IncludeDirectory(ANGULAR_APP_ROOT, "*.js", searchSubdirectories: true);
                /*
                .Include("~/Scripts/pera-app/pera.config.js")
                .IncludeDirectory("~/Scripts/pera-app/dashboard/", "*.js", true)
                .IncludeDirectory("~/Scripts/pera-app/fileupload/", "*.js", true)
                .IncludeDirectory("~/Scripts/pera-app/garages/", "*.js", true)
                .IncludeDirectory("~/Scripts/pera-app/teammembers/", "*.js", true)
                .IncludeDirectory("~/Scripts/pera-app/utils/", "*.js", true)
                .IncludeDirectory("~/Scripts/pera-app/tabs/", "*.js", true)
                .IncludeDirectory("~/Scripts/pera-app/fileupload/", "*.js", true)
                .IncludeDirectory("~/Scripts/pera-app/invoiceTeamMembers/", "*.js", true);
                 */
            bundles.Add(peraBundle);
                /*
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
                 
                 */
          
        }
    }
}
