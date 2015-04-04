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


	void Start ()
	{

	}

	public void Init()
	{

	}

	public void ClickOK()
	{
        //if (WARNING_Manager.IsWarningHigh ())return;
        //if (TI.NAME.STR == "noname") 
        //{

        //    WARNING_Manager.AddWarningHighForce("Thông Báo","bạn phải đổi tên trước khi nạp thẻ, hãy thoát hết game sau đó mở lại để nhập tên");
        //    return;
        //}
        //if (l1_mathe.text.IndexOf(" ")>=0|| l2_seri.text.IndexOf(" ")>=0) 
        //{
        //    Debug.Log("1231242353463467");
        //    WARNING_Manager.AddWarningHighForce("Thông Báo","Yêu cầu nhập đúng định dạng");
        //    return;
        //}

		string nhamang_ = "VTE";
        if (AddCoinControl.networkType == 1) nhamang_ = "VNP";
        else if (AddCoinControl.networkType == 2) nhamang_ = "VMS";

        Debug.Log("Nha Mang:" + nhamang_);

		int c = Nap (nhamang_,l1_mathe.text,l2_seri.text);
		int gem_add = c;
		switch (c)
		{

			case 10000: gem_add = 3400; break;
			case 20000: gem_add = 6900; break;
			case 50000: gem_add = 18000; break;
			case 100000: gem_add = 39000; break;
			case 200000: gem_add = 81000; break;
            case 500000:gem_add = 210000; break;
			case -1: gem_add = 0; break;
			default: gem_add = 1000;break;
		}

		if (gem_add > 0) 
		{
            WWW www1 = new WWW("http://gamethuanviet.com/ban_ca_an_xu/UpdateCard.php?username=" + ScoreControl._UDID + "&card=" + c.ToString());//here
            ScoreControl.addCoind(gem_add);           
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
            AddCoinControl.instance.LabelAdcoinNotive.text = "Bạn vừa nạp " + gem_add + " Coin thành công";
            Debug.Log("thanh cong: " + "bạn vừa nạp " + gem_add + " Coin vào tài khoản ");
#if UNITY_ANDROID
                        //ButtonAddCoin.stopADS();
#elif UNITY_WP8
                            
                         WP8Statics.StopAds("");
#endif
                       

		}
       
           



	}
	
	public delegate void Callback( string data, string error );
	int Nap(string nhamang_,string mathe,string seri)
	{
		string url = "http://115.84.179.60:2222/WebServiceReceiveMO.asmx";  
		WWW www = CreateRequest (url,nhamang_,mathe,seri);
		while (!www.isDone)  //wait until www isdone
			;
		Debug.Log (www.text);

		string MESS = "Khong thanh cong$0$0";
		int start = www.text.IndexOf ("message:")+8;
		int end = www.text.IndexOf ("</NAPTHEResult>");
		if (end > start) 
		{

			MESS = www.text.Substring (start, end - start);
			Debug.Log("mess= "+MESS);
			int end_mess = MESS.IndexOf('$');
			if(MESS.IndexOf("500000")>=0) return 500000;
			else  if(MESS.IndexOf("200000")>=0) return 200000;
			else  if(MESS.IndexOf("100000")>=0) return 100000;
			else  if(MESS.IndexOf("50000")>=0) return 50000;
			else  if(MESS.IndexOf("20000")>=0) return 20000;
			else  if(MESS.IndexOf("10000")>=0) return 10000;
			else 
			{
				if(end_mess>0)
				{
					//WARNING_Manager.AddWarningHigh("Không thành công!",MESS.Substring(0,end_mess));
                    Debug.Log("ko thanh cong: " + MESS.Substring(0, end_mess));
                    AddCoinControl.instance.LabelAdcoinNotive.text = "Message : " + MESS.Substring(0, end_mess);
				}
				return -1;
			}
		}
		return -1;
	}
	private IEnumerator IEWaitForData(WWW www, float stop, Callback callback)
	{ 
		yield return www;
		Debug.Log (www.text);

	} // sendData 


	private WWW CreateRequest(string url,string nhamang_,string mathe,string seri) 
	{ 
		//return null;
		string envelope =
			"<?xml version="+'\"'+"1.0"+'\"'+" encoding="+'\"'+"utf-8"+'\"'+"?>"+
				"<soap:Envelope xmlns:soap="+'\"'+"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'>"+
				"<soap:Body>"+
				"<NAPTHE xmlns="+'\"'+"http://tempuri.org/"+'\"'+">"
				+"<TenKenh>"+nhamang_+"</TenKenh>"
				+"<MaThe>"+mathe+"</MaThe>"
				+"<SeRi>"+seri+"</SeRi>"+
                "<UserName>" + ScoreControl._UDID + "</UserName>" +
				"<Password>"+"TOAN_STT"+"</Password>"+
				"</NAPTHE>"+
				"</soap:Body>"+
				"</soap:Envelope>";

        Debug.Log(envelope);
		Hashtable headers = new Hashtable();
		headers["Content-Type"] = "text/xml;"; 
		WWW www=null;
#if UNITY_ANDROID
		www =  new WWW (url, System.Text.Encoding.UTF8.GetBytes(envelope), headers);        
#elif UNITY_WP8
		System.Collections.Generic.Dictionary<string,string> theader = new System.Collections.Generic.Dictionary<string,string>();
		theader.Add("Content-Type","text/xml;");
		www = new WWW(url,System.Text.Encoding.UTF8.GetBytes(envelope),theader);
		//www =  new WWW (url, System.Text.Encoding.UTF8.GetBytes(envelope), HashtableToDictionary<string, string>(headers)); 
#elif UNITY_IPHONE
		www =  new WWW (url, System.Text.Encoding.UTF8.GetBytes(envelope), headers);        
#endif
		return www;
		
	}

   
}
