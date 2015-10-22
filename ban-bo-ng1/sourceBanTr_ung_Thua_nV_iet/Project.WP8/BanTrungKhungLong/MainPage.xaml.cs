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
using Microsoft.Phone.Tasks;
using System.Windows.Threading;
using UnityApp = UnityPlayer.UnityApp;
using UnityBridge = WinRTBridge.WinRTBridge;
using System.Windows.Media.Imaging;
using com.vserv.windows.ads.wp8;
using com.vserv.windows.ads;
//using vservWindowsPhone;//Full Ads
using InMobi.WP.AdSDK;
using GoogleAds;

namespace BanTrungKhungLong
{
	public partial class MainPage : PhoneApplicationPage
	{
		private bool _unityStartedLoading;
		private bool _useLocation;
        private static InterstitialAd interstitialAd;
        AdRequest adRequest = new AdRequest(); //admob           
        AdView bannerAd;
        public static int isShowAds = 1;//1 = true
        public static MainPage instance;
        VservAdView adView;
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
            
			showAdmobBanner();
           // VservAdControl VMB = VservAdControl.Instance; //full ads
          //  VMB.DisplayAd("2fdc084a", LayoutRoot);  //n_m_hoang@hotmail.com
          //  VMB.VservAdClosed += new EventHandler(VACCallback_OnVservAdClosing);
          //  VMB.VservAdError += new EventHandler(VACCallback_OnVservAdNetworkError);
          //  VMB.VservAdNoFill += new EventHandler(VACCallback_OnVservAdNoFill);
            instance = this;

           
		}
        void showVservAdsFull()
        {
            /*
            VservAdControl VMB = VservAdControl.Instance; //full ads
            VMB.DisplayAd("2fdc084a", LayoutRoot);  //n_m_hoang@hotmail.com
            VMB.VservAdClosed += new EventHandler(VACCallback_OnVservAdClosing);
            VMB.VservAdError += new EventHandler(VACCallback_OnVservAdNetworkError);
            VMB.VservAdNoFill += new EventHandler(VACCallback_OnVservAdNoFill);
            */
            VservAdView adView = new VservAdView();
            adView.FailedToLoadAd += VACCallback_OnVservAdNoFill;
            // adView.WillDismissOverlay += AdCollapsed;
            //  adView.WillLeaveApp += LeavingApplication;
            // adView.WillPresentOverlay += AdExpanded;
            //  adView.FailedToCacheAd += DidFailed_CacheAd;   
            adView.UX = VservAdUX.Interstitial; // To specify Interstitial ads.
            adView.ZoneId = "2fdc084a"; //to test  "8063";
            adView.TimeOut = 20; // To specify the timeout in case of Ad failure. Default is 20
            // adView.DidLoadAd += AdReceived;
            adView.LoadAd(); // This will load the Ad and on success provides a callBack named DidLoadAd. You need to handle this to perform any action on this event in your application.
            //adView.CacheAd();  // To perform caching of the Ad. If required, this event can be used to run any custom code.
            adView.ShowAd();   // To show the Cached Ad
            //adView.CancelAd();

        }


        void WP8Statics_StopAds(object sender, EventArgs e)
        {
            adGridAdmob.Visibility = Visibility.Collapsed;
            isShowAds = 0;
         
        }

