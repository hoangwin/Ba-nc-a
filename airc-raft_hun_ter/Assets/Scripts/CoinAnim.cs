using UnityEngine;
using System.Collections;

public class CoinAnim : MonoBehaviour {

	// Use this for initialization
    public void CoinMoveComplete()
    {
        SoundEngine.instance.PlayOneShot(SoundEngine.instance._soundCoin);
        GameObject.Destroy(this.gameObject);
    }


}
