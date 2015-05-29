
using UnityEngine;
using System.Collections;
public class LevelManager {
	public static string allLevels;
	public static string[] arraylevelList;
	public static float BEGIN_X = -2.375f;
    public static float BEGIN_Y_DEFAULT = 3.8f;
    public static float BEGIN_Y_TARGET = 3.8f;
	public static float BEGIN_Y = 3.8f;
	public static float WIDTH = 0.53f;
	public static float HIGHT_OFFSET_Y = -0.039f;
	public const int MAX_COL = 10;
	public const int MAX_ROW = 17;
	public static GameObject[,] bubbleTableArray = new GameObject[MAX_ROW,MAX_COL];
	public static ArrayList bubbleListNeighboursSameValue = new ArrayList();//la de tinh cac object se cung value
	public static ArrayList bubbleListNeighbours = new ArrayList();//la xem thu thang nao se rot xuong
    public static ArrayList bubbleListStillAliveIndex = new ArrayList();// la nhung gia tri da co san tren bang. neu ko co thi remove ra
	public static int currentLevel = -1;
    public static GameObject currentBubble = null;
    public static int countAllBubble = 0;
    public static int countAllBubbleType = 0;
    public static int star1 = 0;//de xep hang dc bao nhieu *
    public static int star2 = 0;
    public static int star3 = 0;
    public static int countbubbleShoot;
    public static bool isMoveWall;
	public static float getposX(int indexX,int indexY)
	{
		if((indexY%2) == 0)
			return BEGIN_X + indexX*WIDTH;
		else
			return BEGIN_X + indexX*WIDTH - WIDTH/2;
	}
	
    public static float getposY(int indexX,int indexY)
	{
		return BEGIN_Y - indexY*(WIDTH + HIGHT_OFFSET_Y);
	}

	public static int calIndexX(float x,int indexY)
	{
		if((indexY%2) == 0)
		{
			return (int)((x - BEGIN_X  + WIDTH/2)/WIDTH);
		}
		else
		{
			int i = (int)((x - BEGIN_X + WIDTH)/WIDTH);
			if(i==0)
				i++;
			return i;
		}
	}

	public static int calIndexY(float y)
	{
		//BEGIN_Y - indexY*(WIDTH +HIGHT_OFFSET_Y ) = y;
		return (int)((BEGIN_Y - y  + WIDTH/2) /(WIDTH + HIGHT_OFFSET_Y));
	}

	
	public static int[,] getLevel(int level)
    {
        BEGIN_Y = BEGIN_Y_DEFAULT;
        isMoveWall = false;
        GameObject objWall = GameObject.Find("WallTop");
        objWall.transform.localPosition = new Vector3(objWall.transform.localPosition.x, BEGIN_Y + WIDTH / 2 + 0.1f, 0);

        countAllBubble = 0;
        countbubbleShoot = 0;
		if(level<0) level =1;
		string file  = "Level/"+ ((level-1)/10 + 1);
		TextAsset txt = (TextAsset)Resources.Load(file, typeof(TextAsset));	
		Debug.Log("file :" +file +" , Level :" + ((level-1)%10));
		Debug.Log(	txt.text);	
		allLevels = txt.text;
		arraylevelList = allLevels.Split('+'); 	
		string data = arraylevelList[(level-1)%10];
		//Debug.Log(data);
		int[,] temp = new int[MAX_ROW,MAX_COL];

	    for (int j=0 ; j<MAX_ROW ; j++)
	    {
			for (int i=0 ; i<MAX_COL ; i++)
	            {
	                    temp[j,i] = -1;
	            }
	    }

	    int tempX = 0;
	    int tempY = 0;

	    for (int i=0 ; i<data.Length ; i++)
	    {
	            if (data[i] >= 48 && data[i] <= 55)
	            {
	                    temp[tempY,tempX] = (int)(data[i] - 48);
	                    tempX++;
	            }
	            else if (data[i] == 45)
	            {
	                    temp[tempY,tempX] = -1;
	                    tempX++;
	            }

	            if (tempX == MAX_COL)
	            {
	                    tempY++;

	                    if (tempY == MAX_ROW)
	                    {
	                            return temp;
	                    }

	                    tempX = tempY % 2;
	            }
	    }

		for (int j=0 ; j<MAX_ROW ; j++)
		{
			for (int i=0 ; i<MAX_COL ; i++)
			{
				if(bubbleTableArray[j,i] != null)
				{
					GameObject.Destroy(bubbleTableArray[j,i]);
				}
				if(temp[j,i] > -1)
				{
                    countAllBubble++;

					GameObject obj = (GameObject)GameObject.Instantiate (Resources.Load ("BubblePrefab")); 
					obj.GetComponent<Bubble>().indexX =i;
					obj.GetComponent<Bubble>().indexY =j;
				
					obj.GetComponent<Bubble>().setPos();
					obj.GetComponent<Bubble>().setValue(temp[j,i]);
					obj.GetComponent<Bubble>().state = Bubble.STATE_BUBBLE_IDE;
					//obj.rigidbody2D.mass = 1;
                    obj.rigidbody2D.isKinematic = true;
                    //obj.rigidbody2D.
                     //   obj.set

                   // obj.rigidbody2D.gameObject.SetActive(false);
					bubbleTableArray[obj.GetComponent<Bubble>().indexY,obj.GetComponent<Bubble>().indexX] = obj;
					 
					//BubbleListObject.Add(obj);
				}
			}
		}
        LevelManager.getBubbleListStillAliveIndex();
        countAllBubbleType = bubbleListStillAliveIndex.Count;
        star3 = countAllBubble / countAllBubbleType;
        star2 = (int)( star3 * 1.5f);
        star1 = (int)(star3 * 2f);
		return temp;
	}

