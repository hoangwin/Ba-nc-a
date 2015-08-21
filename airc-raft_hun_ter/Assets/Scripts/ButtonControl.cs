using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonControl : MonoBehaviour {

    public Toggle toggleMusic;
    public Toggle togglesfx;
    public GameObject PanelOption;
    public GameObject PanelInfo;
    public GameObject PanelUI;
    public Image buttonIGM;
    public Sprite buttonIGMUP;
    public Sprite buttonIGMDOWN;
    public Text[] textFishCollection;
    public static ButtonControl instance;

    public static int state = 0; //0 = main menu//1 = game play
    public static int DialogState = 0;
    public const int DIALOG_STATE_BUTON_MAINMENU =0;
    public const int DIALOG_STATE_OPTION = 1;
    public const int DIALOG_STATE_HELP = 2;
    public const int DIALOG_STATE_INFO = 3;
    public const int DIALOG_STATE_ADD_COIN = 4;
    public const int DIALOG_STATE_GAME_PLAY = 5;
    


    void Start () {
        Debug.Log("aaaaaaaaaaaaa");
        instance = this;
        
    }
   
    void OnDestroy () {
    }
 
    // 1 = on , 2 = off
 
    public void OnSoundToggleChange(bool value ) {
        if (togglesfx.isOn) {
            SoundEngine.isSoundSFX = true;
        //    Debug .Log  ("sound toggle 1");
            SoundEngine.instance.PlayOneShot(SoundEngine.instance._soundclick);
        } else {
            SoundEngine.isSoundSFX = false;
         //   Debug .Log  ("sound toggle 2");
        }
        
    }
    public void OnMusicToggleChange(bool value)
    {
        if (toggleMusic.isOn)
        {
            Debug .Log  ("music toggle 1");
            SoundEngine.isSoundMusic = true;
            SoundEngine.instance.PlayLoop(SoundEngine.instance._soundBG1);
        } else {
            Debug .Log  ("music toggle 2");
            SoundEngine.isSoundMusic = false;
            SoundEngine.instance.stopSound();
        }
       
    }
 

	public void PuzzleButtonPress()
	{
        GamePlay.gameMode = 0;
        Application.LoadLevel("GamePlayScence");
        SoundEngine.instance.PlayOneShot(SoundEngine.instance._soundclick);
		
    
	}

    public void ExtraModeButtonPress()
	{
        GamePlay.gameMode = 1;
        SoundEngine.instance.PlayOneShot(SoundEngine.instance._soundclick);
		Application.LoadLevel("GamePlayScence");
    
	}
    public void ButtonSharedFB()
    {
        if (ScoreControl._isFB == 0)
        {

            ScoreControl.addCoind(150);           
            ScoreControl._isFB = 1;
            ScoreControl.saveGame();
            MainMenu.instance.LabelCoin.text = ScoreControl._Coin.ToString();
            MainMenu.instance.LabelShareFB.SetActive(false);
            Bonus.instance.playAnimAddCoinCompleted();
        }      
#if UNITY_ANDROID
        using (AndroidJavaClass jc = new AndroidJavaClass("com.mygame.bancaanxuhd.UnityPlayerNativeActivity"))
        {
			jc.CallStatic<int>("SharedFB");
		}
#elif UNITY_WP8
        WP8Statics.SharedFB("Test");
#endif

    }

    public void ButtonRateApp()
    {
        if (ScoreControl._isRate == 0)
        {
            ScoreControl.addCoind(150);
            ScoreControl._isRate = 1;
            ScoreControl.saveGame();
            MainMenu.instance.LabelRating.SetActive(false);
            MainMenu.instance.LabelCoin.text = ScoreControl._Coin.ToString();
            Bonus.instance.playAnimAddCoinCompleted();
        }       
#if UNITY_ANDROID
        SoundEngine.instance.PlayOneShot(SoundEngine.instance._soundclick);
		Application.OpenURL ("market://details?id=com.mygame.bancaanxuhd");
#elif UNITY_WP8
        WP8Statics.RateApp("Test");
#endif

    }
    public void ButtonQuitPress()
    {
        Application.Quit();
    }
	public void IGMButtonPress()
	{
        SoundEngine.instance.PlayOneShot(SoundEngine.instance._soundclick);
        HideAllDiaLog();
        if (GamePlay.isShowPauseMenu)
        {
            buttonIGM.sprite = buttonIGMUP;
            iTween.MoveTo(GamePlay.instance.PanelPause, iTween.Hash("y", -Screen.height/2));
            GamePlay.isShowPauseMenu = !GamePlay.isShowPauseMenu;
            GamePlay.isShowFrameDiaLog = 2;//chuan bi dong //here

        }
        else
        {
            buttonIGM.sprite = buttonIGMDOWN;
            iTween.MoveTo(GamePlay.instance.PanelPause, iTween.Hash("y", GamePlay.instance.PanelPauseTranFormBegin.y));
            GamePlay.isShowPauseMenu = !GamePlay.isShowPauseMenu;
            GamePlay.isShowFrameDiaLog = 3;//chuan bi dong //here
        }
        
	}
    public void HideAllDiaLog()
    {
      

        if (PanelOption != null)
            PanelOption.SetActive(false);

        if (PanelInfo != null)
            PanelInfo.SetActive(false);

        if (PanelUI != null && ButtonControl.state == 0)
            PanelUI.SetActive(false);
    }
    public void HelpButtonPress()
    {
        SoundEngine.instance.PlayOneShot(SoundEngine.instance._soundclick);
        HideAllDiaLog();
        if (ButtonControl.state == 1)
            IGMButtonPress();
        PanelInfo.SetActive(true);
        showHelpBoard();
        DialogState = DIALOG_STATE_HELP;
        GamePlay.isShowFrameDiaLog = 3;
    }
    public void showHelpBoard()
    {

        SoundEngine.instance.PlayOneShot(SoundEngine.instance._soundclick);
        //GameObject.Find("InfoNotice").GetComponent<UILabel>().text = "craft coin value";
        for (int i = 0; i < 15; i++)
        {
            textFishCollection[i].text = Fish.coinArray[i].ToString();
        }

    }
    public void InfoButtonPress()
    {
        SoundEngine.instance.PlayOneShot(SoundEngine.instance._soundclick);
        
        HideAllDiaLog();
        if (ButtonControl.state == 1)
        {
            
            IGMButtonPress();
        }
        PanelInfo.SetActive(true);
        showInfoBoard();
        DialogState = DIALOG_STATE_INFO;
        GamePlay.isShowFrameDiaLog = 3;
    }
    public void showInfoBoard()
    {
        
       // GameObject.Find("InfoNotice").GetComponent<UILabel>().text = "The number of fish you have fired";
        for (int i = 0; i < 15; i++)
        {
            textFishCollection[i].text =  ScoreControl._FishShooted[i].ToString();
            Debug.Log(i);

        }
    }
	public void MainMenuButtonPress()
	{
        SoundEngine.instance.PlayOneShot(SoundEngine.instance._soundclick);
		Application.LoadLevel ("MainMenu");
        ScoreControl.saveGame();
	}
	//Select 	Level

    public void ButtonOPtionPress()
    {
        HideAllDiaLog();
        if (!SoundEngine.isSoundSFX)
        {
         //   ButtonControl.instance.soundToggle.value = false;
        }
        if (!SoundEngine.isSoundMusic)
        {
           // ButtonControl.instance.musicToggle.value = false;
        }

        SoundEngine.instance.PlayOneShot(SoundEngine.instance._soundclick);
        DialogState = DIALOG_STATE_OPTION;
        if (ButtonControl.state == 0)
            PanelUI.SetActive(false);		
        if (ButtonControl.state == 1)            
            IGMButtonPress();
        PanelOption.SetActive(true);
        GamePlay.isShowFrameDiaLog = 3;//chuan bi dong
       
    }
     

    public void ButtonChangeGunLeft()
    {
        SoundEngine.instance.PlayOneShot(SoundEngine.instance._soundGunChange);
        GamePlay.instance.Gunner.GetComponent<Gunner>().setGunType(-1);
        GamePlay.isShowFrameDiaLog = 2;
    }
    public void ButtonChangeGunRight()
    {
        SoundEngine.instance.PlayOneShot(SoundEngine.instance._soundGunChange);
        GamePlay.isShowFrameDiaLog = 2;
        GamePlay.instance.Gunner.GetComponent<Gunner>().setGunType(1);
    }
    //addcoin
    public void ButtonCloseOptionPress()
    {
        SoundEngine.instance.PlayOneShot(SoundEngine.instance._soundclick);
        if (ButtonControl.state == 0)
        {
            PanelUI.SetActive(true);
            DialogState = DIALOG_STATE_BUTON_MAINMENU;
        }
        else
        {
            GamePlay.isShowFrameDiaLog = 2;//chuan bi dong
            DialogState = DIALOG_STATE_GAME_PLAY;
        }
        
        PanelOption.SetActive(false);
    }


    public void ButtonCloseInfoPress()
    {
        SoundEngine.instance.PlayOneShot(SoundEngine.instance._soundclick);
        if (ButtonControl.state == 0)
        {
            PanelUI.SetActive(true);
            DialogState = DIALOG_STATE_BUTON_MAINMENU;
        }
        else
        {
            GamePlay.isShowFrameDiaLog = 2;//chuan bi dong
            DialogState = DIALOG_STATE_GAME_PLAY;
        }
        
        PanelInfo.SetActive(false);
    }
    

    public void ButtonThumRight()
    {
        SoundEngine.instance.PlayOneShot(SoundEngine.instance._soundclick);
        BGcontrol.setIndex(1);
        GamePlay.isShowFrameDiaLog = 2;//chuan bi dong //here
    }
    public void ButtonThumLeft()
    {
        SoundEngine.instance.PlayOneShot(SoundEngine.instance._soundclick);
        BGcontrol.setIndex(1);
        GamePlay.isShowFrameDiaLog = 2;//chuan bi dong //here
    }
    public void EscapePress()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            switch (ButtonControl.DialogState)
            {
                case ButtonControl.DIALOG_STATE_BUTON_MAINMENU:
                    Debug.Log("Quit");
                    Application.Quit();
                    break;
                case ButtonControl.DIALOG_STATE_GAME_PLAY:
                    if (GamePlay.isShowPauseMenu)
                    {
                        buttonIGM.sprite = buttonIGMUP;
                        iTween.MoveTo(GamePlay.instance.PanelPause, iTween.Hash("y", -Screen.height / 2));
                    //    iTween.MoveTo(GamePlay.instance.PanelPause, iTween.Hash("y", -6));
                        GamePlay.isShowPauseMenu = !GamePlay.isShowPauseMenu;
                    }
                    else
                    {
                        buttonIGM.sprite = buttonIGMDOWN;
                        iTween.MoveTo(GamePlay.instance.PanelPause, iTween.Hash("y", GamePlay.instance.PanelPauseTranFormBegin.y));
                        //iTween.MoveTo(GamePlay.instance.PanelPause, iTween.Hash("y", 1));
                        GamePlay.isShowPauseMenu = !GamePlay.isShowPauseMenu;
                    }
                    break;
                case ButtonControl.DIALOG_STATE_OPTION:
                    ButtonControl.instance.ButtonCloseOptionPress();
                    break;
                case ButtonControl.DIALOG_STATE_HELP:
                    ButtonControl.instance.ButtonCloseInfoPress();                    
                    break;
                case ButtonControl.DIALOG_STATE_INFO:
                    ButtonControl.instance.ButtonCloseInfoPress();                    
                    break;
                case ButtonControl.DIALOG_STATE_ADD_COIN:
                   // AddCoinControl.instance.ButtonCloseAddCoinPress();                    
                    break;

            }
        }
    }
}
