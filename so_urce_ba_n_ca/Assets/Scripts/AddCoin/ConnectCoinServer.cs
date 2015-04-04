using UnityEngine;
using System.Collections;

public class ConnectCoinServer : MonoBehaviour {
    public static bool isFishCheckBeginGame = false;    
    public static WWW www = null;
    public static ConnectCoinServer instance;
	// Use this for initialization
	void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
       
	}
    public IEnumerator CheckAddCoininServerApter3Second()
    {
        Debug.Log("about to yield return WaitForSeconds(3)");
        yield return new WaitForSeconds(3);
        CheckAddCoininServer();
    }
    public void CheckAddCoininServer()
    {

        if (AddCoinControl.isNeedCheckCoinInServer)
        {
            AddCoinControl.isNeedCheckCoinInServer = false;
            //    www = new WWW("http://gamethuanviet.com/ban_ca_an_xu/GetCoin.php?username=" + ScoreControl._UDID);
            Debug.Log("WWW : " + "http://gamethuanviet.com/ban_ca_an_xu/GetCoin.php?username=" + ScoreControl._UDID);
            HTML.GetHTML("http://gamethuanviet.com/ban_ca_an_xu/GetCoin.php?username=" + ScoreControl._UDID, (HTML.EventHandlerCompleted)ONDONE, this);
        }


    }
    void ONDONE()
    {
        string str = HTML.STR_RESULE;
        Debug.Log("Mesage:" + HTML.STR_RESULE);
        if (str == null)
        {

            AddCoinControl.instance.LabelAdcoinNotive.text = "Không kết nối được Server";
                
            
            return;
        }
        Debug.Log("here :" + str);
        str = str.Trim();
        if (str.Length > 2)
        {
            int coin = int.Parse(str);

            if (coin > 0)
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
                WWW www1 = new WWW("http://gamethuanviet.com/ban_ca_an_xu/DeleteCoin.php?username=" + ScoreControl._UDID);
                if (AddCoinControl.instance != null)
                    AddCoinControl.instance.LabelAdcoinNotive.text = "Bạn nhận được "+str +" Coin";
#if UNITY_ANDROID
						AddCoinControl.stopADS();
#elif UNITY_WP8
                         WP8Statics.StopAds("");
#endif
            }
            else
            {
                if (AddCoinControl.instance != null)
                    AddCoinControl.instance.LabelAdcoinNotive.text = "Bạn nhận được 0 Coin";
            }
        }
        else
        {
            if (AddCoinControl.instance!=null)
                AddCoinControl.instance.LabelAdcoinNotive.text = "Bạn nhận được 0 Coin";
        }
    }
}
