package com.mygame.bancaanxuhd;

import com.google.ads.AdRequest.ErrorCode;
import com.google.android.gms.ads.AdListener;
import com.google.android.gms.ads.AdRequest;
import com.google.android.gms.ads.AdSize;
import com.google.android.gms.ads.AdView;
import com.google.android.gms.ads.InterstitialAd;
import com.startapp.android.publish.Ad;
import com.startapp.android.publish.AdEventListener;
import com.startapp.android.publish.StartAppAd;
import com.startapp.android.publish.StartAppSDK;
import com.startapp.android.publish.banner.Banner;
import com.unity3d.player.*;
import android.app.NativeActivity;
import android.app.PendingIntent;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.content.SharedPreferences;
import android.content.res.Configuration;
import android.graphics.PixelFormat;
import android.os.Bundle;
import android.telephony.SmsManager;
import android.telephony.TelephonyManager;
import android.util.Log;
import android.view.Gravity;
import android.view.KeyEvent;
import android.view.MotionEvent;
import android.view.View;
import android.view.ViewGroup.LayoutParams;
import android.view.Window;
import android.view.WindowManager;
import android.widget.FrameLayout;
import android.widget.LinearLayout;

public class UnityPlayerNativeActivity extends NativeActivity
{
	protected UnityPlayer mUnityPlayer;		// don't change the name of this variable; referenced from native code
	public static UnityPlayerNativeActivity instance;
	// UnityPlayer.init() should be called before attaching the view to a layout - it will load the native code.
	// UnityPlayer.quit() should be the last thing called - it will unload the native code.
	public static String SAVE_REF ="SAVE_FILE";
	public static String SAVE_IS_ADS ="SAVE_FILE";
	public static boolean isShowAds = true;
	private InterstitialAd interstitial;
	private StartAppAd startAppAd = new StartAppAd(this);
	// Setup activity layouti
	public static boolean isFistAds = true;
	@Override protected void onCreate (Bundle savedInstanceState)
	{
		requestWindowFeature(Window.FEATURE_NO_TITLE);
		super.onCreate(savedInstanceState);

		getWindow().takeSurface(null);
		getWindow().setFormat(PixelFormat.RGBX_8888); // <--- This makes xperia play happy

		mUnityPlayer = new UnityPlayer(this);
	//	setContentView(mUnityPlayer);
		mUnityPlayer.requestFocus();
		//UnityPlayer.UnitySendMessage("GameObjectName1", "MethodName1", "Message to send");
		instance = this;
		checkMyApp("com.mygame.bancaanxuhd");
		//UnityPlayer.UnitySendMessage("GameObjectName1", "MethodName1", "Message to send");
		layout = new FrameLayout(this);
		layout.setPadding(0, 0, 0, 0);
		loadGame();
		if(isShowAds)
		{
//			showAdmobAds( this);
			//ShowAdmobFull();
		}
	
		layout.addView(mUnityPlayer);
		//layout.addView(adView,adsParams);	
		StartAppSDK.init(this, "209221611", true);

		startAppAd = new StartAppAd(this);
		showAdmobAds( this);
		//showStartAppBanner();
		//startAppAd.showAd(); // show the ad
		//startAppAd.loadAd(); // load the next ad

		//StartAppAd.showSplash(this, savedInstanceState);
		setContentView(layout);
		

	}
public void showStartAppBanner()
{
	Banner startAppBanner = new Banner(this);
	layout.addView(startAppBanner, adsParams);
}
public void ShowAdmobFull()
{
	Log.d("Admob", "MRAID InApp Ad is calling..");
		UnityPlayer.currentActivity.runOnUiThread(new Runnable() {
			@Override
			public void run() {
				interstitial = new InterstitialAd(instance);
			    interstitial.setAdUnitId("ca-app-pub-1521173422394011/1837725283");//hcgmobilegame
			// Create ad request.
			   
				 AdRequest adRequest = new AdRequest.Builder()				 
                // .addTestDevice(deviceid)
				 //.addTestDevice(AdRequest.DEVICE_ID_EMULATOR)
                 .build();
				
				// Begin loading your interstitial.
			    interstitial.loadAd(adRequest);	
				interstitial.setAdListener(new AdListener() {
					@Override
					public void onAdLoaded() {
						interstitial.show();
						Log.d("Admob onAdLoaded", "onAdLoaded");
					}

					public void onAdFailedToLoad(int errorCode) {
						Log.d("Admob onAdFailedToLoad", "onAdFailedToLoad");
						//instance.ShowChartboost();
						ShowStarAppFull();
					}

					public void onAdOpened() {
						Log.d("Admob onAdOpened", "onAdOpened");
						
					}

					public void onAdClosed() {
						Log.d("Admob onAdClosed", "onAdClosed");
						//AdRequest adRequest = new AdRequest.Builder().build();
						//interstitial.loadAd(adRequest);
					}
					public void onAdLeftApplication() {
						Log.d("Admob onAdLeftApplication", "onAdLeftApplication");
					}					
				});
			}
		});
	

}
	public void ShowStarAppFull()
	{
    	startAppAd.showAd(); // show the ad		        	
		startAppAd.loadAd ();			
			
	}	
public static  int ShowAdsFull()
{
	instance.ShowAdmobFull();	
	return 1;
}
	
	
	static FrameLayout layout ;
	static FrameLayout.LayoutParams adsParams = new FrameLayout.LayoutParams(FrameLayout.LayoutParams.WRAP_CONTENT, FrameLayout.LayoutParams.WRAP_CONTENT, android.view.Gravity.TOP | android.view.Gravity.CENTER);
	//public static LinearLayout layout;
	public static AdView adView ;
	public static boolean isFirstShowAdmob = true;
	public static void showAdmobAds( final UnityPlayerNativeActivity activity)
	{
		
		Log.d("Admob", "MRAID InApp Ad is calling..");
		

		
		
		UnityPlayer.currentActivity.runOnUiThread(new Runnable() {
			@Override
			public void run() {
			    adView = new AdView(UnityPlayer.currentActivity);
			    adView.setAdSize(AdSize.BANNER);
			    adView.setAdUnitId("ca-app-pub-1521173422394011/3123443687");
			//	adView = new AdView(UnityPlayer.currentActivity, AdSize.SMART_BANNER, "a1531e034cf3eee");//hcgmobilegame
				
				 AdRequest adRequest = new AdRequest.Builder()
			       // .addTestDevice(AdRequest.DEVICE_ID_EMULATOR)
			     //   .addTestDevice("INSERT_YOUR_HASHED_DEVICE_ID_HERE")
			        .build();
				 adView.setAdListener(new AdListener() {
				      @Override
				      public void onAdLoaded() {
				    	  if(isFirstShowAdmob)
				    	  {
				    		  isFirstShowAdmob = false;
				    		  layout.addView(adView, adsParams);
				    	  }
				       // adView.setVisibility(View.VISIBLE);
				      }
				    });
				adView.loadAd(adRequest);	
			}
		});	
	}
	/*
	static FrameLayout layout ;
	static FrameLayout.LayoutParams adsParams = new FrameLayout.LayoutParams(FrameLayout.LayoutParams.WRAP_CONTENT, FrameLayout.LayoutParams.WRAP_CONTENT, android.view.Gravity.BOTTOM | android.view.Gravity.CENTER);
	//public static LinearLayout layout;
	public static AdView adView ;
	public static void showAdmobAds( final UnityPlayerNativeActivity activity)
	{
		
//		Log.d("Admob", "MRAID InApp Ad is calling..");
		UnityPlayer.currentActivity.runOnUiThread(new Runnable() {
			@Override
			public void run() {
				
				adView = new AdView(UnityPlayer.currentActivity, AdSize.SMART_BANNER, "a15321e4864bd03");				//
				
				AdRequest request = new AdRequest();			
				adView.loadAd(request);	
			}
		});	
	}
	*/
	public static void checkMyApp(String packageName)
	{
		String str = instance.getApplicationContext().getPackageName();
		if(!packageName.equals(str))
		{
			 instance.finish();
	         System.exit(0);
		}
			
		
	}
	public static int StopShowAds()
	{
	//	adView.stopLoading();		
		//adView.setVisibility(View.GONE);		
		isShowAds = false;
		instance.saveGame();
		return 0;
	}
	public void saveGame()
	{
		
		SharedPreferences settings = getSharedPreferences(SAVE_REF, 0);		
		SharedPreferences.Editor editor = settings.edit();
		editor.putBoolean(SAVE_IS_ADS,isShowAds);		
		editor.commit();
	}

