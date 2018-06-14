using System;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Content;
using Java.Interop;
using Android.Webkit;

namespace Application
{
	[Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
	public class MainActivity : AppCompatActivity
	{

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.activity_main);

			Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

			FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

		}

		public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            WebView wv = (WebView)FindViewById(Resource.Id.webView1);
            wv.SetWebViewClient(new WebViewClient());
            //wv.LoadUrl("https://www.google.com:443/");
            wv.LoadUrl("http://192.168.1.41:82/");
            //View view = (View) sender;
            //Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
            //    .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }

        [Export("OnClick")]
        public void OnClick(WebView v)
        {
            //var uri = Android.Net.Uri.Parse("https://google.com/");
            //var intent = new Intent(Intent.ActionView, uri);
            //StartActivity(intent);

            //v.LoadUrl("https://www.google.com/");
            WebView wv = (WebView)FindViewById(Resource.Id.webView1);
            wv.LoadUrl("https://www.google.com");
        }

    }
}

