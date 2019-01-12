using System;
using System.Timers;
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace KeepTimerRunning
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private System.Timers.Timer _timer;
        private double _interval;
        private TextView _textView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            _textView = FindViewById<TextView>(Resource.Id.textView);

            // Use the project generated floating button for starting the timer
            var fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            // Setup the timer
            _interval = TimeSpan.FromSeconds(1).TotalMilliseconds;
            _timer = new Timer { Interval = _interval };
            _timer.Elapsed += TimerOnElapsed;
            _timer.Start();
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            RunOnUiThread(() =>
            {
                _textView.Text = DateTime.Now.ToLongTimeString();
            });

            Log.Debug("Timer", DateTime.Now.ToLongTimeString());
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            var id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            MakeSureTimerIsRunning();
        }

        /// <summary>
        ///     Make sure that the timer is running.
        /// </summary>
        public void MakeSureTimerIsRunning()
        {
            if (_timer == null)
            {
                return;
            }

            // Setting the interval triggers the timer to start again.
            _timer.Interval = _interval;
        }
    }
}