using UnityEngine;
using System.Collections;

public class AddCoinControl : MonoBehaviour {    
    public GameObject PanelButtonSMS;
    public GameObject PanelButtonNetwork;
    public GameObject PanelButtonCARD;
    public UILabel LabelAdcoinNotive;
    public static int networkType = 0;//0 = viettel,1 = mobile phone,2 = vinaphone

    public static AddCoinControl instance;

    public static bool isNeedCheckCoinInServer = false;
	// Use this for initialization
	void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void ButtonCloseAddCoinPress()
    {
        SoundEngine.instance.PlayOneShot(SoundEngine.instance._soundclick);
        if (ButtonControl.state == 0)
        {
            ButtonControl.instance.PanelUI.SetActive(true);
            ButtonControl.DialogState =ButtonControl.DIALOG_STATE_BUTON_MAINMENU;
        }
        else
        {
            GamePlay.isShowFrameDiaLog = 2;//chuan bi dong
            ButtonControl.DialogState = ButtonControl.DIALOG_STATE_GAME_PLAY;
        }
        ButtonControl.instance.PanelAddCoin.SetActive(false);
    }

    public void showPanelButtonSMS()
    {
        
        PanelButtonSMS.SetActive(true);
        PanelButtonNetwork.SetActive(false);
        PanelButtonCARD.SetActive(false);
    }
    void showPanelButtonNetwork()
    {
        PanelButtonSMS.SetActive(false);
        PanelButtonNetwork.SetActive(true);
        PanelButtonCARD.SetActive(false);
    }
    void showPanelButtonCARD()
    {
        PanelButtonSMS.SetActive(false);
        PanelButtonNetwork.SetActive(false);
        PanelButtonCARD.SetActive(true);
    }
    public void ButtonSMSPress()
    {
        showPanelButtonSMS();
    }

    public void ButtonCARDPress()
    {
        showPanelButtonCARD();
    }
    //close button
    public void ButtonClosePress()
    {
        
        ButtonControl.instance.PanelAddCoin.SetActive(false);
        GamePlay.isShowFrameDiaLog = 2;//chuan bi dong
    }

    public void PanelButtonCARDPress10_000()
    {
        AddCoinControl.instance.PanelButtonCARDPress(10000);
    }
    public void PanelButtonCARDPress20_000()
    {
        AddCoinControl.instance.PanelButtonCARDPress(20000);
    }
    public void PanelButtonCARDPress50_000()
    {
        AddCoinControl.instance.PanelButtonCARDPress(50000);
    }
    public void PanelButtonCARDPress100_000()
    {
        AddCoinControl.instance.PanelButtonCARDPress(100000);
    }
    public void PanelButtonCARDPress200_000()
    {
        AddCoinControl.instance.PanelButtonCARDPress(200000);
    }
    public void PanelButtonCARDPress500_000()
    {
        AddCoinControl.instance.PanelButtonCARDPress(500000);
    }
     public void PanelButtonCARDPress(int i)
    {
        SoundEngine.instance.PlayOneShot(SoundEngine.instance._soundclick);
        PanelButtonNetwork.SetActive(true);
        PanelButtonCARD.SetActive(false);
        AddCoinControl.instance.ViettelNetworkButtonPress();
        AddCoinControl.networkType = 0;
    }

     public void ButtonSendSMS1()
     {
         isNeedCheckCoinInServer = true;
         SoundEngine.instance.PlayOneShot(SoundEngine.instance._soundclick);
         AddCoinControl.instance.sendSMS(0);
     }
     public void ButtonSendSMS2()
     {
         isNeedCheckCoinInServer = true;
         SoundEngine.instance.PlayOneShot(SoundEngine.instance._soundclick);
         AddCoinControl.instance.sendSMS(1);
     }
     public void ButtonSendSMS3()
     {
         isNeedCheckCoinInServer = true;
         SoundEngine.instance.PlayOneShot(SoundEngine.instance._soundclick);
         AddCoinControl.instance.sendSMS(2);
     }

