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

            bundles.Add(new ScriptBundle("~/bundles/PERAapp")
                .IncludeDirectory("~/Scripts/Angular", "*.js", true)
                .IncludeDirectory("~/Scripts/Angular", "*.js")
                .Include("~/Scripts/Angular/pera.js"));
        }
    }
}
