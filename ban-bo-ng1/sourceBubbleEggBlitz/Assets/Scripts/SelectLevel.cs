using UnityEngine;
using System.Collections;

public class SelectLevel : MonoBehaviour {

	public static SelectLevel instance;
    public static int currentpage = 1;
    public static int MAX_PAGE = 34;
    public GameObject background;
	void Start () {
		DEF.Init();
		DEF.ScaleAnchorGui();		
		instance = this;
        currentpage = ScoreControl.mUnblockLevel / 20 + 1;
        if (currentpage > 34) currentpage = 34;
        setAllButton();
        DEF.scaleFixImagetoScreen(background);
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
           // Debug.Log("-----------------------");
           // Debug.Log(level);
          //  Debug.Log(ScoreControl.strLevelStar.Length);
            
          //  Debug.Log(ScoreControl.mUnblockLevel);
            if (level == ScoreControl.mUnblockLevel)
            {
                GameObject.Find("Background" + i.ToString()).GetComponent<UISprite>().spriteName = "MenuSelectLevel0";
            
            }else if (level < ScoreControl.mUnblockLevel)
            {
                
            //  GameObject.Find ("Background" + i.ToString()).GetComponent<SpriteRenderer>().sprite = ButtonNormal1;
                if (ScoreControl.strLevelStar[level] == '1')
                    GameObject.Find("Background" + i.ToString()).GetComponent<UISprite>().spriteName = "MenuSelectLevel1";
                else if (ScoreControl.strLevelStar[level] == '2')
                    GameObject.Find("Background" + i.ToString()).GetComponent<UISprite>().spriteName = "MenuSelectLevel2";
                else
                    GameObject.Find("Background" + i.ToString()).GetComponent<UISprite>().spriteName = "MenuSelectLevel3";
            }
            else
            {
                GameObject.Find("Background" + i.ToString()).GetComponent<UISprite>().spriteName = "MenuSelectLevelDisable";
            }
            
        }

        GameObject.Find("LabelPage").GetComponent<UILabel>().text = currentpage.ToString() +"/" + MAX_PAGE.ToString();
    }

}
