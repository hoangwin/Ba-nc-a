using UnityEngine;
using System.Collections;

public class AnimControl : MonoBehaviour {

	// Use this for initialization
    public GameObject mainBUtton1; Vector3 posmainBUtton1;
    public GameObject mainBUtton2; Vector3 posmainBUtton2;
    public GameObject mainBUtton3; Vector3 posmainBUtton3;
    public GameObject mainBUtton4; Vector3 posmainBUtton4;
    public GameObject ancho1;
    public GameObject ancho2;
    public static AnimControl instance;
	void Start () {
	    instance = this;
                
        posmainBUtton1 = mainBUtton1.transform.position;
        posmainBUtton2 = mainBUtton2.transform.position;
        posmainBUtton3 = mainBUtton3.transform.position;
        posmainBUtton4 = mainBUtton4.transform.position;
      //  posmainBUtton2 = camera.WorldToScreenPoint(mainBUtton2.transform.localPosition);
      //  posmainBUtton3 = camera.WorldToScreenPoint(mainBUtton3.transform.localPosition);
      //  posmainBUtton4 = camera.WorldToScreenPoint(mainBUtton4.transform.localPosition);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void MenuBegin()
    {
        // iTween.MoveFrom(mainBUtton1, iTween.Hash("x", -.8, "time", .6, "delay", 1.2, "oncomplete", "setFlag", "oncompleteTarget", gameObject));
        //iTween.MoveFrom(mainBUtton4, iTween.Hash("x", -2, "time", 0.9, "oncomplete", "setFlag"));
     //   iTween.MoveFrom(mainBUtton1, iTween.Hash("x", -3, "time", 0.6));      
        //iTween.MoveBy(gameObject, iTween.Hash("x", 2, "easeType", "easeInOutExpo", "loopType", "pingPong", "delay", .1));
       
        if (mainBUtton1 != null)
        {
            mainBUtton1.transform.position = posmainBUtton1;
            mainBUtton2.transform.position = posmainBUtton2;
            mainBUtton3.transform.position = posmainBUtton3;
            mainBUtton4.transform.position = posmainBUtton4;

            
            
          //  float x = camera.WorldToScreenPoint(ancho1.transform.position).x;
          //  Debug.Log(x);
          //  Debug.Log("  mainBUtton1.transform.position :" + ancho1.transform.position.x);
            
          //  Debug.Log(Screen.width);
            iTween.MoveFrom(mainBUtton1, iTween.Hash("x", - Screen.width/2, "time", 0.6, "easeType", "linear", "islocal", true));
            iTween.MoveFrom(mainBUtton2, iTween.Hash("x", - Screen.width/2, "time", 0.7, "easeType", "linear", "islocal", true));
            iTween.MoveFrom(mainBUtton3, iTween.Hash("x", - Screen.width/2, "time", 0.8, "easeType", "linear", "islocal", true));
            iTween.MoveFrom(mainBUtton4, iTween.Hash("x", - Screen.width/2, "time", 0.9, "easeType", "linear", "islocal", true));
        }       
    }
    public void menuEnd()
    {
       
        if (mainBUtton1 != null)
        {
            mainBUtton1.transform.position = posmainBUtton1;
            mainBUtton2.transform.position = posmainBUtton2;
            mainBUtton3.transform.position = posmainBUtton3;
            mainBUtton4.transform.position = posmainBUtton4;
            iTween.MoveTo(mainBUtton1, iTween.Hash("x", 4 * Screen.width/5, "time", 0.3, "easeType", "linear", "islocal", true, "oncomplete", "onCompletedMainMenuSHowAds"));
            iTween.MoveTo(mainBUtton2, iTween.Hash("x", 4 * Screen.width/5, "time", 0.4, "easeType", "linear", "islocal", true));
            iTween.MoveTo(mainBUtton3, iTween.Hash("x", 4 * Screen.width / 5, "time", 0.5, "easeType", "linear", "islocal", true));
            iTween.MoveTo(mainBUtton4, iTween.Hash("x", 4 * Screen.width / 5, "time", 0.6, "easeType", "linear", "islocal", true, "oncomplete", "onCompletedMainMenu"));
        }
        
    }
}
