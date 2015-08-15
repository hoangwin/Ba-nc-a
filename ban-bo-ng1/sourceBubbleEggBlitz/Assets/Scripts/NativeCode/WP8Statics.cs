using UnityEngine;
using System.Collections;

public static class WP8Statics
{
    //public delegate void MyDelegate(int num);
    //public static MyDelegate myDelegate;   
 
    public static event System.EventHandler WP8FunctionHandleStopAds;//StopAds
    public static event System.EventHandler WP8FunctionHandleShowAds;//StopAds
    public static event System.EventHandler WP8FunctionHandleShowAdsBanner;//StopAds
    
    
    public static void StopAds(string str)
    {
     //   ScoreControl._isAdcoin = 1;
     //   ScoreControl.saveGame();
        if (WP8FunctionHandleStopAds != null)
        {
            WP8FunctionHandleStopAds(str, null);
        }
    }

    public static void ShowAdsFull(string str)
    {
        //   ScoreControl._isAdcoin = 1;

        if (WP8FunctionHandleShowAds != null)
        {
            WP8FunctionHandleShowAds(str, null);
        }
    }
    public static void ShowAdsBanner(string str)
    {
        Debug.Log("Show Banner");
        //   ScoreControl._isAdcoin = 1;
        if (WP8FunctionHandleShowAdsBanner != null)
        {
            WP8FunctionHandleShowAdsBanner(str, null);
        }
    }
}