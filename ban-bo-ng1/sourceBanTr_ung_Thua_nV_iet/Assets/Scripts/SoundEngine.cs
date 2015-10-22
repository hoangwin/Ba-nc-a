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
                    if(!sound.GetComponent<AudioSource>().isPlaying)
                        sound.GetComponent<AudioSource>().Play ();
                }
                else if (str.Equals("SoundClick"))
                {
                    if(soundclick!= null)
                        soundclick.GetComponent<AudioSource>().Play();
                }

                else
                    sound.GetComponent<AudioSource>().Play();
			}
		}
	}
}