     public void ViettelNetworkButtonPress()
     {
         SoundEngine.instance.PlayOneShot(SoundEngine.instance._soundclick);
         //GameObject.Find("BackgroundVietel").transform.parent.GetComponent<UIButton>().normalSprite = "MenuButtonSoundOn";
         GameObject.Find("BackgroundVietel").GetComponent<UISprite>().spriteName = "MenuAdcoin_Network1_select";
         GameObject.Find("BackgroundMobiPhone").GetComponent<UISprite>().spriteName = "MenuAdcoin_Network2";
         GameObject.Find("BackgroundVinaPhone").GetComponent<UISprite>().spriteName = "MenuAdcoin_Network3";
         AddCoinControl.networkType = 0;
     }
     public void MobiPhoneNetworkButtonPress()
     {
         SoundEngine.instance.PlayOneShot(SoundEngine.instance._soundclick);
         //BackgroundMobiPhone
         GameObject.Find("BackgroundVietel").GetComponent<UISprite>().spriteName = "MenuAdcoin_Network1";
         GameObject.Find("BackgroundMobiPhone").GetComponent<UISprite>().spriteName = "MenuAdcoin_Network2_select";
         GameObject.Find("BackgroundVinaPhone").GetComponent<UISprite>().spriteName = "MenuAdcoin_Network3";
         AddCoinControl.networkType = 2;
     }
     public void VinaPhoneNetworkButtonPress()
     {
         SoundEngine.instance.PlayOneShot(SoundEngine.instance._soundclick);
         //BackgroundVinaPhone
         GameObject.Find("BackgroundVietel").GetComponent<UISprite>().spriteName = "MenuAdcoin_Network1";
         GameObject.Find("BackgroundMobiPhone").GetComponent<UISprite>().spriteName = "MenuAdcoin_Network2";
         GameObject.Find("BackgroundVinaPhone").GetComponent<UISprite>().spriteName = "MenuAdcoin_Network3_select";
         AddCoinControl.networkType = 1;
     }
    public void CheckCoinButtonPress()
     {
         AddCoinControl.isNeedCheckCoinInServer = true;
         if (AddCoinControl.instance != null)
             AddCoinControl.instance.LabelAdcoinNotive.text = "Connecting Server";
         if (ConnectCoinServer.instance != null)
             ConnectCoinServer.instance.CheckAddCoininServer();
     }


     public void sendSMS(int index)
     {
         string mobile = "7595";
         string message = "NMH BanCaAnXu " + ScoreControl._UDID;         
         switch (index)
         {
             case 0:
                 mobile = "7595";
                 //Application.OpenURL("sms:" + mobile + "?body=NMH BCTC " + SaveLoadData.UserName);
                 //Debug.Log("sms:" + mobile + "?body=NMH BCTC " + SaveLoadData.UserName);
                 break;
             case 1:
                 mobile = "7695";
                 //Application.OpenURL("sms:" + mobile + "?body=NMH BCTC "+ SaveLoadData.UserName);
                 //Debug.Log("sms:" + mobile + "?body=NMH BCTC " + SaveLoadData.UserName);
                 break;
             case 2:
                 mobile = "7795";
                 //Application.OpenURL("sms:" + mobile + "?body=NMH BCTC "+ SaveLoadData.UserName);
                 //Debug.Log("sms:" + mobile + "?body=NMH BCTC " + SaveLoadData.UserName);
                 break;
         }
         //ButtonConfirmCancelPress();
         Debug.Log("Send SMS: " + message);
         SendSMS(mobile, message);


         /*
         string url = string.Format("sms:{0}?body={1}", number, message);
         Debug.Log(url);
         Application.OpenURL(url);
          * */

     }
     public static void SendSMS(string number, string Message)
     {
#if UNITY_ANDROID
        using (AndroidJavaClass jc = new AndroidJavaClass("com.mygame.bancaanxuhd.UnityPlayerNativeActivity"))
        {
			jc.CallStatic<int>("OpenSMS",number,Message);
		}
#elif UNITY_WP8
         WP8Statics.SendSMS(number + "|" + Message);
#elif UNITY_IOS
         string url = string.Format("sms:{0}?body={1}", number, Message);
        //rem here//here IOsStatic.OpenSMS(number, Message);
#endif
     }
     public static void stopADS()
     {
#if UNITY_ANDROID
         using (AndroidJavaClass jc = new AndroidJavaClass("com.mygame.bancaanxuhd.UnityPlayerNativeActivity"))
        {
			jc.CallStatic<int>("StopShowAds");
		}
#elif UNITY_WP8
         WP8Statics.StopAds("");
#endif
     }
}
