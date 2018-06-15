using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;

namespace Application
{
    [Activity(Label = "webpageactivity")]
    public class webpageactivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.webpage);
            // Create your application here

            WebView wv = (WebView)FindViewById(Resource.Id.webView1);
            wv.SetWebViewClient(new WebViewClient());

            wv.Settings.JavaScriptEnabled = true;
            wv.LoadUrl($"http://{MainActivity.IpAddress}:{MainActivity.Port}/"); //http://192.168.1.41:82/");
        }
    }
}