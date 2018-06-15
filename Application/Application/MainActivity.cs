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
using System.Net;
using System.Text;
using System.IO;
using System.Collections.Generic;
using Android.Text;

namespace Application
{
	[Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
	public class MainActivity : AppCompatActivity
	{
        public static IPAddress IpAddress { get; set; }
        public static uint Port { get; set; }

        private List<string> nodes;
        private ListView listView;
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.activity_main);

			Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

			FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            var btnConnect = FindViewById<Button>(Resource.Id.button1);
            btnConnect.Click += ConnectOnClick;
            listView = FindViewById<ListView>(Resource.Id.listView1);
            
            nodes = new List<string>();
            FindViewById<ListView>(Resource.Id.listView1).ItemClick += MainActivity_ItemClick;
            //WebView wv = (WebView)FindViewById(Resource.Id.webView1);
            //wv.SetWebViewClient(new WebViewClient());

            //wv.Settings.JavaScriptEnabled = true;
            ////wv.LoadUrl("https://www.google.com:443/");


            //wv.LoadUrl("http://192.168.1.41:82/");

            //var dict = new List<string>() { "j_username=Master" };
            //var dict2 = new List<string>() { "j_username=Master", "j_password=Master12345" };

            //var resp = Login("http://192.168.1.41:82/prelogin", dict);
            //resp = Login("http://192.168.1.41:82/login", dict2);
        }

        private void ConnectOnClick(object sender, EventArgs e)
        {
            if (IPAddress.TryParse(FindViewById<TextInputEditText>(Resource.Id.textInputEditText1).Text, out IPAddress iPAddress) &&
                uint.TryParse(FindViewById<TextInputEditText>(Resource.Id.textInputEditText2).Text, out uint port))
            {
                IpAddress = iPAddress;
                Port = port;
                var entry = $"{IpAddress}:{Port}";
                if (!nodes.Contains(entry))
                    nodes.Add(entry);

                
                ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, nodes);
                listView.Adapter = adapter;

                Intent act = new Intent(this, typeof(webpageactivity));
                StartActivity(act);
            }
        }

        private void MainActivity_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var item  = listView.GetItemAtPosition(e.Position);

            var split = item.ToString().Split(':');
            IpAddress = IPAddress.Parse(split[0]);
            Port = uint.Parse(split[1]);

            Intent act = new Intent(this, typeof(webpageactivity));
            StartActivity(act);
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
            //WebView wv = (WebView)FindViewById(Resource.Id.webView1);
            //wv.SetWebViewClient(new WebViewClient());
            
            //wv.Settings.JavaScriptEnabled = true;
            ////wv.LoadUrl("https://www.google.com:443/");
            //wv.LoadUrl("http://192.168.1.41:82/");
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

            ////v.LoadUrl("https://www.google.com/");
            //WebView wv = (WebView)FindViewById(Resource.Id.webView1);
            //wv.LoadUrl("https://www.google.com");
        }

        public static CookieContainer CookieContainer { get; private set; } = new CookieContainer();

        public static string Login(string loginPageAddress, List<string> loginData)
        {
            var encoding = new ASCIIEncoding();

            var query = string.Join("&", loginData);
           
            
            byte[] data = encoding.GetBytes(query);
            WebRequest request = WebRequest.Create(loginPageAddress);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;
            Stream stream = request.GetRequestStream();
            stream.Write(data, 0, data.Length);
            stream.Close();
            WebResponse response = request.GetResponse();
            String result;
            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                result = sr.ReadToEnd();
                sr.Close();
            }
            return result;

            //var request = (HttpWebRequest)WebRequest.Create(loginPageAddress);

            //request.Method = "POST";
            //request.ContentType = "application/x-www-form-urlencoded";

            //var query = string.Join("&", loginData);

            //var buffer = Encoding.ASCII.GetBytes(query);
            //request.ContentLength = buffer.Length;
            //var requestStream = request.GetRequestStream();
            //requestStream.Write(buffer, 0, buffer.Length);
            //requestStream.Flush();
            //requestStream.Close();

            //request.CookieContainer = CookieContainer;

            //var response = request.GetResponse().GetResponseStream();
            //StreamReader objReader = new StreamReader(response);
            //string responseFromServer = objReader.ReadToEnd();
            //response.Close();

        }

    }
}

