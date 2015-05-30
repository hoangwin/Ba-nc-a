using UnityEngine;
using System.Collections;

public class ScoreControl : MonoBehaviour {	
	public static int Score = 0;
	public static int BestScore = 0;
    public static int MAX_LEVEL = 680;
    public static int mUnblockLevel = 0;

	public static string STRING_USER_NAME ="USER_NAME";
	
    public static string STRING_UNBLOCK_LEVEL = "level_puzzle";
	public static string UserName = "NaN";
	public static string STRING_LEVEL_STAR = "LEVEL_STAR";
	public static string strLevelStar =  new string('0',680);
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public static void saveGame()
	{
        
		PlayerPrefs.SetString(STRING_USER_NAME, UserName);
        PlayerPrefs.SetInt(STRING_UNBLOCK_LEVEL, mUnblockLevel);
        PlayerPrefs.SetString(STRING_LEVEL_STAR, strLevelStar);
		PlayerPrefs.Save();
	}
	public static void loadGame()
	{
		//PlayerPrefs.DeleteAll();
		UserName = PlayerPrefs.GetString(STRING_USER_NAME);
        mUnblockLevel = PlayerPrefs.GetInt(STRING_UNBLOCK_LEVEL);
        strLevelStar = PlayerPrefs.GetString(STRING_LEVEL_STAR);
        if (strLevelStar.Length < 1)
            strLevelStar = new string('0', 680);
        if (mUnblockLevel < 1) mUnblockLevel = 1;
		if (UserName.Length <= 4)
						UserName = "NaN";				        
		//BestScore += OFFSET_SCORE;

	}
}
