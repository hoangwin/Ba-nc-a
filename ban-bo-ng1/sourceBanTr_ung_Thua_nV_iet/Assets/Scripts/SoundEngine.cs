using UnityEngine;
using System.Collections;

public class SoundEngine : MonoBehaviour {
	public static bool isSound = true;
    public static GameObject soundclick = null;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static void playSound(string str)
	{
		if (SoundEngine.isSound) {
			GameObject sound = GameObject.Find (str);
			if (sound != null) {
				//	 Debug.Log("Play Sound");
                if(str.Equals("SoundCoin"))
                {
                    if(!sound.audio.isPlaying)
                        sound.audio.Play ();
                }
                else if (str.Equals("SoundClick"))
                {
                    if(soundclick!= null)
                        soundclick.audio.Play();
                }

                else
                    sound.audio.Play();
			}
		}
	}
}
