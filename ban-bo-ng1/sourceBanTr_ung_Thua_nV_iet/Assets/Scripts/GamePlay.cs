using UnityEngine;
using System.Collections;

public class GamePlay : MonoBehaviour {

	public GameObject PanelOverGame;
	public GameObject PanelWin;
	public GameObject PanelPause;
    public GameObject Gunner;
    public GameObject BubblePre;

    public Sprite SpriteStar1;
    public Sprite SpriteStar2;
    public Sprite SpriteStar3;
	//public static float speedx = 0.06f ;
	public static GamePlay instance;

	//state
	public static int currentState = 0;
	public static int nextState = 0;
	public const int STATE_WATTING = 0;
	public const int STATE_PLAY = 1;
	public const int STATE_DROP = 2;
	public const int STATE_OVER = 3;
	public const int STATE_PAUSE = 4;
	public const int STATE_WAITING_WIN_LOSE = 5;
	public const int STATE_WIN = 6;
	public const int STATE_LOSE = 7;
	//end state

	public static float TimePlayedSubState = 0f;
	
	public static float CURRENT_START_Y = -3.3f;
	public static int STATE_INIT = 0;
	public static bool isWin = true;
	public UILabel LabelLevel;
	public UILabel LabelScore;

    public Animator GunnerAnim;
	void Start () {

		DEF.Init();


		instance = this;
		//NGUITools.SetActive(PanelOverGame,false);   
		ScoreControl.Score = 0;
		DEF.ScaleAnchorGui();
		if(LevelManager.currentLevel < 1)
			LevelManager.currentLevel = 1;
		if(LevelManager.currentLevel >= 680)
			LevelManager.currentLevel = 679;
		LevelManager.getLevel(LevelManager.currentLevel);
        
        LevelManager.creatNewBubble();
        GameObject.Find("WallBottom").layer = 12;
		Physics2D.IgnoreLayerCollision (12, 12, true);
		changeState(STATE_PLAY);

        LabelLevel.text = LevelManager.currentLevel.ToString() + "\nMàn";
		LabelScore.text ="0\nĐiểm";
        GameObject obj = GameObject.Find("AnimGunner");
       
        BubblePre.layer = 12;
	}
	
