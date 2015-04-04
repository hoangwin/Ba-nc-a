using UnityEngine;
using System.Collections;

public class WinAnimHandle : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void showAdmobFull()
    {
        Debug.Log("Show Ads");
        AdsManager.ShowADS_FULL();
    }
}
