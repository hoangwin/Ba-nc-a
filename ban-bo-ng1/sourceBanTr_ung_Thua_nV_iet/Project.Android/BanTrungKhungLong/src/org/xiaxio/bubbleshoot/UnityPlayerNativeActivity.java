package org.xiaxio.bubbleshoot;

import com.google.ads.AdRequest.ErrorCode;
import com.google.android.gms.ads.AdListener;
import com.google.android.gms.ads.AdRequest;
import com.google.android.gms.ads.AdSize;
import com.google.android.gms.ads.AdView;
import com.google.android.gms.ads.InterstitialAd;
//import com.startapp.android.publish.StartAppAd;
//import com.startapp.android.publish.StartAppSDK;
//import com.startapp.android.publish.banner.Banner;
import com.unity3d.player.*;

import android.app.NativeActivity;
import android.content.res.Configuration;
import android.graphics.PixelFormat;
import android.os.Bundle;
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

import com.chartboost.sdk.*;
import com.chartboost.sdk.Libraries.CBLogging.Level;
import com.chartboost.sdk.Model.CBError.CBClickError;
import com.chartboost.sdk.Model.CBError.CBImpressionError;
public class UnityPlayerNativeActivity extends NativeActivity
{
	protected UnityPlayer mUnityPlayer;		// don't change the name of this variable; referenced from native code
	public static UnityPlayerNativeActivity instance;
	private InterstitialAd interstitial;
//	private StartAppAd startAppAd = new StartAppAd(this);;
	// Setup activity layouti
	public static boolean isFistAds = true;
	@Override protected void onCreate (Bundle savedInstanceState)
	{
		requestWindowFeature(Window.FEATURE_NO_TITLE);
		super.onCreate(savedInstanceState);

	    Chartboost.startWithAppId(this, "543936a7c26ee439949058b5", "3f537efd1065fba3919a1f3fb1f1a14df2d0d421");//chartboost hoang...hotmail
		Chartboost.setLoggingLevel(Level.ALL);
		Chartboost.setDelegate(delegate);
	    /* Optional: If you want to program responses to Chartboost events, supply a delegate object here and see step (10) for more information */
	    //Chartboost.setDelegate(delegate);
	    Chartboost.onCreate(this);
		
		getWindow().takeSurface(null);
		getWindow().setFormat(PixelFormat.RGBX_8888); // <--- This makes xperia play happy

		mUnityPlayer = new UnityPlayer(this);
		if (mUnityPlayer.getSettings ().getBoolean ("hide_status_bar", true))
		{
			setTheme(android.R.style.Theme_NoTitleBar_Fullscreen);
			getWindow ().setFlags (WindowManager.LayoutParams.FLAG_FULLSCREEN,
			                       WindowManager.LayoutParams.FLAG_FULLSCREEN);
		}

	//	setContentView(mUnityPlayer);
		mUnityPlayer.requestFocus();
		//UnityPlayer.UnitySendMessage("GameObjectName1", "MethodName1", "Message to send");
		instance = this;
		layout = new FrameLayout(this);
		layout.setPadding(0, 0, 0, 0);
		showAdmobAds( this);
		instance.ShowAdmobFull();
		isFistAds = true;
		layout.addView(mUnityPlayer);
		//layout.addView(adView,adsParams);			
		//StartAppSDK.init(this, "106420618", "208256714", true);
		//startAppAd.showAd(); // show the ad
		//startAppAd.loadAd(); // load the next ad

		//StartAppAd.showSplash(this, savedInstanceState);
		setContentView(layout);
		

	}
	 
