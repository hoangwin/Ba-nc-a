using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Info;
using Windows.Foundation;
using Windows.Devices.Geolocation;
using System.Windows.Threading;
using UnityApp = UnityPlayer.UnityApp;
using UnityBridge = WinRTBridge.WinRTBridge;
using vservWindowsPhone;//Full Ads
using InMobi.WP.AdSDK;
using GoogleAds;

namespace BanTrungKhungLong
{
	public partial class MainPage : PhoneApplicationPage
	{
		private bool _unityStartedLoading;
		private bool _useLocation;
        private static InterstitialAd interstitialAd;
        AdView bannerAd;
        public static int isShowAds = 1;//1 = true
        public static MainPage instance;
		// Constructor
		public MainPage()
		{
			var bridge = new UnityBridge();
			UnityApp.SetBridge(bridge);
			InitializeComponent();
			bridge.Control = DrawingSurfaceBackground;
            WP8Statics.WP8FunctionHandleStopAds += WP8Statics_StopAds;
            WP8Statics.WP8FunctionHandleShowAds += WP8Statics_ShowAds;
            WP8Statics.WP8FunctionHandleShowAdsBanner += WP8Statics_ShowAdsBanner;
            
            VservAdControl VMB = VservAdControl.Instance; //full ads
            VMB.DisplayAd("2fdc084a", LayoutRoot);  //n_m_hoang@hotmail.com
            VMB.VservAdClosed += new EventHandler(VACCallback_OnVservAdClosing);
            VMB.VservAdError += new EventHandler(VACCallback_OnVservAdNetworkError);
            VMB.VservAdNoFill += new EventHandler(VACCallback_OnVservAdNoFill);
            instance = this;

           
		}

        void WP8Statics_StopAds(object sender, EventArgs e)
        {
            adGridAdmob.Visibility = Visibility.Collapsed;
            isShowAds = 0;
         
        }

        void WP8Statics_ShowAds(object sender, EventArgs e)
        {
            Dispatcher.BeginInvoke(AdmobFullAdsShow);
         
        }
        void WP8Statics_ShowAdsBanner(object sender, EventArgs e)
        {
            Dispatcher.BeginInvoke(showBannerAdmob);          
        }
        void showBannerAdmob()
        {

            bannerAd = new AdView
            {
                Format = AdFormats.Banner,
                AdUnitID = "ca-app-pub-7413680112188055/2826421325"//hoangcaogia
            };
            bannerAd.FailedToReceiveAd += OnFailedToReceiveAd;
            bannerAd.ReceivedAd += OnAdReceivedBanner;

            adGridAdmob.Children.Add(bannerAd);
            AdRequest adRequest = new AdRequest();
            //adRequest.ForceTesting = true;
            bannerAd.LoadAd(adRequest);
			
        }
        private void OnAdReceivedBanner(object sender, AdEventArgs e)//admob Full Ads
        {
            System.Diagnostics.Debug.WriteLine("Ad received successfully");
            //interstitialAd.ShowAd();
        }
      void VACCallback_OnVservAdClosing(object sender, EventArgs e)
        {
         
            
        }
        void VACCallback_OnVservAdNetworkError(object sender, EventArgs e)
        {
             }

        void VACCallback_OnVservAdNoFill(object sender, EventArgs e)
        {
			//AdmobFullAdsShow();

         }
       	
		void AdmobFullAdsShow()
        {
            interstitialAd = new InterstitialAd("ca-app-pub-7413680112188055/4861557728");//full ads hogncaogia
            AdRequest adRequest = new AdRequest();
            interstitialAd.ReceivedAd += OnAdReceived;
            interstitialAd.FailedToReceiveAd += OnAdFailed;
            interstitialAd.LoadAd(adRequest);
        }
        private void OnAdFailed(object sender, AdErrorEventArgs e)//admob Full Ads
        {

        }
        private void OnAdReceived(object sender, AdEventArgs e)//admob Full Ads
        {
            System.Diagnostics.Debug.WriteLine("Ad received successfully");
            interstitialAd.ShowAd();
        }
		
