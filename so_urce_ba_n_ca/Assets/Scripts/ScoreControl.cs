using UnityEngine;
using System.Collections;

public class ScoreControl : MonoBehaviour {
    public static string _UserName = "NaN";
    public static string _UDID = "PHONE_UID";	
    public static int _Coin = 0;
    public static int _Coin1 = 0;
	public static int _BestCoin = 0;
    public static int _MAX_LEVEL = 680;    
    public static int[] _FishShooted = new int[15];
    public static string _strCoinBackup = "USER_BACKUP";//chong hack
    public static int _isAdcoin = 0;
    public static int _isRate = 0;
    public static int _isFB = 0;

	public static string _STRING_ASSET_USER_NAME ="USER_NAME";
    public static string _STRING_ASSET_COIN = "USER_COIN";
    public static string _STRING_ASSET_COIN_BACKUP = "USER_BACKUP";//chong hack  
    public static string _STRING_ASSET_BEST_COIN = "BEST_COIN";
    public static string _STRING_ASSET_FISH_SHOOTED = "FISH_SHOOT";    	
	public static string _STRING_ASSET_BACKINDEX = "BACKGROUND_INDEX";
    public static string _STRING_ASSET_IS_ADD_COIN = "IS_ADD_COIN";
    public static string _STRING_ASSET_IS_RATE = "IS_RATE";
    public static string _STRING_ASSET_IS_FB = "IS_FB";
    public static string _STRING_ASSET_TIME_WATTING_ADD_COIN = "TIME_WAITTIN_ADD_COIN";    	
    public static int _MAX_COIN_INIT = 300;
	
	public static string strFishShooted = "0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0";

    public static float TimeWaittingAddCoin = 0f;	
    // Use this for initialization

