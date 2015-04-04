using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
public class IOsStatic : MonoBehaviour {

	// Use this for initialization
    [DllImport("__Internal")]
    private static extern float NaticeOpenSMS(string phoneNumber, string BodyText);
    public static void OpenSMS(string phoneNumber, string BodyText)
    {
        NaticeOpenSMS(phoneNumber, BodyText);
    }

}
