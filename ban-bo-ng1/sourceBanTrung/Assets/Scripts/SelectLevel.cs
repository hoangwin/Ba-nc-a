using UnityEngine;
using System.Collections;

public class SelectLevel : MonoBehaviour {

	public static SelectLevel instance;
    public static int currentpage = 1;
    public static int MAX_PAGE = 34;
    public Sprite spritestar0;
    public Sprite spritestar1;
    public Sprite spritestar2;
    public Sprite spritestar3;
    public Sprite spritedisable;

	void Start () {
		DEF.Init();
		DEF.ScaleAnchorGui();		
		instance = this;
        currentpage = ScoreControl.mUnblockLevel / 20 + 1;
        if (currentpage > 34) currentpage = 34;
        setAllButton();
        
	}

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.LoadLevel("MainMenu");
			
		}
	}
	public void setAllButton()
    {
        
        int level = 1;
        for (int i = 1; i <= 20; i++)
        {
            level = (currentpage - 1) * 20 + i;        
            GameObject.Find("Label" + i.ToString()).GetComponent<UILabel>().text  = level.ToString();
        //    Debug.Log("-----------------------");
           // Debug.Log(level);
          //  Debug.Log(ScoreControl.strLevelStar.Length);
            
          //  Debug.Log(ScoreControl.mUnblockLevel);
            if (level == ScoreControl.mUnblockLevel)
            {
                GameObject.Find("ButtonLevel" + i.ToString()).GetComponent<UIButton>().normalSprite = "MenuSelectLevel0";// spritestar0;
            
            }else if (level < ScoreControl.mUnblockLevel)
            {
                
            //  GameObject.Find ("Background" + i.ToString()).GetComponent<SpriteRenderer>().sprite = ButtonNormal1;
                if (ScoreControl.strLevelStar[level] == '1')
                    GameObject.Find("ButtonLevel" + i.ToString()).GetComponent<UIButton>().normalSprite = "MenuSelectLevel1";
                else if (ScoreControl.strLevelStar[level] == '2')
                    GameObject.Find("ButtonLevel" + i.ToString()).GetComponent<UIButton>().normalSprite =  "MenuSelectLevel2";
                else
                    GameObject.Find("ButtonLevel" + i.ToString()).GetComponent<UIButton>().normalSprite = "MenuSelectLevel3";
            }
            else
            {
                GameObject.Find("ButtonLevel" + i.ToString()).GetComponent<UIButton>().normalSprite = "MenuSelectLevelDisable";
            }
            
        }

        GameObject.Find("LabelPage").GetComponent<UILabel>().text = currentpage.ToString() +"/" + MAX_PAGE.ToString();
    }

}