    public static int OFFSET_COIN = 0;
	void Start () {
        PlayerPrefs.DeleteAll();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public static void addCoind(int addcoin)
    {
        Debug.Log("aaaaaaaaaaa");
        //Debug.Log("Coin:" + ScoreControl._Coin);
        //Debug.Log("Coin1:" + ScoreControl._Coin1);
        if (_Coin == _Coin1 - OFFSET_COIN)
        {
            _Coin += addcoin;
            _Coin1 = _Coin + OFFSET_COIN;
        }
        else
        {
            _Coin = _Coin1 - OFFSET_COIN;
        }
    }
	public static void saveGame()
    {
        strFishShooted = "";
        for (int i = 0; i < 15; i++)
        {
            strFishShooted += _FishShooted[i].ToString() +" ";        
        }
        strFishShooted.Trim();
        
		PlayerPrefs.SetString(_STRING_ASSET_USER_NAME, _UserName);

        PlayerPrefs.SetInt(_STRING_ASSET_COIN, _Coin);        
        PlayerPrefs.SetInt(_STRING_ASSET_BEST_COIN, _BestCoin);
        PlayerPrefs.SetString(_STRING_ASSET_FISH_SHOOTED, strFishShooted);

            //public static string _STRING_COIN_BACKUP = "USER_BACKUP";//chong hack
        _strCoinBackup = _Coin.ToString();
        _strCoinBackup = _strCoinBackup.Replace('0', 'a');
        _strCoinBackup = _strCoinBackup.Replace('1', 'b');
        _strCoinBackup = _strCoinBackup.Replace('2', 'c');
        _strCoinBackup = _strCoinBackup.Replace('3', 'd');
        _strCoinBackup = _strCoinBackup.Replace('4', 'e');
        _strCoinBackup = _strCoinBackup.Replace('5', 'f');
        _strCoinBackup = _strCoinBackup.Replace('6', 'g');
        _strCoinBackup = _strCoinBackup.Replace('7', 'h');
        _strCoinBackup = _strCoinBackup.Replace('8', 'i');
        _strCoinBackup = _strCoinBackup.Replace('9', 'j');
        PlayerPrefs.SetString(_STRING_ASSET_COIN_BACKUP, _strCoinBackup);
		PlayerPrefs.SetInt(_STRING_ASSET_BACKINDEX, BGcontrol.index);

        PlayerPrefs.SetInt(_STRING_ASSET_IS_ADD_COIN, _isAdcoin);
        PlayerPrefs.SetInt(_STRING_ASSET_IS_RATE,_isRate);
        PlayerPrefs.SetInt(_STRING_ASSET_IS_FB, _isFB);
        PlayerPrefs.SetFloat(_STRING_ASSET_TIME_WATTING_ADD_COIN, TimeWaittingAddCoin);

		PlayerPrefs.Save();
	}
	public static void loadGame()
	{
		//PlayerPrefs.DeleteAll();
		//return;
        _UDID = SystemInfo.deviceUniqueIdentifier;
		_UserName = PlayerPrefs.GetString(_STRING_ASSET_USER_NAME);
        _Coin = PlayerPrefs.GetInt(_STRING_ASSET_COIN );
       
        _BestCoin = PlayerPrefs.GetInt(_STRING_ASSET_BEST_COIN);        
        strFishShooted = PlayerPrefs.GetString(_STRING_ASSET_FISH_SHOOTED);
       
        
        if (strFishShooted.Length < 1)
            strFishShooted = "0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0";

        string[] tempstr = strFishShooted.Split(' ');
        for (int i = 0; i < tempstr.Length;i++)
        {
            if(i<15)
                _FishShooted[i] = int.Parse(tempstr[i]);
        }
        if (_BestCoin == 0 && _Coin == 0)
        {
            _Coin = _MAX_COIN_INIT;
           
            Debug.Log("_Coin : " + _Coin);
            Debug.Log("_Coin1 : " + _Coin1);
            Debug.Log("----------------");
        }
		_Coin1 = _Coin + OFFSET_COIN;
		if (_UserName.Length <= 4)
						_UserName = "NaN";				        
		//BestScore += OFFSET_SCORE;
        _strCoinBackup = PlayerPrefs.GetString(_STRING_ASSET_COIN_BACKUP);
        _strCoinBackup = _strCoinBackup.Replace('a', '0');
        _strCoinBackup = _strCoinBackup.Replace('b', '1');
        _strCoinBackup = _strCoinBackup.Replace('c', '2');
        _strCoinBackup = _strCoinBackup.Replace('d', '3');
        _strCoinBackup = _strCoinBackup.Replace('e', '4');
        _strCoinBackup = _strCoinBackup.Replace('f', '5');
        _strCoinBackup = _strCoinBackup.Replace('g', '6');
        _strCoinBackup = _strCoinBackup.Replace('h', '7');
        _strCoinBackup = _strCoinBackup.Replace('i', '8');
        _strCoinBackup = _strCoinBackup.Replace('j', '9');

        Debug.Log("_strCoinBackup : " + _strCoinBackup);
        if (_strCoinBackup.Length > 1)
        {
            int temp = int.Parse(_strCoinBackup);
            if (temp != _Coin)//kiem tra co bi hack ko
            {
                _Coin = _MAX_COIN_INIT;
                _Coin1 = _Coin + OFFSET_COIN;
                _BestCoin = 0;
                for (int i = 0; i < tempstr.Length; i++)
                {
                    if (i < 15)
                        _FishShooted[i] = 0;
                }
            }
        }
		BGcontrol.index = PlayerPrefs.GetInt(_STRING_ASSET_BACKINDEX);

        _isAdcoin = PlayerPrefs.GetInt(_STRING_ASSET_IS_ADD_COIN);
        _isRate = PlayerPrefs.GetInt(_STRING_ASSET_IS_RATE);
        _isFB = PlayerPrefs.GetInt(_STRING_ASSET_IS_FB);
        TimeWaittingAddCoin = PlayerPrefs.GetFloat(_STRING_ASSET_TIME_WATTING_ADD_COIN);
	}

    public static void InitTimerAddCoin(GameObject BGTimeAddCoin, UILabel LabelTimeAddCoin, UILabel LabelCoin, UILabel LabelBestCoin)
    {
        if (TimeWaittingAddCoin == 0 && ScoreControl._Coin < 300)
        {
            if (TimeWaittingAddCoin == 0)
            {
                TimeWaittingAddCoin = 10f;
                BGTimeAddCoin.SetActive(true);
            }
        }
        else if(TimeWaittingAddCoin != 0)
        {
            BGTimeAddCoin.SetActive(true);
        }
        else
        {
            LabelTimeAddCoin.text = " ";
            BGTimeAddCoin.SetActive(false);
        }
    }
    public static void UpdateTimerAddCoin(GameObject BGTimeAddCoin, UILabel LabelTimeAddCoin, UILabel LabelCoin)
    {
        if (TimeWaittingAddCoin > 0)
        {
            TimeWaittingAddCoin -= Time.deltaTime;
            LabelTimeAddCoin.text = ((int)TimeWaittingAddCoin).ToString();
            if (TimeWaittingAddCoin < 0)
            {
                ScoreControl.addCoind(50);
                if (ScoreControl._Coin < ScoreControl._MAX_COIN_INIT)
                {
                    TimeWaittingAddCoin = 10;
                    LabelTimeAddCoin.text = ((int)TimeWaittingAddCoin).ToString();
                }
                else
                {
                    TimeWaittingAddCoin = 0;
                    LabelTimeAddCoin.text = " ";
                    BGTimeAddCoin.SetActive(false);
                }
                LabelCoin.text = ScoreControl._Coin.ToString();
            }
        }

    }
}
