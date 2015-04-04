using UnityEngine;
using System.Collections;

public class HTML
{
    static public string MEGA_LINK = "http://stgamevn.com/toan/stgame/biendong/";
    public delegate void EventHandlerCompleted();
    
    public static EventHandlerCompleted getHTMLCompleted;
    
    public static string STR_RESULE;
    static public string GetHTML2(string uri)
    {
        WWW www = new WWW(uri);

        while (!www.isDone)  //wait until www isdone
            ;

        if (www.error != null)
            return null;
        return www.text;
    }
    static public void GetHTML(string uri, EventHandlerCompleted c, MonoBehaviour mono)
    {
        
        getHTMLCompleted = c;
        mono.StartCoroutine(MyLoadPage(uri, getHTMLCompleted));
    }
    static IEnumerator MyLoadPage(string url, EventHandlerCompleted c)
    {
        WWW www = new WWW(url);
        yield return www;
        if (www.error == null)
        {
            STR_RESULE = www.text;
            c();
        }
        else
        {
            STR_RESULE = null;
            c();
        }
    }
    


}