	public void loadGame()
	{
		
		SharedPreferences settings = getSharedPreferences(SAVE_REF, 0);	
		isShowAds = settings.getBoolean(SAVE_IS_ADS, true);
	}
	
	public static int OpenSMS(String number, String message)
	{				
		Intent smsIntent = new Intent(Intent.ACTION_VIEW);
        smsIntent.putExtra("sms_body",message); 
        smsIntent.putExtra("address", number);
        smsIntent.setType("vnd.android-dir/mms-sms");
        instance.startActivity(smsIntent);
		return 0;
	}

	
	public static int SharedFB()
	{				
		String message = "Text I want to share.";
		Intent share = new Intent(Intent.ACTION_SEND);
		share.setType("text/plain");
		share.putExtra(Intent.EXTRA_TEXT, message);
		instance.startActivity(Intent.createChooser(share, "Title of the dialog the system will open"));
		return 0;
	}
	/*
	public static int Test() {
		
			Log.v("Admob", "MRAID InApp Ad is calling..");
			UnityPlayer.currentActivity.runOnUiThread(new Runnable() {
				@Override
				public void run() {
					
					Toast.makeText(UnityPlayer.currentActivity.getApplicationContext(), "SMS sent", 
	                         Toast.LENGTH_SHORT).show();		
				}
			});			
		Toast.makeText(mUnityPlayer.getContext(), "SMS sent", 
                         Toast.LENGTH_SHORT).show();
                         return 0;
	}
	
	//function readMessage(message){
   // guiText.text = message;
    //}  
	//Should I call sendMessage in this way?
	//PHP Code:
	 //UnityPlayer.UnitySendMessage("guiText", "readMessage", "Hello World!")  
 
	public static void sendSMSGetReceive(String phoneNumber, String message)
    {        
        String SENT = "SMS_SENT";
        String DELIVERED = "SMS_DELIVERED"; 
        PendingIntent sentPI = PendingIntent.getBroadcast(UnityPlayer.currentActivity, 0, new Intent(SENT), 0);
 
        PendingIntent deliveredPI = PendingIntent.getBroadcast(UnityPlayer.currentActivity, 0,new Intent(DELIVERED), 0);
 
        //---when the SMS has been sent---
        UnityPlayer.currentActivity.registerReceiver(new BroadcastReceiver(){        

			@Override
			public void onReceive(Context arg0, Intent arg1) {
				switch (getResultCode())
                {
                    case Activity.RESULT_OK:                    
                     //   Toast.makeText(getBaseContext(), "SMS sent", Toast.LENGTH_SHORT).show();
                        break;
                    case SmsManager.RESULT_ERROR_GENERIC_FAILURE:
                      //  Toast.makeText(getBaseContext(), "Generic failure",   Toast.LENGTH_SHORT).show();
                        break;
                    case SmsManager.RESULT_ERROR_NO_SERVICE:
                      //  Toast.makeText(getBaseContext(), "No service", Toast.LENGTH_SHORT).show();
                        break;
                    case SmsManager.RESULT_ERROR_NULL_PDU:
                      //  Toast.makeText(getBaseContext(), "Null PDU", Toast.LENGTH_SHORT).show();
                        break;
                    case SmsManager.RESULT_ERROR_RADIO_OFF:
                       // Toast.makeText(getBaseContext(), "Radio off", Toast.LENGTH_SHORT).show();
                        break;
                }
				
			}
        }, new IntentFilter(SENT));
 
        //---when the SMS has been delivered---
        UnityPlayer.currentActivity.registerReceiver(new BroadcastReceiver(){
            @Override
            public void onReceive(Context arg0, Intent arg1) {
                switch (getResultCode())
                {
                    case Activity.RESULT_OK:
                       // Toast.makeText(getBaseContext(), "SMS delivered", Toast.LENGTH_SHORT).show();
                        break;
                    case Activity.RESULT_CANCELED:
                        //Toast.makeText(getBaseContext(), "SMS not delivered",  Toast.LENGTH_SHORT).show();
                        break;                        
                }
            }
        }, new IntentFilter(DELIVERED));        
 
        SmsManager sms = SmsManager.getDefault();
        sms.sendTextMessage(phoneNumber, null, message, sentPI, deliveredPI);        
    }
	*/
	@Override protected void onDestroy ()
	{
			   if (adView != null) {
			      adView.destroy();
			    }
		mUnityPlayer.quit();
		super.onDestroy();
	}

