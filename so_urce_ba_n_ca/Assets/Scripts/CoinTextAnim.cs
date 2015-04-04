using UnityEngine;
using System.Collections;

public class CoinTextAnim : MonoBehaviour {
	// Use this for initialization
    public Animator Anim;
	void Start () {
        if(Anim == null)
            Anim = GetComponent<Animator>();
	}
	void Update()
    {
        transform.Translate(0, 1* Time.deltaTime, 0);
    }
	// Update is called once per frame
    public void _destroy()
    {
        GameObject.Destroy(this.gameObject);
    }
}
