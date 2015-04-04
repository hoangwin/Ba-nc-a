using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	// Use this for initialization

	public static MainMenu instance;
	void Start () {
		DEF.Init ();
		DEF.ScaleAnchorGui();
		ScoreControl.loadGame();
		setBGButton ();
		instance = this;
        if (SoundEngine.soundclick == null)
        {
            SoundEngine.soundclick = GameObject.Find("SoundClick");
            DontDestroyOnLoad(SoundEngine.soundclick);
        }
	}	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}

	}
	public void setBGButton()
	{
		GameObject bgButton = GameObject.Find("LabelSoundOnOff");
		UILabel  target = bgButton.GetComponentInChildren<UILabel>();
		if(SoundEngine.isSound)
			target.text = "Sound : On";
		else
			target.text = "Sound : Off";
		//target.MakePixelPerfect();
	}
}