    public static int getMaxLevel()
    {
		return arraylevelList.Length;
    }
  
    public static void getBubbleListStillAliveIndex()
    {
        bubbleListStillAliveIndex.Clear();
        for (int j = 0; j < MAX_ROW; j++)
        {
            for (int i = 0; i < MAX_COL; i++)
            {
                if (bubbleTableArray[j, i] != null)
                {
                    if (bubbleTableArray[j, i].GetComponent<Bubble>().state == Bubble.STATE_BUBBLE_IDE)//here
                    {
                        int value = bubbleTableArray[j, i].GetComponent<Bubble>().value;
                        int n = 0;
                        for (n = 0; n < bubbleListStillAliveIndex.Count; n++)
                        {

                            if ((int)bubbleListStillAliveIndex[n] == value)
                                break;
                        }
                        if (n == bubbleListStillAliveIndex.Count)
                            bubbleListStillAliveIndex.Add(value);
                    }

                }
            }
        }

    }
  
    public static bool checkABubbleWillShoot(int value)
    {
        for (int i = 0; i < bubbleListStillAliveIndex.Count; i++)
        {
            if(value == ((int)bubbleListStillAliveIndex[i]))
                return true;
        }
        return false;
    }
	
    public static void setDefauldBeforeCheck()
	{
		for (int j=0 ; j<MAX_ROW ; j++)
		{
			for (int i=0 ; i<MAX_COL ; i++)
			{
				if(bubbleTableArray[j,i] != null)
					bubbleTableArray[j,i].GetComponent<Bubble>().isCheck = false;
			}
		}

	}
	
    public static void addObjectNeighborsSameValue(int row,int col,int _value)
	{

		if(bubbleTableArray[row,col] != null)
			//if(bubbleTableArray[row,col].GetComponent<Bubble>() != null)
				if(bubbleTableArray[row,col].GetComponent<Bubble>().value == _value && !bubbleTableArray[row,col].GetComponent<Bubble>().isCheck)
				{
					bubbleListNeighboursSameValue .Add(bubbleTableArray[row,col]);
					bubbleTableArray[row,col].GetComponent<Bubble>().isCheck = true;
				}

	}
	
