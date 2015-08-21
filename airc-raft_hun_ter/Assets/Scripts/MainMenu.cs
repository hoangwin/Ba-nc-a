using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour {
	// Use this for initialization

	public static MainMenu instance;
	ManagerFish managerFish = new ManagerFish();
    public Text LabelCoin;
    public Text LabelBestCoin;
    public Text LabelTimeAddCoin;
    public GameObject BGTimeAddCoin;

    public GameObject LabelRating;
    public GameObject LabelShareFB;
    public GameObject LabelAddCoinNotice;
    
    
	void Start () {
		DEF.Init ();
		//DEF.ScaleAnchorGui();
		ScoreControl.loadGame();		
		instance = this;
       
        ButtonControl.state = 0;
        LabelCoin.text = ScoreControl._Coin.ToString();
        LabelBestCoin.text = ScoreControl._BestCoin.ToString();
        ScoreControl.InitTimerAddCoin(BGTimeAddCoin, LabelTimeAddCoin, LabelCoin,LabelBestCoin);
        if(ConnectCoinServer.isFishCheckBeginGame == false)
        {
            ConnectCoinServer.isFishCheckBeginGame = true;
            AddCoinControl.isNeedCheckCoinInServer = true;
            Debug.Log("Check Coin");
         //   ConnectCoinServer.instance.CheckAddCoininServer(); 
        }
        ButtonControl.DialogState = ButtonControl.DIALOG_STATE_BUTON_MAINMENU;
        BGcontrol.setIndex(0);

        if (ScoreControl._isFB == 1)
        {           
            MainMenu.instance.LabelShareFB.SetActive(false);            
        }
        if (ScoreControl._isRate == 1)
        {
            MainMenu.instance.LabelRating.SetActive(false);
        }

        if (ScoreControl._isAdcoin == 1)
        {
            MainMenu.instance.LabelAddCoinNotice.SetActive(false);
        }
        checkShowAdcoin();
        
	}

    static public float timeShowAds = 0;
    static public bool firstShowAdsFull = false;    
    //--------------------------------------
    //  EVE
    public static void ShowADS_FULL()
    {
#if UNITY_ANDROID
      

        if (timeShowAds > 60 || !firstShowAdsFull)
        {
            firstShowAdsFull = true;
            timeShowAds = 0;
            using (AndroidJavaClass jc = new AndroidJavaClass("com.hunter.aircraft.UnityPlayerNativeActivity"))
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
	// Update is called once per frame
	void Update () {
        ScoreControl.UpdateTimerAddCoin(BGTimeAddCoin, LabelTimeAddCoin, LabelCoin);
        ButtonControl.instance.EscapePress();
	}
    void OnApplicationPause(bool pause)
    {
        if (pause)
        {
          //  ScoreControl.saveGame();
        }
        else
        {
            if (ConnectCoinServer.instance != null)
            {
                if (ConnectCoinServer.instance != null)
                {
                    StartCoroutine(ConnectCoinServer.instance.CheckAddCoininServerApter3Second());
                    //ConnectCoinServer.instance.CheckAddCoininServer();
                }
                
            }
          //  ScoreControl.saveGame();
        }
    }
    public void checkShowAdcoin()
    {
#if UNITY_IPHONE
			int expDate = 20140717;
			int date = int.Parse(System.DateTime.Now.ToString("yyyyMMdd"));
            iShowButtonAdcoin = false;
			if (expDate <= date) {
                iShowButtonAdcoin = true;
               //here ButtonAdcoin.SetActive(true);
				Debug.Log("Date : " + date);
            }
            else
            {
                ButtonAdcoin.SetActive(false);
            }

			Debug.Log("Tick: " + System.DateTime.Now.Ticks);
#endif
    }
	void OnApplicationQuit ()
	{
		ScoreControl.saveGame();
		Debug.Log("Exit Mainmenu o day");
	}
}
