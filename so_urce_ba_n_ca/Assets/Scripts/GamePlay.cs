using UnityEngine;
using System.Collections;

public class GamePlay : MonoBehaviour {
    public GameObject PanelPause;
    public GameObject Gunner;
    public GameObject CoinPostion;
	public ManagerFish managerFish;

	//public static float speedx = 0.06f ;
	public static GamePlay instance;
	//state
	public static int currentState = 0;
	public static int nextState = 0;
	public const int STATE_WATTING = 0;
	public const int STATE_PLAY = 1;
    public static bool isShowPauseMenu = false;
    public static int isShowFrameDiaLog = 0;
    //end state

	public static float TimePlayedSubState = 0f;
    public static int FrameCount = 0;   
	
	public static int STATE_INIT = 0;
	public static bool isWin = true;
	public UILabel LabelCoin;
	public UILabel LabelBestCoin;
    public UILabel LabelTimeAddCoin;
    public GameObject BGTimeAddCoin;
    public static int gameMode = 0;
    public GameObject ButtonAdcoin;
    void Awake()
    {
        // Make the game run as fast as possible in the web player
        Application.targetFrameRate = 60;
    }
	void Start () {
		DEF.Init();
        ///managerFish = gameObject.AddComponent<ManagerFish>();
		//managerFish = new ManagerFish();
		
		instance = this;		
		DEF.ScaleAnchorGui();

		//Physics2D.IgnoreLayerCollision (12, 12, true);
		changeState(STATE_PLAY);      
        LabelCoin.text = ScoreControl._Coin.ToString();
        LabelBestCoin.text = ScoreControl._BestCoin.ToString();

        isShowPauseMenu = false;
        isShowFrameDiaLog = 0;
        LeanTween.init(150);
        ScoreControl.InitTimerAddCoin(BGTimeAddCoin,LabelTimeAddCoin, LabelCoin,LabelBestCoin);
        ButtonControl.state = 1;
        ButtonControl.DialogState = ButtonControl.DIALOG_STATE_GAME_PLAY;
        BGcontrol.setIndex(0);
        
   //     if ( MainMenu.iShowButtonAdcoin)
   //     {        
      
   //     }
  //      else
   //     {
   //         ButtonAdcoin.SetActive(false);
   //     }
        MainMenu.ShowADS_FULL();
	}
    
	// Update is called once per frame
	void Update () 
	{
        FrameCount++;
		TimePlayedSubState += Time.deltaTime;

        ScoreControl.UpdateTimerAddCoin(BGTimeAddCoin,LabelTimeAddCoin, LabelCoin);
        ButtonControl.instance.EscapePress();                
		
//		Debug.Log(currentState);
		switch (currentState) {
		
		case GamePlay.STATE_PLAY:
			break;
		}

		if(nextState != currentState)
		{
			currentState = nextState;
			return;
		}
	}
	public static void changeState(int State)
	{
		nextState = State;
	}
	void FixedUpdate()
	{

	}



    void OnApplicationPause(bool pause)
    {
       if(pause)
       {
           ScoreControl.saveGame();
       }
       else
       {
          // if (ConnectCoinServer.instance!=null)
          // {
          //     StartCoroutine(ConnectCoinServer.instance.CheckAddCoininServerApter3Second());
          // }
           // ConnectCoinServer.instance.CheckAddCoininServer(); 
           
       }
    }

	void OnApplicationQuit ()
    {
        ScoreControl.saveGame();
        Debug.Log("Exit o day");
    }
}
