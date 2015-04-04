using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;
public class NAPCARD : MonoBehaviour 
{
	//public UILabel nhamang;
	public UILabel l1_mathe;
	public UILabel l2_seri;
	public UIPopupList l0_popup;
    private WWW httpRequestCard;
    private float elapsedTime = 0.0f;
    public bool isRequesting = false;
    private float waitTime = 10f;//second
    public delegate void CallbackFunction(string message);
	void Start ()
	{

	}

	public void Init()
	{

	}

	public void ClickOK()
	{

        if (l1_mathe.text.IndexOf(" ") >= 0
                   || string.IsNullOrEmpty(l1_mathe.text)
                   || l2_seri.text.IndexOf(" ") >= 0
                   || string.IsNullOrEmpty(l2_seri.text))
        {
            AddCoinControl.instance.LabelAdcoinNotive.text = "Nhap sai: Khong dung dinh dang";
            return;
        }

		string nhamang_ = "VTE";
        if (AddCoinControl.networkType == 1) nhamang_ = "VNP";
        else if (AddCoinControl.networkType == 2) nhamang_ = "VMS";
        StartCoroutine(sendRequestBuy(nhamang_, l1_mathe.text, l2_seri.text, onRequestBuyDone));
       

	}
    public IEnumerator sendRequestBuy(string telcoCode, string seriNumber, string cardNumber, CallbackFunction callback)
    {
        Debug.Log(telcoCode + " " + seriNumber + " " + cardNumber);

        elapsedTime = 0.0f;
        isRequesting = true;
      //  PopupManager.Loading(gameObject, Language.performing);
        string url = "http://115.84.179.60:2222/WebServiceReceiveMO.asmx";
        httpRequestCard = createRequest(url, telcoCode, seriNumber, cardNumber);

        yield return httpRequestCard;

        if (httpRequestCard != null && string.IsNullOrEmpty(httpRequestCard.error))
        {
            Debug.Log(httpRequestCard.text);
            callback(httpRequestCard.text);
        }
        else
        {
            Debug.Log("Request fail");
            //fireEventConnectFail();
        }

        isRequesting = false;
    }

    private WWW createRequest(string url, string telcoCode, string seriNumber, string cardNumber)
    {
        string envelope =
            "<?xml version=" + '\"' + "1.0" + '\"' + " encoding=" + '\"' + "utf-8" + '\"' + "?>" +
                "<soap:Envelope xmlns:soap=" + '\"' + "http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'>" +
                "<soap:Body>" +
                "<NAPTHE xmlns=" + '\"' + "http://tempuri.org/" + '\"' + ">"
                + "<TenKenh>" + telcoCode + "</TenKenh>"
                + "<MaThe>" + cardNumber + "</MaThe>"
                + "<SeRi>" + seriNumber + "</SeRi>" +
                "<UserName>" + ScoreControl._UDID + "</UserName>" +
                "<Password>" + "TOAN_STT" + "</Password>" +
                "</NAPTHE>" +
                "</soap:Body>" +
                "</soap:Envelope>";
        Hashtable headers = new Hashtable();
        headers["Content-Type"] = "text/xml;";
        WWW www = null;
#if UNITY_ANDROID
        www = new WWW(url, System.Text.Encoding.UTF8.GetBytes(envelope), headers);
#elif UNITY_WP8
        System.Collections.Generic.Dictionary<string, string> theader = new System.Collections.Generic.Dictionary<string, string>();
        theader.Add("Content-Type", "text/xml;");
        www = new WWW(url, System.Text.Encoding.UTF8.GetBytes(envelope), theader);
#elif UNITY_IPHONE
        www = new WWW(url, System.Text.Encoding.UTF8.GetBytes(envelope), headers);
#endif
        return www;
    }

	
    public void onRequestBuyDone(string data)
    {
        //PopupManager.CloseLoading();
        if (data != null)
        {
            //Check info response
            int parValue = processDataFromServer(data);
            //Convert to candy value
            Debug.Log(parValue);
            int candyValue = 0;
            switch (parValue)
            {
                case 500000: candyValue = 210000; break;//200%
                case 200000: candyValue = 81000; break;//140%
                case 100000: candyValue = 39000; break; //130%
                case 50000: candyValue = 18000; break;  //120%
                case 20000: candyValue = 6900; break;  //110%
                case 10000: candyValue = 3400; break;   //100%
                case -1: candyValue = 0; break;
                default: candyValue = 10000; break;
            }

            if (candyValue > 0)
            {
                WWW www1 = new WWW("http://gamethuanviet.com/ban_ca_an_xu/UpdateCard.php?username=" + ScoreControl._UDID + "&card=" + parValue.ToString());//here
                ScoreControl.addCoind(candyValue);
                ScoreControl.saveGame();
                Bonus.instance.playAnimAddCoinCompleted();
                if (ButtonControl.state == 0)
                {
                    MainMenu.instance.LabelCoin.text = ScoreControl._Coin.ToString();

                }
                else
                    GamePlay.instance.LabelCoin.text = ScoreControl._Coin.ToString();
                //TI.DA_NAP_CARD.SetAndSave(1);
                //WARNING_Manager.ON_WARNING_LOW = false;
                //WARNING_Manager.AddWarningLowForce("Thành công!","bạn vừa nạp "+gem_add +" Xu vào tài khoản "+TI.NAME.STR);
                //TIG.XU.PlusAndSave(gem_add);
                //MainMenu.instance.UpdateLabels();
                AddCoinControl.instance.LabelAdcoinNotive.text = "Bạn vừa nạp " + candyValue + " Coin thành công";
                Debug.Log("thanh cong: " + "bạn vừa nạp " + candyValue + " Coin vào tài khoản ");
#if UNITY_ANDROID
                        AddCoinControl.stopADS();
#elif UNITY_WP8
                            
                         WP8Statics.StopAds("");
#endif
            }
            else
            {
               
            }
        }
        else
        {
          //  PopupManager.Warning(gameObject, Language.alert, Language.cannotConnect);
        }
    }
    public int processDataFromServer(string data)
    {
        string message = "Khong thanh cong$0$0";
        int start = data.IndexOf("message:") + 8;
        int end = data.IndexOf("</NAPTHEResult>");
        if (end > start)
        {
            message = data.Substring(start, end - start);
            Debug.Log("mess= " + message);
            int end_mess = message.IndexOf('$');
            if (message.IndexOf("500000") >= 0) return 500000;
            else if (message.IndexOf("200000") >= 0) return 200000;
            else if (message.IndexOf("100000") >= 0) return 100000;
            else if (message.IndexOf("50000") >= 0) return 50000;
            else if (message.IndexOf("20000") >= 0) return 20000;
            else if (message.IndexOf("10000") >= 0) return 10000;
            else
            {
                if (end_mess > 0)
                {
                   // PopupManager.Warning(gameObject, Language.noSuccess, message.Substring(0, end_mess));
                    Debug.Log("ko thanh cong:");
                    AddCoinControl.instance.LabelAdcoinNotive.text = "Message :" + message.Substring(0, end_mess);
                }
                return -1;
            }
        }
        return -1;
    }
   
}
