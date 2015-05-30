 using UnityEngine;
using System.Collections;

public class Bubble : MonoBehaviour {
	public int indexX; 
	public int indexY;
	public float fixPosX; 
	public float fixPosY;
	public float currentPosX; 
	public float currentPosY;
	public Vector2 currentvelocity;



    public const int STATE_BUBBLE_WATING_SHOOT = 0;
    public const int STATE_BUBBLE_SHOOT = 1;    
    public const int STATE_BUBBLE_IDE = 2;
    public const int STATE_BUBBLE_WAITTING_DESTROY = 3;
    public const int STATE_BUBBLE_DESTROY = 4;
    public int state = 0;
	
    public int value;

	public bool isCheck = false;
	public Animator anim;// = gameObject.GetComponent<Animator>();
    public Animator animScore;
	// Use this for initialization
	public void setValue(int _value)
	{
		value = _value;
		

		if(anim ==null)
		{
            GameObject obj = getChildGameObject("BubbleAnim");//herehere GameObject obj =  getChildGameObject("BubbleAnim");
			anim = obj.GetComponent<Animator>();
		}
		//switch(valye
		anim.Play("IDE" + value.ToString());
        



	}
	public void setPos()
	{
		fixPosX = LevelManager.getposX(indexX,indexY);
		fixPosY = LevelManager.getposY(indexX,indexY);
		transform.localPosition = new Vector3(fixPosX,fixPosY ,1);
	}

	void Start () {
		GameObject obj =  getChildGameObject("BubbleAnim");
        GameObject obj1 =  getChildGameObject("BubbleAnimEffect");
        
		anim = obj.GetComponent<Animator>();
        animScore = obj1.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		//if(state == 2)//drop
		//	Debug.Log ("u:" + transform.localPosition.y);
		if(transform.localPosition.y <-3.3)
		{
            this.destroyAfterAnim();
          //  SoundEngine.playSound("SoundCoin");
			
		}
	}

	public GameObject getChildGameObject(string withName) {
		//Author: Isaac Dart, June-13.
		Transform pTransform =GetComponent<Transform>();
		
		foreach (Transform trs in pTransform) {
			
			if (trs.gameObject.name == withName)
				
				return trs.gameObject;
			
		}
		
		return null;
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
       // Debug.Log("aaaaaaaaaaaaaaaaaaaaa");

		if(collision.gameObject.name.Equals("Wall1") || collision.gameObject.name.Equals("Wall2"))
		{ 
			if(state != STATE_BUBBLE_IDE)
			{
				//Debug.Log ("Wall");
				rigidbody2D.velocity = new Vector2( currentvelocity.x * (-1f),currentvelocity.y);
				currentvelocity = rigidbody2D.velocity;

			//	Debug.Log("ttttttttttttt");
			}
			else
			{
				setPos();
			}
			return;
		}
      //  else if (collision.gameObject.name.Equals("WallBottom") && GamePlay.currentState == GamePlay.STATE_PLAY)
	//	{
      //      GamePlay.TimePlayedSubState = 0f;
        //    GamePlay.isWin = false;
		//	GamePlay.changeState(GamePlay.STATE_WAITING_WIN_LOSE);		
	//	}

		if(state == STATE_BUBBLE_SHOOT)
		{

			//Debug.Log ("ball");
			indexY = LevelManager.calIndexY(transform.localPosition.y);
			indexX = LevelManager.calIndexX(transform.localPosition.x,indexY);
			//can ckeck cho nay
			if(indexX >= LevelManager.MAX_COL)
				indexX--;
			if(LevelManager.bubbleTableArray[indexY,indexX] != null)
				indexY++;

			if(indexX <0)
				indexX++;
			if(LevelManager.bubbleTableArray[indexY,indexX] != null)
				indexY++;
			//Debug.Log(" " + indexX +", " +indexY);
			state = STATE_BUBBLE_IDE;

            LevelManager.currentBubble.layer = 0;// da dinh tren do
        //    if(collision.gameObject.GetComponent<Bubble>().state == STATE_BUBBLE_IDE)
			    rigidbody2D.velocity = new Vector2(0,0);
			//rigidbody2D.mass = 9999;
            LevelManager.bubbleTableArray[indexY, indexX] = LevelManager.currentBubble;
            
            LevelManager.currentBubble.rigidbody2D.isKinematic = true;

            if (LevelManager.getAllNeighborsSameValue(LevelManager.currentBubble))
            {
                SoundEngine.playSound("SoundMatch");
                //LevelManager.checkSetAnimDestroyAllMatch();
                LevelManager.checkSetAnimDestroyAllMatch2();
            }
            else
            {
                animScore.Play("BUBBLE_ANIM_STICK");
                SoundEngine.playSound("SoundStick");
            }
            LevelManager.getBubbleListStillAliveIndex();
            //Debug.Log("Bubble Value :" + LevelManager.bubbleListStillAliveIndex.Count);
            LevelManager.creatNewBubble();

            LevelManager.CheckMoveWall();

            if (LevelManager.checkWin())
            {
                GamePlay.TimePlayedSubState = 0f;
                GamePlay.isWin = true;
                GamePlay.changeState(GamePlay.STATE_WAITING_WIN_LOSE);
                SoundEngine.playSound("game_win");
            }
           
			setPos();
            

		}
		else
			;//	setPos();
	
	}
    public void destroyAfterAnim()
    {

        GameObject obj =  this.gameObject;
        obj.GetComponent<Bubble>().anim.Play("DESTROY");
        obj.GetComponent<Bubble>().animScore.Play("BUBBLE_ANIM_SCORE_EFFECT");
        obj.GetComponent<Bubble>().state = Bubble.STATE_BUBBLE_WAITTING_DESTROY;
      //  obj.GetComponent<Bubble>().rigidbody2D.isKinematic = false;
        obj.GetComponent<Bubble>().rigidbody2D.velocity = new Vector2(0,0);
        obj.layer = 12;
    }
}