	// Update is called once per frame
	void Update () 
	{
		TimePlayedSubState += Time.deltaTime;
        AdsManager.timeShowAds += Time.deltaTime;
		if (Input.GetKeyDown (KeyCode.Escape)) {
				
                if (currentState == STATE_PLAY)
                {
                    GamePlay.changeState(GamePlay.STATE_PAUSE);
                    GamePlay.instance.PanelPause.SetActive(true);
                }
                else if (currentState == STATE_PAUSE)
                {
                    GamePlay.changeState(GamePlay.STATE_PLAY);
                    GamePlay.instance.PanelPause.SetActive(false);
                }
                else
                {
                    Application.LoadLevel("MainMenu");
                }
		}        
//		Debug.Log(currentState);
		switch (currentState) {
			
		
				
		
		case GamePlay.STATE_PLAY:
			shootBubble();
            LevelManager.UpdatekMoveWall();
			break;
		case GamePlay.STATE_WAITING_WIN_LOSE:
            if (TimePlayedSubState < 2)
                break;

			if(isWin)
			{

				PanelWin.SetActive(true);
                AdsManager.ShowADS_FULL();
                GameObject.Find("LabelScoreWin").GetComponent<UILabel>().text = ScoreControl.Score.ToString();
                if (ScoreControl.mUnblockLevel == LevelManager.currentLevel)
                {
                    ScoreControl.mUnblockLevel++;
                }
                
                //kiem tra star
             
                int star = 0;
                if (LevelManager.countbubbleShoot <= LevelManager.star3)
                {
                    star = 3;
                    GameObject.Find("Star").GetComponent<SpriteRenderer>().sprite = SpriteStar3;
                }
                else if (LevelManager.countbubbleShoot <= LevelManager.star2)
                {
                    star =2;
                    GameObject.Find("Star").GetComponent<SpriteRenderer>().sprite = SpriteStar2;
                }
                else
                {
                    star =1;
                    GameObject.Find("Star").GetComponent<SpriteRenderer>().sprite = SpriteStar1;
                }
                //save thanh string
                //Debug.Log("----------------------");
                //Debug.Log("LevelManager.star3:" + ScoreControl.strLevelStar);

                ScoreControl.strLevelStar = ScoreControl.strLevelStar.Insert(LevelManager.currentLevel, star.ToString());
                //Debug.Log("LevelManager.star2:" + ScoreControl.strLevelStar);
                ScoreControl.strLevelStar = ScoreControl.strLevelStar.Remove(LevelManager.currentLevel + 1,1);
                //Debug.Log("LevelManager.star1:" + ScoreControl.strLevelStar);     
                ScoreControl.saveGame();

				changeState(STATE_WIN);
			}
			else
			{
				PanelOverGame.SetActive(true);
				changeState(STATE_LOSE);
			}

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

	public void shootBubble()
	{
       
           
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0) || Input.GetMouseButton(0)||
            ((Input.touchCount == 1) && (Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Ended)))
        {
            Vector2 fingerPos = new Vector2(0, 0);
            if (Input.touchCount == 1)
            {
                fingerPos = Input.GetTouch(0).position;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                fingerPos = Input.mousePosition;
            }
            fingerPos = Camera.main.ScreenToWorldPoint(fingerPos);

            if (fingerPos.y > 4.1 || fingerPos.y < -2)
                return;


            fingerPos.y = fingerPos.y - CURRENT_START_Y;
            //Debug.Log("rrr:"+fingerPos.x+","+fingerPos.y);
            float Max = 15f;

            if (Mathf.Abs(fingerPos.x) > Mathf.Abs(fingerPos.y))
            {
                fingerPos.y = Mathf.Abs(fingerPos.y / fingerPos.x * Max);
                fingerPos.x = fingerPos.x / Mathf.Abs(fingerPos.x) * Max;
            }
            else
            {
                fingerPos.x = fingerPos.x / Mathf.Abs(fingerPos.y) * Max;
                fingerPos.y = Max;
            }

          
            float angle = -Mathf.Atan2(fingerPos.x, fingerPos.y) * 180 / Mathf.PI;  
            Gunner.transform.eulerAngles = new Vector3(0, 0, angle);
             LevelManager.currentBubble.transform.eulerAngles = new Vector3(0, 0, angle);
            if (LevelManager.currentBubble.GetComponent<Bubble>().state == Bubble.STATE_BUBBLE_WATING_SHOOT)
            {

                if (Input.GetMouseButtonUp(0) || ((Input.touchCount == 1) && (Input.GetTouch(0).phase == TouchPhase.Ended)))
                {
                    SoundEngine.playSound("SoundShoot");
                    LevelManager.countbubbleShoot++;
                    GunnerAnim.Play("GUNNER_SHOOT");
                    LevelManager.currentBubble.GetComponent<Rigidbody2D>().velocity = fingerPos;//
                    LevelManager.currentBubble.GetComponent<Bubble>().currentvelocity = fingerPos;//
                    LevelManager.currentBubble.transform.eulerAngles = new Vector3(0, 0, 0);
                    LevelManager.currentBubble.GetComponent<Bubble>().state = Bubble.STATE_BUBBLE_SHOOT;
                }
            }
        }
	
	}

	public void restart() 
	{
        if (LevelManager.currentLevel >= 679)
            LevelManager.currentLevel = 1;
		LevelManager.getLevel(LevelManager.currentLevel);
		PanelPause.SetActive(false);
		PanelWin.SetActive(false);
		PanelOverGame.SetActive(false);
		changeState( GamePlay.STATE_PLAY);
		ScoreControl.Score =0;
		LabelLevel.text = LevelManager.currentLevel.ToString() +"\nLevel";
		LabelScore.text ="0\nScore";
        GameObject.Destroy(LevelManager.currentBubble);        
        LevelManager.creatNewBubble();
	}
	public void initGameOver()
	{
//		BoxCollider2D [] arraycollider;
		//for (int i= trapList.Count-1; i>-1; i--) {
		//	arraycollider =((GameObject) trapList[i]).GetComponents<BoxCollider2D>();
		//	for(int j =0; j<arraycollider.Length;j++)
		//		arraycollider[j].enabled = false;
		//}
	}

	
}
