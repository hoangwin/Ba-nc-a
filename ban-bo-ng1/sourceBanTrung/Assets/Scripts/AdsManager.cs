using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class AdsManager : MonoBehaviour
{
    public static bool isLoadFullAds = false;
    static public bool firstShowAdsFull = false;
    static public bool firstShowAdsBanner = false;
    static public float timeShowAds = 0;
    static public bool isInit = false;
    //--------------------------------------
    //  EVENTS
    //--------------------------------------



    private void _OnInterstitialAdLoaded()
    {
     //   dispatch(GoogleMobileAdEvents.ON_INTERSTITIAL_AD_LOADED);
    }

    private void _OnInterstitialAdFailedToLoad()
    {
      //  dispatch(GoogleMobileAdEvents.ON_INTERSTITIAL_AD_FAILED_LOADING);
    }

    private void _OnInterstitialAdOpened()
    {
      //  dispatch(GoogleMobileAdEvents.ON_INTERSTITIAL_AD_OPENED);
    }

    private void _OnInterstitialAdClosed()
    {
      //  dispatch(GoogleMobileAdEvents.ON_INTERSTITIAL_AD_CLOSED);
    }

    private void _OnInterstitialAdLeftApplication()
    {
     //   dispatch(GoogleMobileAdEvents.ON_INTERSTITIAL_AD_LEFT_APPLICATION);
    }

   

    public static void OnInterstisialsClose()
    {
        Debug.Log("Load new Ads");
        AdsManager.isLoadFullAds = false;
      //  GoogleMobileAd.LoadInterstitialAd();
        
    }
    public static void OnInterstisialsLoadFail()
    {
        Debug.Log("Load Ads Fail");
        isLoadFullAds = false;
    }
    public static void OnInterstisialsLoad()
    {
        Debug.Log("Load Ads OK");
        isLoadFullAds = true;
    }
    public static void initAdsEvent()
    {
#if UNITY_ANDROID
        if (!isInit)
        {
            isInit = true;

            //GoogleMobileAd.Init();
            //GoogleMobileAd.controller.addEventListener(GoogleMobileAdEvents.ON_INTERSTITIAL_AD_LOADED, OnInterstisialsLoad);
            //GoogleMobileAd.controller.addEventListener(GoogleMobileAdEvents.ON_INTERSTITIAL_AD_FAILED_LOADING, OnInterstisialsLoadFail);
            //GoogleMobileAd.controller.addEventListener(GoogleMobileAdEvents.ON_INTERSTITIAL_AD_CLOSED, OnInterstisialsClose);

            //loadin ad:
/*
            GoogleMobileAd.Init();
            GoogleMobileAd.addEventListener(GoogleMobileAdEvents.ON_INTERSTITIAL_AD_CLOSED, OnInterstisialsClose);
            GoogleMobileAd.addEventListener(GoogleMobileAdEvents.ON_INTERSTITIAL_AD_FAILED_LOADING, OnInterstisialsLoadFail);
            GoogleMobileAd.addEventListener(GoogleMobileAdEvents.ON_INTERSTITIAL_AD_LOADED, OnInterstisialsLoad);
 */
        }
#endif

    }
    public static void ShowADS_FULL()
    {

        
       
#if UNITY_ANDROID
        if (timeShowAds > 120 || !firstShowAdsFull)
        {
            if (!AdsManager.isLoadFullAds)
            {
                Debug.Log("Loading Ads");
            //    GoogleMobileAd.LoadInterstitialAd();
            }
            else
            {
                Debug.Log("Call Show Full Ads");
                firstShowAdsFull = true;
                timeShowAds = 0;
            //    GoogleMobileAd.ShowInterstitialAd();
               // UM_AdManager.instance.StartInterstitialAd();
                //if (UM_AdManager.instance.IsInited == false)
               // {
                //    UM_AdManager.instance.Init();
                //    UM_AdManager.instance.StartInterstitialAd();
               // }
               // else
              //  {
              //      GoogleMobileAd.ShowInterstitialAd();
              //  }

            }
        }



          //  using (AndroidJavaClass jc = new AndroidJavaClass("org.xiaxio.bubbleshoot.UnityPlayerNativeActivity"))
          //  {
          //      jc.CallStatic<int>("ShowAdsFull");
          //  }
            
#elif UNITY_WP8
        if (timeShowAds > 120 || !firstShowAdsFull)
        {
            firstShowAdsFull = true;
            timeShowAds = 0;
            WP8Statics.ShowAdsFull("");

        }
#elif UNITY_IOS
        if (timeShowAds > 120 || !firstShowAdsFull)
        {
            firstShowAdsFull = true;
            timeShowAds = 0;
            IOsStatic.ShowAdsFull(" ", " ");
            
        }
#endif



    }
    public static void ShowADS_BANNER()
    {

#if UNITY_ANDROID
        /*
        if (!firstShowAdsBanner)
        {
            firstShowAdsBanner = true;
            if (UM_AdManager.instance.IsInited == false)
            {
                UM_AdManager.instance.Init();
            }
            int id = UM_AdManager.instance.CreateAdBanner(TextAnchor.LowerCenter);
            UM_AdManager.instance.ShowBanner(id);
            Debug.Log("tentente");
        }*/   
#elif UNITY_WP8
        WP8Statics.ShowAdsBanner("");
#elif UNITY_IOS// vi ben code kia luon show luc dau
        IOsStatic.ShowAds(" ", " ");
#endif



    }
  

}
