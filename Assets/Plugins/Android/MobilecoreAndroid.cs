using UnityEngine;
using System.Collections;

namespace UnityAndroid
{
	public class MobilecoreAndroid
	{			
		public const string INTERSTITIAL_BACK = "INTERSTITIAL_BACK";
		public const string INTERSTITIAL_NO_CONNECTION = "INTERSTITIAL_NO_CONNECTION";
		public const string INTERSTITIAL_QUIT = "INTERSTITIAL_QUIT";
		public const string INTERSTITIAL_NOT_READY = "INTERSTITIAL_NOT_READY";
		public const string INTERSTITIAL_ALREADY_SHOWING = "INTERSTITIAL_ALREADY_SHOWING";
		public const string INTERSTITIAL_SHOW_ERROR = "INTERSTITIAL_SHOW_ERROR";

		public const int LOGTYPE_DEBUG = 0;
		public const int LOGTYPE_PROD = 1;

		public const string ALL_UNITS = "ALL_UNITS";
		public const string INTERSTITIAL = "INTERSTITIAL";
		public const string STICKEEZ = "STICKEEZ";
		public const string DIRECT_TO_MARKET = "DIRECT_TO_MARKET";
		
		public const string AD_UNIT_READY = "AD_UNIT_READY";
		public const string AD_UNIT_NOT_READY = "AD_UNIT_NOT_READY";
		public const string AD_UNIT_DISMISSED = "AD_UNIT_DISMISSED";

		public const int STICKEEZ_POSITION_TOP_LEFT = 10;
		public const int STICKEEZ_POSITION_TOP_RIGHT = 11;
		public const int STICKEEZ_POSITION_MIDDLE_LEFT = 12;
		public const int STICKEEZ_POSITION_MIDDLE_RIGHT = 13;
		public const int STICKEEZ_POSITION_BOTTOM_LEFT = 14;
		public const int STICKEEZ_POSITION_BOTTOM_RIGHT = 15;

		AndroidJavaObject jo;
	
		public MobilecoreAndroid (string devHash, int logType, GameObject gameObject, params string[] adUnits)
		{
			jo = new AndroidJavaObject("com.mobilecore.plugin.unity.light.MobilecoreLight");	
			jo.Call ("init", devHash, logType, adUnits);
			if (gameObject != null) {
				jo.Call ("setGameObjectForCallback", gameObject.name);
			}
		}
		
		public void ShowInterstitial ()
		{
			jo.Call ("showInterstitial");
		}
	
	//	public void ShowInterstitial (bool forceShow)
	//	{
	//		jo.Call ("showInterstitial", forceShow);
	//	}
		
		public void OpenUrl (string url, bool inside)
		{
			jo.Call ("openUrl", url, inside);
		}
		
		public void SetInterstitialReadyListener ()
		{
			jo.Call ("setInterstitialReadyListener");
		}	
		
		public bool isInterstitialReady ()
		{
			return jo.Call<bool>("isInterstitialReady");
		}


		// Direct To Market

		public void DirectToMarket ()
		{
			jo.Call ("directToMarket");
		}

		public void SetDirectToMarketReadyListener ()
		{
			jo.Call ("setDirectToMarketReadyListener");
		}
		
		public bool IsDirectToMarketReady ()
		{
			return jo.Call<bool>("isDirectToMarketReady");
		}

		// Stickeez
		
		public void SetStickeezReadyListener ()
		{
			jo.Call ("setStickeezReadyListener");
		}
		
		public void ShowStickee ()
		{
			jo.Call ("showStickee");
		}
		
		public void HideStickee ()
		{
			jo.Call ("hideStickee");
		}
		
		public bool IsStickeeReady ()
		{
			return jo.Call<bool>("isStickeeReady");
		}
		
		public bool IsStickeeShowing ()
		{
			return jo.Call<bool>("isStickeeShowing");
		}
		
		public bool IsStickeeShowingOffers ()
		{
			return jo.Call<bool>("isStickeeShowingOffers");
		}

		public void SetStickeezPosition(int position) 
		{
			jo.Call ("setStickeezPosition", position);
		}

		public bool OnBackPressed() 
		{
			return jo.Call<bool>("onBackPressed");
		}
		
		public void SetAdUnitEventListener() 
		{
			jo.Call ("setAdUnitEventListener");
		}
		
	}
		
	public interface MobilecoreCallbackListener
	{
		void OnConfirmation (string responseString);
	}	
	
	public interface MobilecoreOnInterstitialReadyListener
	{
		void OnInterstitialReady ();
	}
	
	public interface MobilecoreOnStickeezReadyListener
	{
		void OnStickeezReady ();
	}
	
	public interface MobilecoreOnDirectToMarketReadyListener
	{
		void OnDirectToMarketReady ();
	}

	public interface MobilecoreOnAdUnitEventListener
	{
		void OnInterstitialEvent (string eventType);
		void OnStickeezEvent (string eventType);
		void OnDirectToMarketEvent (string eventType);
	}
}