    public static void addObjectNeighbors(int row,int col)
	{
		
		if(bubbleTableArray[row,col] != null)
			//if(bubbleTableArray[row,col].GetComponent<Bubble>() != null)
				if(!bubbleTableArray[row,col].GetComponent<Bubble>().isCheck)
				{
					bubbleListNeighbours.Add(bubbleTableArray[row,col]);
					bubbleTableArray[row,col].GetComponent<Bubble>().isCheck = true;
				}
		
	}

	public static bool getAllNeighborsSameValue(GameObject currentBubble)
	{
		bubbleListNeighboursSameValue.Clear();
		setDefauldBeforeCheck();
		bubbleListNeighboursSameValue.Add(currentBubble);
		int value = currentBubble.GetComponent<Bubble>().value;
		currentBubble.GetComponent<Bubble>().isCheck = true;
		GameObject obj = null;
		int row = 0 ;
		int col = 0;
		for(int i = 0;i< bubbleListNeighboursSameValue.Count;i++)
		{
			obj = (GameObject)bubbleListNeighboursSameValue[i];
			row = obj.GetComponent<Bubble>().indexY;
			col = obj.GetComponent<Bubble>().indexX;
			//kiem tra 6 vi tri xung quanh
			//
			//		6   6   4   4   2   2   3   3
			//		  6   6   4   4   2   2   3
			//		2   2   3  (1)   (2)   6   4   4
			//		  2   3  (3)  (0)  (4)   4   4
			//		-   -   -  (5)   (6)   -   -   -
			//		  -   -   -   -   -   -   -
			//		-   -   -   -   -   -   -   -
			//		  -   -   -   -   -   -   -
			//		-   -   -   -   -   -   -   -
			//		  -   -   -   -   -   -   -
			//vi tri so 1

			if ((row % 2) == 0) {//herehere
				if (col > 0) {

					addObjectNeighborsSameValue(row,col-1,value);
					//bubbleListObjectSameValue.Add(tableArray[row][col-1]);
				}
				
				if (col < MAX_COL -1) {
					addObjectNeighborsSameValue(row,col+1,value);
					//bubbleListObjectSameValue.Add(bubbleTableArray[row,col+1]);
					
					if (row > 0) {
						addObjectNeighborsSameValue(row-1,col,value);
						addObjectNeighborsSameValue(row-1,col+1,value);
						//bubbleListObjectSameValue.Add(bubbleTableArray[row-1,col]);
						//bubbleListObjectSameValue.Add(bubbleTableArray[row-1,col+1]);
					}
					
					if (row < MAX_ROW -1) {
						addObjectNeighborsSameValue(row+1,col,value);
						addObjectNeighborsSameValue(row+1,col + 1,value);
						//bubbleListObjectSameValue.Add(bubbleTableArray[row+1,col]);
						//bubbleListObjectSameValue.Add(bubbleTableArray[row+1,col+1]);
					}
				} else {
					if (row > 0) {
						addObjectNeighborsSameValue(row-1,col,value);
						//bubbleListObjectSameValue.Add(bubbleTableArray[row-1,col]);
					}
					
					if (row < MAX_ROW -1) {
						addObjectNeighborsSameValue(row+1,col,value);
						//bubbleListObjectSameValue.Add(bubbleTableArray[row+1,col]);
					}
				}
			} else {
				if (col < MAX_COL -1) {
					addObjectNeighborsSameValue(row,col+1,value);
					//bubbleListObjectSameValue.Add(bubbleTableArray[row,col+1]);
				}
				
				if (col > 0) {
					addObjectNeighborsSameValue(row,col-1,value);
					//bubbleListObjectSameValue.Add(bubbleTableArray[row,col-1]);
					
					if (row > 0) {
						addObjectNeighborsSameValue(row-1,col,value);
						addObjectNeighborsSameValue(row-1,col-1,value);
						//bubbleListObjectSameValue.Add(bubbleTableArray[row-1,col]);
						//bubbleListObjectSameValue.Add(bubbleTableArray[row-1,col-1]);
					}
					
					if (row < MAX_ROW-1) {
						addObjectNeighborsSameValue(row+1,col,value);
						addObjectNeighborsSameValue(row+1,col-1,value);
						//bubbleListObjectSameValue.Add(bubbleTableArray[row+1,col]);
						//bubbleListObjectSameValue.Add(bubbleTableArray[row+1,col-1]);
					}
				} else {
					if (row > 0) {
						addObjectNeighborsSameValue(row-1,col,value);
						//bubbleListObjectSameValue.Add(bubbleTableArray[row-1,col]);
					}
					
					if (row < MAX_ROW) {
						addObjectNeighborsSameValue(row + 1,col,value);
						//bubbleListObjectSameValue.Add(bubbleTableArray[row+1,col]);
					}
				}
			}



		}
		//Debug.Log ("Check Count" + bubbleListNeighboursSameValue.Count);
		//kiem tra
		if(bubbleListNeighboursSameValue.Count>=3)
		{
			activeBubbleDrop();
			return true;
		}




		return false;
	}
	
