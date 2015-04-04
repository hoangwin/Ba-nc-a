package com.banbong.bantrung.bantrungkhunglong;

import com.google.android.gms.ads.AdListener;
import com.google.android.gms.ads.AdRequest;
import com.google.android.gms.ads.AdSize;
import com.google.android.gms.ads.AdView;
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

public class UnityPlayerNativeActivity extends NativeActivity
{
	protected UnityPlayer mUnityPlayer;		// don't change the name of this variable; referenced from native code

	// Setup activity layout
	@Override protected void onCreate (Bundle savedInstanceState)
	{
		requestWindowFeature(Window.FEATURE_NO_TITLE);
		super.onCreate(savedInstanceState);

		getWindow().takeSurface(null);
		setTheme(android.R.style.Theme_NoTitleBar_Fullscreen);
		getWindow().setFormat(PixelFormat.RGB_565);

		mUnityPlayer = new UnityPlayer(this);
		if (mUnityPlayer.getSettings ().getBoolean ("hide_status_bar", true))
			getWindow ().setFlags (WindowManager.LayoutParams.FLAG_FULLSCREEN,
			                       WindowManager.LayoutParams.FLAG_FULLSCREEN);

		int glesMode = mUnityPlayer.getSettings().getInt("gles_mode", 1);
		boolean trueColor8888 = false;
		mUnityPlayer.init(glesMode, trueColor8888);
		View playerView = mUnityPlayer.getView();
	//	setContentView(playerView);
		playerView.requestFocus();
		
		layout = new FrameLayout(this);
		layout.setPadding(0, 0, 0, 0);
		showAdmobAds( this);
	
		layout.addView(playerView);
					
		setContentView(layout);		

	}
	
	static FrameLayout layout ;
	static FrameLayout.LayoutParams adsParams = new FrameLayout.LayoutParams(FrameLayout.LayoutParams.WRAP_CONTENT, FrameLayout.LayoutParams.WRAP_CONTENT, android.view.Gravity.TOP | android.view.Gravity.CENTER);
	//public static LinearLayout layout;
	public static AdView adView ;
	public static boolean isloadAds = false;
	
	public static void showAdmobAds( final UnityPlayerNativeActivity activity)
	{
		
		Log.v("Admob", "MRAID InApp Ad is calling..");
		UnityPlayer.currentActivity.runOnUiThread(new Runnable() {
			@Override
			public void run() {
			    adView = new AdView(UnityPlayer.currentActivity);
			    adView.setAdSize(AdSize.BANNER);
			    adView.setAdUnitId("a1531e034cf3eee");
			    
			//	adView = new AdView(UnityPlayer.currentActivity, AdSize.SMART_BANNER, "a1531e034cf3eee");//hcgmobilegame
				
				 AdRequest adRequest = new AdRequest.Builder()
			       // .addTestDevice(AdRequest.DEVICE_ID_EMULATOR)
			      //  .addTestDevice("INSERT_YOUR_HASHED_DEVICE_ID_HERE")
			        .build();
				 adView.setAdListener(new AdListener() {
					  @Override
					  public void onAdLoaded() {
						  if(isloadAds == false)
						  {
							  layout.addView(adView,adsParams);
							  isloadAds = true;
						  }
					  }
					});
				adView.loadAd(adRequest);	
			}
		});	
	}
	
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
		if(!getApplicationContext().getPackageName().equals("com.banbong.bantrung.bantrungkhunglong"))
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
