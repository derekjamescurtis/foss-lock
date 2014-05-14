using BundleTransformer.Core.Builders;
using BundleTransformer.Core.Orderers;
using BundleTransformer.Core.Transformers;
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

            // used by bundle-transformer package to override the default
            // funtionaliy of system.web.optimization bundling.
            var nullBuilder = new NullBuilder();
            var cssTransformer = new CssTransformer();
            var jsTransformer = new JsTransformer();
            var nullOrderer = new NullOrderer();

            // setup our style bundles.. this will handle our 
            // less compilation automatically
            var stylesBundle = new Bundle("~/Static/Styles") { 
                Builder = nullBuilder,
                Orderer = nullOrderer
            };
            stylesBundle.Transforms.Add(cssTransformer);
            stylesBundle.Include("~/Static/less/site.less");
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