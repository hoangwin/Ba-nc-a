using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BGcontrol : MonoBehaviour {

	// Use this for initialization
    public Sprite BG1;
    public Sprite BG2;
    public Sprite BG3;
    
    public Sprite BGThume1;
    public Sprite BGThume2;
    public Sprite BGThume3;
    public Image BGThume;
    public static int index;
    public static BGcontrol instance;
	void Start () {
        instance = this;
        index = 0;
	}
	

	// Update is called once per frame
	void Update () {
	
	}
    public static void setIndex(int offset)
    {
        index += offset;
        if (index < 0)
            index = 2;
        else if (index > 2)
            index = 0;
        BGcontrol obj = GameObject.Find("MainBackGround").GetComponent<BGcontrol>();
        GameObject obj1 = GameObject.Find("game_bg_Thum");
        //Debug.Log(obj);
       // Debug.Log(GameObject.Find("MainBackGround"));
        if (index == 0)
        {
            GameObject.Find("MainBackGround").GetComponent<SpriteRenderer>().sprite = obj.BG1;
            if (obj1 != null)     
                instance.BGThume.sprite = obj.BGThume1;            
        }
        else if (index == 1)
        {
            GameObject.Find("MainBackGround").GetComponent<SpriteRenderer>().sprite = obj.BG2;
            if (obj1 != null)
                instance.BGThume.sprite = obj.BGThume2;            
                
        }
        else if (index == 2)
        {
            GameObject.Find("MainBackGround").GetComponent<SpriteRenderer>().sprite = obj.BG3;
            if (obj1 != null)
                instance.BGThume.sprite = obj.BGThume3;            
                
        }        
    }
}
