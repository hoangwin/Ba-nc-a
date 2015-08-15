using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour {
	// Use this for initialization

	public static MainMenu instance;
    public GameObject background;
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
        DEF.scaleFixImagetoScreen(background);
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
        Text target = bgButton.GetComponentInChildren<Text>();
		if(SoundEngine.isSound)
			target.text = "Sound : On";
		else
			target.text = "Sound : Off";
		//target.MakePixelPerfect();
	}
}