    public static void activeBubbleDrop()
	{

		setDefauldBeforeCheck();
		for(int n = 0; n< bubbleListNeighboursSameValue.Count;n++)
		{
			((GameObject)( bubbleListNeighboursSameValue[n])).GetComponent<Bubble>().isCheck = true;

		}
		for (int j=0 ; j<MAX_ROW ; j++)
		{
			for (int i=0 ; i<MAX_COL ; i++)
			{
				bubbleListNeighbours.Clear();
				if(bubbleTableArray[j,i] != null && !bubbleTableArray[j,i].GetComponent<Bubble>().isCheck)
				{

					getAllNeighbors(bubbleTableArray[j,i]);
					//Debug.Log("Nummm : "+ bubbleListNeighbours.Count);
					//kiem tra o day. neu ko co lien ket den 0 thi se cho roi
					bool isdrop = true;
					for(int n = 0; n< bubbleListNeighbours.Count;n++)
					{
						if(((GameObject)( bubbleListNeighbours[n])).GetComponent<Bubble>().indexY ==0)
						{
							isdrop = false;
							break;
						}
						
					}
					if(isdrop)
					{
						Bubble bubble;
						for(int n = 0; n< bubbleListNeighbours.Count;n++)
						{

							bubble = ((GameObject)( bubbleListNeighbours[n])).GetComponent<Bubble>();
                            
                           
							bubble.rigidbody2D.gravityScale =1;
							bubble.collider2D.enabled = false;
                            bubble.rigidbody2D.isKinematic = false;
							bubble.rigidbody2D.velocity = new Vector2(Random.Range(-2f,2f),Random.Range(1f,3f));
							bubbleTableArray[bubble.indexY,bubble.indexX] = null;
							
						}
					}
				}
				
				
				
			}
		}
	}
	
    public static void checkSetAnimDestroyAllMatch()
	{
		GameObject obj = null;
		for(int i = 0;i< bubbleListNeighboursSameValue.Count;i++)
		{
           
			obj = (GameObject)bubbleListNeighboursSameValue[i];
            LevelManager.bubbleTableArray[obj.GetComponent<Bubble>().indexY, obj.GetComponent<Bubble>().indexX] = null;
            obj.GetComponent<Bubble>().destroyAfterAnim();
		}
	}
	
    public static void checkSetAnimDestroyAllMatch2()
    {
        GameObject bubble = null;
        for (int i = 0; i < bubbleListNeighboursSameValue.Count; i++)
        {
            
            bubble = (GameObject)bubbleListNeighboursSameValue[i];
            bubble.rigidbody2D.gravityScale = 1;
            bubble.collider2D.enabled = false;
            bubble.rigidbody2D.isKinematic = false;
            bubble.rigidbody2D.velocity = new Vector2(Random.Range(-2f, 2f), Random.Range(1f, 3f));
            LevelManager.bubbleTableArray[bubble.GetComponent<Bubble>().indexY, bubble.GetComponent<Bubble>().indexX] = null;
        }
    }
  