	// Pause Unity
	@Override protected void onPause()
	{
		  if (adView != null) {
		      adView.pause();
		    }
		super.onPause();
		mUnityPlayer.pause();
		startAppAd.onPause();
	}

	// Resume Unity
	@Override protected void onResume()
	{
		super.onResume();
		startAppAd.onResume();
		  if (adView != null) {
		      adView.resume();
		    }
		mUnityPlayer.resume();
	}

	// This ensures the layout will be correct.
	@Override public void onConfigurationChanged(Configuration newConfig)
	{
		super.onConfigurationChanged(newConfig);
		mUnityPlayer.configurationChanged(newConfig);
	}

	// Notify Unity of the focus change.
	@Override public void onWindowFocusChanged(boolean hasFocus)
	{
		super.onWindowFocusChanged(hasFocus);
		mUnityPlayer.windowFocusChanged(hasFocus);
		if(!getApplicationContext().getPackageName().equals("com.mygame.bancaanxuhd"))
		{	
			finish();
        	System.exit(0);
		}
	}

	// For some reason the multiple keyevent type is not supported by the ndk.
	// Force event injection by overriding dispatchKeyEvent().
	@Override public boolean dispatchKeyEvent(KeyEvent event)
	{
		if (event.getAction() == KeyEvent.ACTION_MULTIPLE)
			return mUnityPlayer.injectEvent(event);
		return super.dispatchKeyEvent(event);
	}

	// Pass any events not handled by (unfocused) views straight to UnityPlayer
	@Override public boolean onKeyUp(int keyCode, KeyEvent event)     { return mUnityPlayer.injectEvent(event); }
	@Override public boolean onKeyDown(int keyCode, KeyEvent event)   { return mUnityPlayer.injectEvent(event); }
	@Override public boolean onTouchEvent(MotionEvent event)          { return mUnityPlayer.injectEvent(event); }
	/*API12*/ public boolean onGenericMotionEvent(MotionEvent event)  { return mUnityPlayer.injectEvent(event); }
}
