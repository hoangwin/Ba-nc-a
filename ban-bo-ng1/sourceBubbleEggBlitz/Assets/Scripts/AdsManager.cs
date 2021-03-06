﻿using UnityEngine;
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
    //  EVE
    public static void ShowADS_FULL()
    {  
#if UNITY_ANDROID
        /*
        if (timeShowAds > 80 || !firstShowAdsFull)
        {
            if (!AdsManager.isLoadFullAds)
            {
                Debug.Log("Loading Ads");
                GoogleMobileAd.LoadInterstitialAd();
            }
            else
            {
                Debug.Log("Call Show Full Ads");
                firstShowAdsFull = true;
                timeShowAds = 0;
                GoogleMobileAd.ShowInterstitialAd();
            }
        }
        */

        if (timeShowAds > 60 || !firstShowAdsFull)
        {
            firstShowAdsFull = true;
            timeShowAds = 0;
            using (AndroidJavaClass jc = new AndroidJavaClass("com.bubble.eggblitz.UnityPlayerNativeActivity"))
            {
                jc.CallStatic<int>("ShowAdsFull");
            }
        }
            
#elif UNITY_WP8
        if (timeShowAds > 60 || !firstShowAdsFull)
        {
            firstShowAdsFull = true;
            timeShowAds = 0;
            WP8Statics.ShowAdsFull("");

        }
#elif UNITY_IOS
        if (timeShowAds > 60 || !firstShowAdsFull)
        {
            firstShowAdsFull = true;
            timeShowAds = 0;
            IOsStatic.ShowAdsFull(" ", " ");
        }
#endif



    }
  
  

}
