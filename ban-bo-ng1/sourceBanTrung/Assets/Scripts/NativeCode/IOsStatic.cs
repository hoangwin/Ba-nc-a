using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
public class IOsStatic : MonoBehaviour {

	// Use this for initialization
    [DllImport("__Internal")]
    private static extern float NaticeStopAds(string phoneNumber, string BodyText);
    [DllImport("__Internal")]
    private static extern float NaticeShowAdsBanner(string phoneNumber, string BodyText);

    [DllImport("__Internal")]
    private static extern float NaticeShowAdsFull(string phoneNumber, string BodyText);
    public static void StopAds(string str1, string str2)
    {
        NaticeStopAds(str1, str2);
    }

    public static void ShowAds(string str1, string str2)
    {
        NaticeShowAdsBanner(str1, str2);
    }

    public static void ShowAdsFull(string str1, string str2)
    {
        NaticeShowAdsFull(str1, str2);
    }

}