	public void ShowAdmobFull()// goi tu ben unity sang
	{
		Log.d("Admob", "MRAID InApp Ad is calling..");
		UnityPlayer.currentActivity.runOnUiThread(new Runnable() {
			@Override
			public void run() {							
				interstitial = new InterstitialAd(instance);
				interstitial.setAdUnitId("ca-app-pub-7727165943990659/3465219920");
				// Create ad request.
				AdRequest adRequest = new AdRequest.Builder().build();
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
						instance.ShowChartboost();
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
public static  int ShowAdsFull()// goi tu unity sang
{
	if(isFistAds)
	{
		isFistAds = false;
		UnityPlayerNativeActivity.ShowChartboost();
	}
	else
		instance.ShowAdmobFull();
	return 1;
}
public static  int ShowChartboost()
{
		//Chartboost.cacheInterstitial(CBLocation.LOCATION_DEFAULT);		
		Chartboost.showInterstitial(CBLocation.LOCATION_DEFAULT);
		return 1;
}	
	
	static FrameLayout layout ;
	static FrameLayout.LayoutParams adsParams = new FrameLayout.LayoutParams(FrameLayout.LayoutParams.WRAP_CONTENT, FrameLayout.LayoutParams.WRAP_CONTENT, android.view.Gravity.BOTTOM | android.view.Gravity.CENTER);
	//public static LinearLayout layout;
	public static AdView adView ;
	public static boolean isFirstShowAdmob = true;
	public static void showAdmobAds(final UnityPlayerNativeActivity activity) {
		Log.d("Admob", "MRAID InApp Ad is calling..");
		UnityPlayer.currentActivity.runOnUiThread(new Runnable() {
			@Override
			public void run() {
				adView = new AdView(UnityPlayer.currentActivity);
				adView.setAdSize(AdSize.BANNER);
				adView.setAdUnitId("ca-app-pub-7727165943990659/8036424328");
				// adView = new AdView(UnityPlayer.currentActivity,
				// AdSize.SMART_BANNER, "a1531e034cf3eee");//hcgmobilegame

				AdRequest adRequest = new AdRequest.Builder()
				// .addTestDevice(AdRequest.DEVICE_ID_EMULATOR)
				// .addTestDevice("INSERT_YOUR_HASHED_DEVICE_ID_HERE")
						.build();
				adView.setAdListener(new AdListener() {
					@Override
					public void onAdLoaded() {
						if (isFirstShowAdmob) {
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
	@Override
	public void onStart() {//tu them vo 
	    super.onStart();
	    Chartboost.onStart(this);
	}
	
	@Override protected void onDestroy ()
	{
		   if (adView != null) {
			      adView.destroy();
			    }
		mUnityPlayer.quit();
		super.onDestroy();
		Chartboost.onDestroy(this);
	}

	// Pause Unity
	@Override protected void onPause()
	{
		  if (adView != null) {
		      adView.pause();
		    }
		super.onPause();
		mUnityPlayer.pause();
		Chartboost.onPause(this);
		//startAppAd.onPause();
	}

	// Resume Unity
	@Override protected void onResume()
	{
		super.onResume();
		Chartboost.onResume(this);
		//startAppAd.onResume();
		  if (adView != null) {
		      adView.resume();
		    }
		mUnityPlayer.resume();
	}
	@Override
	public void onStop() {
	    super.onStop();
	    Chartboost.onStop(this);
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
		if (!getApplicationContext().getPackageName().equals(
				"org.xiaxio.bubbleshoot")) {
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
	private ChartboostDelegate delegate = new ChartboostDelegate() {
		@Override
		public boolean shouldRequestInterstitial(String location) {
			Log.i("Chartboost ", "SHOULD REQUEST INTERSTITIAL '"+ (location != null ? location : "null"));		
			return true;
		}
	
		@Override
		public boolean shouldDisplayInterstitial(String location) {
			Log.i("Chartboost ", "SHOULD DISPLAY INTERSTITIAL '"+ (location != null ? location : "null"));
			return true;
		}
	
		@Override
		public void didCacheInterstitial(String location) {
			Log.i("Chartboost ", "DID CACHE INTERSTITIAL '"+ (location != null ? location : "null"));
		}
	
		@Override
		public void didFailToLoadInterstitial(String location, CBImpressionError error) {
			Log.i("Chartboost ", "DID FAIL TO LOAD INTERSTITIAL '"+ (location != null ? location : "null")+ " Error: " + error.name());
		//	Toast.makeText(getApplicationContext(), "INTERSTITIAL '"+location+"' REQUEST FAILED - " + error.name(), Toast.LENGTH_SHORT).show();
		}
	
		@Override
		public void didDismissInterstitial(String location) {
			Log.i("Chartboost ", "DID DISMISS INTERSTITIAL: "+ (location != null ? location : "null"));
		}
	
		@Override
		public void didCloseInterstitial(String location) {
			Log.i("Chartboost ", "DID CLOSE INTERSTITIAL: "+ (location != null ? location : "null"));
		}
	
		@Override
		public void didClickInterstitial(String location) {
			Log.i("Chartboost ", "DID CLICK INTERSTITIAL: "+ (location != null ? location : "null"));
		}
	
		@Override
		public void didDisplayInterstitial(String location) {
			Log.i("Chartboost ", "DID DISPLAY INTERSTITIAL: " +  (location != null ? location : "null"));
		}
	
		@Override
		public boolean shouldRequestMoreApps(String location) {
			Log.i("Chartboost ", "SHOULD REQUEST MORE APPS: " +  (location != null ? location : "null"));
			return true;
		}
	
		@Override
		public boolean shouldDisplayMoreApps(String location) {
			Log.i("Chartboost ", "SHOULD DISPLAY MORE APPS: " +  (location != null ? location : "null"));
			return true;
		}
	
		@Override
		public void didFailToLoadMoreApps(String location, CBImpressionError error) {
			Log.i("Chartboost ", "DID FAIL TO LOAD MOREAPPS " +  (location != null ? location : "null")+ " Error: "+ error.name());
		//	Toast.makeText(getApplicationContext(), "MORE APPS REQUEST FAILED - " + error.name(), Toast.LENGTH_SHORT).show();
		}
	
		@Override
		public void didCacheMoreApps(String location) {
			Log.i("Chartboost ", "DID CACHE MORE APPS: " +  (location != null ? location : "null"));
		}
	
		@Override
		public void didDismissMoreApps(String location) {
			Log.i("Chartboost ", "DID DISMISS MORE APPS " +  (location != null ? location : "null"));
		}
	
		@Override
		public void didCloseMoreApps(String location) {
			Log.i("Chartboost ", "DID CLOSE MORE APPS: "+  (location != null ? location : "null"));
		}
	
		@Override
		public void didClickMoreApps(String location) {
			Log.i("Chartboost ", "DID CLICK MORE APPS: "+  (location != null ? location : "null"));
		}
	
		@Override
		public void didDisplayMoreApps(String location) {
			Log.i("Chartboost ", "DID DISPLAY MORE APPS: " +  (location != null ? location : "null"));
		}
	
		@Override
		public void didFailToRecordClick(String uri, CBClickError error) {
			Log.i("Chartboost ", "DID FAILED TO RECORD CLICK " + (uri != null ? uri : "null") + ", error: " + error.name());
		//	Toast.makeText(getApplicationContext(), "FAILED TO RECORD CLICK " + (uri != null ? uri : "null") + ", error: " + error.name(), Toast.LENGTH_SHORT).show();
		}
		
		
	};
}