    public static bool checkWin()
	{


			for (int i=0 ; i<MAX_COL ; i++)
			{
                if (bubbleTableArray[0, i] != null && (bubbleTableArray[0, i].GetComponent<Bubble>().state != Bubble.STATE_BUBBLE_WAITTING_DESTROY))
					return false;					
			}

		return true;
	}
   
    /*
	public static void destroyAllObjectExplotion()
	{
		if(GamePlay.currentState != GamePlay.STATE_PLAY)
		{

			return;
		}
       
		for (int j=0 ; j<MAX_ROW ; j++)
		{
			for (int i=0 ; i<MAX_COL ; i++)
			{
				if(bubbleTableArray[j,i] != null)
					if(bubbleTableArray[j,i].GetComponent<Bubble>().state ==Bubble.STATE_BUBBLE_WAITTING_DESTROY)
				{
					bubbleTableArray[j,i].GetComponent<Bubble>().state = Bubble.STATE_BUBBLE_DESTROY;
					GameObject obj = bubbleTableArray[j,i];
					bubbleTableArray[j,i] = null;
					GameObject.Destroy(obj);
                    ScoreControl.Score += 100;
				}
			}
		}
        GamePlay.instance.LabelScore.text = ScoreControl.getRealScore().ToString() + "\nScore";
		if(checkWin())
		{
			GamePlay.isWin = true;
			GamePlay.changeState(GamePlay.STATE_WAITING_WIN_LOSE);
		}
	}
    */
	
    public static void getAllNeighbors(GameObject currentBubble)
	{
			bubbleListNeighbours.Clear();			
			bubbleListNeighbours.Add(currentBubble);			
			currentBubble.GetComponent<Bubble>().isCheck = true;
			GameObject obj = null;
			int row = 0 ;
			int col = 0;
			for(int i = 0;i< bubbleListNeighbours.Count;i++)
			{
				obj = (GameObject)bubbleListNeighbours[i];
				row = obj.GetComponent<Bubble>().indexY;
				col = obj.GetComponent<Bubble>().indexX;

				if ((row % 2) == 0) {
					if (col > 0) {
						
						addObjectNeighbors(row,col-1);
						//bubbleListObjectSameValue.Add(tableArray[row][col-1]);
					}
					
					if (col < MAX_COL -1) {
						addObjectNeighbors(row,col+1);
						//bubbleListObjectSameValue.Add(bubbleTableArray[row,col+1]);
						
						if (row > 0) {
							addObjectNeighbors(row-1,col);
							addObjectNeighbors(row-1,col+1);
							//bubbleListObjectSameValue.Add(bubbleTableArray[row-1,col]);
							//bubbleListObjectSameValue.Add(bubbleTableArray[row-1,col+1]);
						}
						
						if (row < MAX_ROW -1) {
							addObjectNeighbors(row+1,col);
							addObjectNeighbors(row+1,col + 1);
							//bubbleListObjectSameValue.Add(bubbleTableArray[row+1,col]);
							//bubbleListObjectSameValue.Add(bubbleTableArray[row+1,col+1]);
						}
					} else {
						if (row > 0) {
							addObjectNeighbors(row-1,col);
							//bubbleListObjectSameValue.Add(bubbleTableArray[row-1,col]);
						}
						
						if (row < MAX_ROW -1) {
							addObjectNeighbors(row+1,col);
							//bubbleListObjectSameValue.Add(bubbleTableArray[row+1,col]);
						}
					}
				} else {
					if (col < MAX_COL -1) {
						addObjectNeighbors(row,col+1);
						//bubbleListObjectSameValue.Add(bubbleTableArray[row,col+1]);
					}
					
					if (col > 0) {
						addObjectNeighbors(row,col-1);
						//bubbleListObjectSameValue.Add(bubbleTableArray[row,col-1]);
						
						if (row > 0) {
							addObjectNeighbors(row-1,col);
							addObjectNeighbors(row-1,col-1);
							//bubbleListObjectSameValue.Add(bubbleTableArray[row-1,col]);
							//bubbleListObjectSameValue.Add(bubbleTableArray[row-1,col-1]);
						}
						
						if (row < MAX_ROW-1) {
							addObjectNeighbors(row+1,col);
							addObjectNeighbors(row+1,col-1);
							//bubbleListObjectSameValue.Add(bubbleTableArray[row+1,col]);
							//bubbleListObjectSameValue.Add(bubbleTableArray[row+1,col-1]);
						}
					} else {
						if (row > 0) {
							addObjectNeighbors(row-1,col);
							//bubbleListObjectSameValue.Add(bubbleTableArray[row-1,col]);
						}
						
						if (row < MAX_ROW) {
							addObjectNeighbors(row + 1,col);
							//bubbleListObjectSameValue.Add(bubbleTableArray[row+1,col]);
						}
					}
				}
			}
	}
    
