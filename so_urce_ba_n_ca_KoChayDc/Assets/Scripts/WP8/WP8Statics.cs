using UnityEngine;
using System.Collections;

public static class WP8Statics
{

    //public delegate void MyDelegate(int num);
    //public static MyDelegate myDelegate;

    public static event System.EventHandler WP8FunctionHandleSMSOpen; //WP8FunctionHandle se la ham trong file xaml.cs
    public static event System.EventHandler WP8FunctionHandleStopAds;//StopAds
    public static event System.EventHandler WP8FunctionHandleRateApp;//StopAds
    public static event System.EventHandler WP8FunctionHandle2FbShared;//StopAds
    public static event System.EventHandler WP8FunctionHandlePurchaseIAP; //WP8FunctionHandle se la ham trong file xaml.cs


    public static void SendSMS(string str)
    {
        if (WP8FunctionHandleSMSOpen != null)
        {

            WP8FunctionHandleSMSOpen(str, null);
        }
    }
    public static void PurchaseIAP(string str)
    {
        if (WP8FunctionHandlePurchaseIAP != null)
        {

            WP8FunctionHandlePurchaseIAP(str, null);
        }
    }
    public static void RateApp(string str)
    {
        if (WP8FunctionHandleRateApp != null)
        {

            WP8FunctionHandleRateApp(str, null);
        }
        //WPUnityBridge.Singleton.Instance.Purchase("4916GameThuanViet.FishHunterHD6000Coin", null, null);
    }

    public static void StopAds(string str)
    {
        ScoreControl._isAdcoin = 1;
        ScoreControl.saveGame();
        if (WP8FunctionHandleStopAds != null)
        {
            WP8FunctionHandleStopAds(str, null);
        }
    }
    public static void SharedFB(string str)
    {
      //  string path = Application.persistentDataPath +"/BanCaAnXu.jpg" ;
      //  Debug.Log(path);
      //  Application.CaptureScreenshot(path);
        if (WP8FunctionHandle2FbShared != null)
        {
            WP8FunctionHandle2FbShared("", null);
        }
    }
    public static void AddCoin(int coin)
    {
        ScoreControl.addCoind(coin);
        if (ButtonControl.state == 0)
        {
            MainMenu.instance.LabelCoin.text = ScoreControl._Coin.ToString();

        }
        else
            GamePlay.instance.LabelCoin.text = ScoreControl._Coin.ToString();
        Bonus.instance.playAnimAddCoinCompleted();
        ScoreControl.saveGame();        
        if (AddCoinControl.instance != null)
            AddCoinControl.instance.LabelAdcoinNotive.text = "Bạn nhận được " + coin + " Coin";

    }
}