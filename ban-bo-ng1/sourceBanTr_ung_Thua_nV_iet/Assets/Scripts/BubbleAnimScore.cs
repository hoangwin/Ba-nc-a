using UnityEngine;
using System.Collections;

public class BubbleAnimScore : MonoBehaviour {

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void setDestroy()
    {
        //		Debug.Log("gg:"+ gameObject.name);
       // LevelManager.destroyAllObjectExplotion();
        GameObject.Destroy(this.transform.parent.gameObject);
        ScoreControl.Score += 100;
        GamePlay.instance.LabelScore.text = ScoreControl.Score.ToString() + "\nScore";
        if (GamePlay.currentState == GamePlay.STATE_WIN)
        {
            GameObject.Find("LabelScoreWin").GetComponent<UILabel>().text = ScoreControl.Score.ToString();
        }

    }
}