		private void DrawingSurfaceBackground_Loaded(object sender, RoutedEventArgs e)
		{
			if (!_unityStartedLoading)
			{
				_unityStartedLoading = true;

				UnityApp.SetLoadedCallback(() => { Dispatcher.BeginInvoke(Unity_Loaded); });
				
				int physicalWidth, physicalHeight;
				object physicalResolution;

				var content = Application.Current.Host.Content;
				var nativeWidth = (int)Math.Floor(content.ActualWidth * content.ScaleFactor / 100.0 + 0.5);
				var nativeHeight = (int)Math.Floor(content.ActualHeight * content.ScaleFactor / 100.0 + 0.5);

				if (DeviceExtendedProperties.TryGetValue("PhysicalScreenResolution", out physicalResolution))
				{
					var resolution = (System.Windows.Size) physicalResolution;
					physicalWidth = (int)resolution.Width;
					physicalHeight = (int)resolution.Height;
				}
				else
				{
					physicalWidth = nativeWidth;
					physicalHeight = nativeHeight;
				}

				UnityApp.SetNativeResolution(nativeWidth, nativeHeight);
				UnityApp.SetRenderResolution(physicalWidth, physicalHeight);
				UnityPlayer.UnityApp.SetOrientation((int)Orientation);

				DrawingSurfaceBackground.SetBackgroundContentProvider(UnityApp.GetBackgroundContentProvider());
				DrawingSurfaceBackground.SetBackgroundManipulationHandler(UnityApp.GetManipulationHandler());

               

            }
		}
        private void OnFailedToReceiveAd(object sender, AdErrorEventArgs errorCode)
        {          
            //  adGridAdmob.Visibility = Visibility.Collapsed;
          //  if (isShowAds == 1)
          //      AdsManager.showAds(DrawingSurfaceBackground, AdsManager.INDEX_INMOBI);
          //  else
          //      AdsManager.showAds(DrawingSurfaceBackground, AdsManager.INDEX_INNER_ACTIVE);	
        }

		private void Unity_Loaded()
		{
			SetupGeolocator();
		}

		private void PhoneApplicationPage_BackKeyPress(object sender, CancelEventArgs e)
		{
			e.Cancel = UnityApp.BackButtonPressed();
		}

		private void PhoneApplicationPage_OrientationChanged(object sender, OrientationChangedEventArgs e)
		{
			UnityApp.SetOrientation((int)e.Orientation);
		}

		protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (!UnityApp.IsLocationEnabled())
                return;
            if (IsolatedStorageSettings.ApplicationSettings.Contains("LocationConsent"))
                _useLocation = (bool)IsolatedStorageSettings.ApplicationSettings["LocationConsent"];
            else
            {
                MessageBoxResult result = MessageBox.Show("Can this application use your location?",
                    "Location Services", MessageBoxButton.OKCancel);
                _useLocation = result == MessageBoxResult.OK;
                IsolatedStorageSettings.ApplicationSettings["LocationConsent"] = _useLocation;
                IsolatedStorageSettings.ApplicationSettings.Save();
            }
        }

		private void SetupGeolocator()
        {
            if (!_useLocation)
                return;

            try
            {
				UnityApp.EnableLocationService(true);
                Geolocator geolocator = new Geolocator();
				geolocator.ReportInterval = 5000;
                IAsyncOperation<Geoposition> op = geolocator.GetGeopositionAsync();
                op.Completed += (asyncInfo, asyncStatus) =>
                    {
                        if (asyncStatus == AsyncStatus.Completed)
                        {
                            Geoposition geoposition = asyncInfo.GetResults();
                            UnityApp.SetupGeolocator(geolocator, geoposition);
                        }
                        else
                            UnityApp.SetupGeolocator(null, null);
                    };
            }
            catch (Exception)
            {
                UnityApp.SetupGeolocator(null, null);
            }
        }
	}
}