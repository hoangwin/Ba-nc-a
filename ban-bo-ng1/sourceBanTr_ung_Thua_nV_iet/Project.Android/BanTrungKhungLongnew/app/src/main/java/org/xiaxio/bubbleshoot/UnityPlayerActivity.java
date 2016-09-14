package org.xiaxio.bubbleshoot;

import com.google.android.gms.ads.AdListener;
import com.google.android.gms.ads.AdRequest;
import com.google.android.gms.ads.AdSize;
import com.google.android.gms.ads.AdView;
import com.google.android.gms.ads.InterstitialAd;
import com.google.android.gms.ads.MobileAds;
import com.unity3d.player.*;
import android.app.Activity;
import android.content.res.Configuration;
import android.graphics.PixelFormat;
import android.os.Bundle;
import android.util.Log;
import android.view.KeyEvent;
import android.view.MotionEvent;
import android.view.View;
import android.view.Window;
import android.view.WindowManager;
import android.widget.FrameLayout;

public class UnityPlayerActivity extends Activity
{
	public static UnityPlayerActivity instance;
	protected UnityPlayer mUnityPlayer; // don't change the name of this variable; referenced from native code
	static FrameLayout layout ;
	public static AdView adView ;
	private InterstitialAd interstitial;
	static FrameLayout.LayoutParams adsParams = new FrameLayout.LayoutParams(FrameLayout.LayoutParams.WRAP_CONTENT, FrameLayout.LayoutParams.WRAP_CONTENT, android.view.Gravity.BOTTOM | android.view.Gravity.CENTER);
	// Setup activity layout
	@Override protected void onCreate (Bundle savedInstanceState)
	{
		requestWindowFeature(Window.FEATURE_NO_TITLE);
		super.onCreate(savedInstanceState);

		getWindow().setFormat(PixelFormat.RGBX_8888); // <--- This makes xperia play happy

		mUnityPlayer = new UnityPlayer(this);
		instance = this;
		layout = new FrameLayout(this);
		layout.setPadding(0, 0, 0, 0);

			//instance.ShowAdmobFull();
		layout.addView(mUnityPlayer);

		showAdmobAds( this);
		//showStartAppBanner();


		setContentView(layout);

		//setContentView(mUnityPlayer);
		//showAdmobAds(this);
		mUnityPlayer.requestFocus();


//

//
		//AdView mAdView = (AdView) findViewById(R.id.adView);
	//	AdRequest adRequest = new AdRequest.Builder().build();
//		mAdView.loadAd(adRequest);

	}
	public static  int ShowAdsFull()// goi tu unity sang
	{

		//UnityPlayerNativeActivity.ShowChartboost();
		instance.ShowAdmobFull();
		return 1;
	}
	public static boolean isFirstShowAdmob = true;
	public static void showAdmobAds(final UnityPlayerActivity activity) {
		Log.d("Admob", "MRAID InApp Ad is calling..");
		UnityPlayer.currentActivity.runOnUiThread(new Runnable() {
			@Override
			public void run() {
				MobileAds.initialize(activity.getApplicationContext());
				adView = new AdView(UnityPlayer.currentActivity);
				adView.setAdSize(AdSize.BANNER);
				adView.setAdUnitId("ca-app-pub-3940256099942544/6300978111");
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
					@Override
					public void onAdFailedToLoad(int errorCode){
					//	instance.showStartAppBanner();
						adView.destroy();
						adView.setVisibility(View.GONE);
					}
					@Override
					public void onAdOpened(){

					}
					@Override
					public void onAdClosed(){

					}
					@Override
					public void onAdLeftApplication(){

					}
				});
				adView.loadAd(adRequest);
			}
		});
	}

	public void ShowAdmobFull()// goi tu ben unity sang
	{
		Log.d("Admob", "MRAID InApp Ad is calling..");
		UnityPlayer.currentActivity.runOnUiThread(new Runnable() {
			@Override
			public void run() {
				interstitial = new InterstitialAd(instance);
				interstitial.setAdUnitId("ca-app-pub-3940256099942544/1033173712");
				// Create ad request.
				AdRequest adRequest = new AdRequest.Builder().build();
				// Begin loading your interstitial.
				interstitial.loadAd(adRequest);

				interstitial.setAdListener(new AdListener() {
					@Override
					public void onAdLoaded() {
						if (interstitial.isLoaded()) {
							interstitial.show();
						}
						Log.d("Admob onAdLoaded", "onAdLoaded");
					}

					public void onAdFailedToLoad(int errorCode) {
						Log.d("Admob onAdFailedToLoad", "onAdFailedToLoad");
						//instance.ShowChartboost();
					//	loadInterstitialAdFaceBook(instance);
						//ShowStarAppFull();
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
						Log.d("Admob onAdLeftAppli", "onAdLeftApplication");
					}
				});
			}
		});

	}
	// Quit Unity
	@Override protected void onDestroy ()
	{
		mUnityPlayer.quit();
		if (adView != null) {
			adView.resume();
		}
		super.onDestroy();
	}

	// Pause Unity
	@Override protected void onPause()
	{
		super.onPause();
		if (adView != null) {
			adView.pause();
		}
		mUnityPlayer.pause();
	}

	// Resume Unity
	@Override protected void onResume()
	{
		super.onResume();
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