    public static void creatNewBubble()
    {
        
        LevelManager.currentBubble = (GameObject)(GameObject.Instantiate(Resources.Load("BubblePrefab")));
        int value = GamePlay.instance.BubblePre.GetComponent<Bubble>().value;
       

        if (LevelManager.bubbleListStillAliveIndex.Count > 0)
        {
            bool isChange = true;
            for (int i = 0; i < LevelManager.bubbleListStillAliveIndex.Count; i++)
            {
                if (value == (int)(LevelManager.bubbleListStillAliveIndex[i]))
                    isChange = false;
            }

            if (isChange)
            {
             //   Debug.Log("Change Change");
                int index = Random.Range(0, LevelManager.bubbleListStillAliveIndex.Count);
                value = (int)(LevelManager.bubbleListStillAliveIndex[index]);
            }
        }
        LevelManager.currentBubble.GetComponent<Bubble>().setValue(value);

        LevelManager.currentBubble.GetComponent<Bubble>().transform.localPosition = new Vector3(0f, GamePlay.CURRENT_START_Y, 0f);
        LevelManager.currentBubble.GetComponent<Bubble>().state = Bubble.STATE_BUBBLE_WATING_SHOOT;
        LevelManager.currentBubble.layer = 12;    
        if (LevelManager.bubbleListStillAliveIndex.Count > 0)
        {
            int index = Random.Range(0, LevelManager.bubbleListStillAliveIndex.Count);
            GamePlay.instance.BubblePre.GetComponent<Bubble>().setValue((int)(LevelManager.bubbleListStillAliveIndex[index]));
        }

    }
   
    public static void CheckMoveWall()
    {
        if (countbubbleShoot % 5 == 0 && countbubbleShoot > 0)
        {
            BEGIN_Y_TARGET = BEGIN_Y_DEFAULT - WIDTH * (countbubbleShoot / 5);
            isMoveWall = true;
        }
    }

    public static void UpdatekMoveWall()
    {
        if (isMoveWall)
        {
            BEGIN_Y -= 0.005f;
            if(BEGIN_Y < BEGIN_Y_TARGET)
            {
                BEGIN_Y = BEGIN_Y_TARGET;
                isMoveWall = false;
            }
            SetAllPos();
          
        }        
        
    }
    public static void SetAllPos()
    {
        GameObject obj = GameObject.Find("WallTop");
        obj.transform.localPosition = new Vector3(obj.transform.localPosition.x, BEGIN_Y + WIDTH/2 + 0.1f, 0);
        for (int j = 0; j < MAX_ROW; j++)
        {
            for (int i = 0; i < MAX_COL; i++)
            {
                if (bubbleTableArray[j, i] != null)
                {
                    bubbleTableArray[j, i].GetComponent<Bubble>().setPos();
                    //BubbleListObject.Add(obj);
                }
            }
        }

    }
}
