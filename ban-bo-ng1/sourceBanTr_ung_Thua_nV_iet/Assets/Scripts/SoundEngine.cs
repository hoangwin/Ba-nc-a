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
          //  Debug.Log("Play Sound0");
			if (sound != null) {
			//		 Debug.Log("Play Sound1");
                if(str.Equals("SoundCoin"))
                {
               //     Debug.Log("Play Sound2");
                    if(!sound.GetComponent<AudioSource>().isPlaying)
                        sound.GetComponent<AudioSource>().Play ();
                }
                else if (str.Equals("SoundClick"))
                {
                //    Debug.Log("Play Sound3");
                    if (soundclick != null)
                    {
                        soundclick.GetComponent<AudioSource>().Play();
                        Debug.Log( soundclick.GetComponent<AudioSource>());
                    }
                  //  else
                   //     Debug.Log("NULL");
                }

                else
                {
                    Debug.Log("Play Sound4");
                    sound.GetComponent<AudioSource>().Play();
                }
			}
		}
	}
}