        void WP8Statics_ShowAds(object sender, EventArgs e)
        {
           
            AdmobFullAdsShow();
         
        }
        void WP8Statics_ShowAdsBanner(object sender, EventArgs e)
        {
            showAdmobBanner();
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
		//	AdmobFullAdsShow();

         }
        void AdmobFullAdsShow()
        {
            //  if (interstitialAd == null)
           // {
                interstitialAd = new InterstitialAd("ca-app-pub-7413680112188055/4861557728");//mobilewp8
                interstitialAd.ReceivedAd += OnAdReceivedFull;
                interstitialAd.FailedToReceiveAd += OnFailedToReceiveAdFull;

                //dung dog rem o duoi cung dc
            //    Deployment.Current.Dispatcher.BeginInvoke(() =>
            //    {
            //        interstitialAd = new InterstitialAd("ca-app-pub-7413680112188055/4861557728");//mobilewp8
            //        interstitialAd.ReceivedAd += OnAdReceivedFull;
            //        interstitialAd.FailedToReceiveAd += OnFailedToReceiveAdFull;
            //        interstitialAd.LoadAd(adRequest);
            //    });
           // }
            //adRequest.ForceTesting = true;//here to rem
            interstitialAd.LoadAd(adRequest);
        }
        void showAdmobBanner()
        {
            if (isShowAds == 1)
            {
                bannerAd = new AdView
                {
                    Format = AdFormats.Banner,
                    AdUnitID = "ca-app-pub-7413680112188055/2826421325"
                };

                bannerAd.FailedToReceiveAd += OnFailedToReceiveAd;
                bannerAd.ReceivedAd += OnReceivedAd;
                AdRequest adRequest = new AdRequest();
                //adRequest.ForceTesting = true;//here rem here
                adGridAdmob.Children.Add(bannerAd);
                bannerAd.LoadAd(adRequest);//hinh nh cai nay thua. Can kiem tra lai
                // toanstt_Refresh_admob();
            }
        }
	
        private void OnAdReceivedFull(object sender, AdEventArgs e)//admob Full Ads
        {
            System.Diagnostics.Debug.WriteLine("Ad received successfully");
            interstitialAd.ShowAd();            
        }
        private void OnFailedToReceiveAdFull(object sender, AdErrorEventArgs errorCode)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                showVservAdsFull();
            });  
            System.Diagnostics.Debug.WriteLine("Ad received Fail" + errorCode.ErrorCode.ToString());
        }
		private void DrawingSurfaceBackground_Loaded(object sender, RoutedEventArgs e)
		{
			if (!_unityStartedLoading)
			{
				_unityStartedLoading = true;

				//UnityApp.SetLoadedCallback(() => { Dispatcher.BeginInvoke(Unity_Loaded); });

				var content = Application.Current.Host.Content;
				var nativeWidth = (int)Math.Floor(content.ActualWidth * content.ScaleFactor / 100.0 + 0.5);
				var nativeHeight = (int)Math.Floor(content.ActualHeight * content.ScaleFactor / 100.0 + 0.5);
				
				var physicalWidth = nativeWidth;
				var physicalHeight = nativeHeight;
				object physicalResolution;

				if (DeviceExtendedProperties.TryGetValue("PhysicalScreenResolution", out physicalResolution))
				{
					var resolution = (System.Windows.Size)physicalResolution;
					var nativeScale = content.ActualHeight / content.ActualWidth;
					var physicalScale = resolution.Height / resolution.Width;
					// don't use physical resolution for devices that don't have hardware buttons (e.g. Lumia 630)
					if (Math.Abs(nativeScale - physicalScale) < 0.01)
					{
						physicalWidth = (int)resolution.Width;
						physicalHeight = (int)resolution.Height;
					}
				}

				UnityApp.SetNativeResolution(nativeWidth, nativeHeight);
				UnityApp.SetRenderResolution(physicalWidth, physicalHeight);
				UnityApp.SetOrientation((int)Orientation);

				DrawingSurfaceBackground.SetBackgroundContentProvider(UnityApp.GetBackgroundContentProvider());
				DrawingSurfaceBackground.SetBackgroundManipulationHandler(UnityApp.GetManipulationHandler());
			}
		}
        
        private void OnReceivedAd(object sender, AdEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Ad received successfully");
        }
        private void OnFailedToReceiveAd(object sender, AdErrorEventArgs errorCode)
        {          
            //  adGridAdmob.Visibility = Visibility.Collapsed;
          //  if (isShowAds == 1)
          //      AdsManager.showAds(DrawingSurfaceBackground, AdsManager.INDEX_INMOBI);
          //  else
          //      AdsManager.showAds(DrawingSurfaceBackground, AdsManager.INDEX_INNER_ACTIVE);	
        }

		/*private void Unity_Loaded()
		{
		}*/

		private void PhoneApplicationPage_BackKeyPress(object sender, CancelEventArgs e)
		{
			e.Cancel = UnityApp.BackButtonPressed();
		}

		private void PhoneApplicationPage_OrientationChanged(object sender, OrientationChangedEventArgs e)
		{
			UnityApp.SetOrientation((int)e.Orientation);
		}
	}
}