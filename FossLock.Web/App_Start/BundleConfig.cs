using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace FossLock.Web.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            var stylesBundle = new Bundle("~/Static/Styles");
            stylesBundle.Include("~/Static/css/site.css");
            bundles.Add(stylesBundle);

            // right now, i just want to use the default js bundler/minifier
            var scriptsBundle = new ScriptBundle("~/Static/Scripts");
            scriptsBundle.Include(
                "~/Static/lib/jquery-1.10.2.js",
                "~/Static/lib/bootstrap3/js/bootstrap.js");
            bundles.Add(scriptsBundle);
        }
    }
}