using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


// Example for IronSource Unity.
public class IronSourceDemoScript : MonoBehaviour
{

   

    public void Start()
    {

#if UNITY_ANDROID
        string appKey = "1977f369d";
#elif UNITY_IPHONE
        string appKey = "8545d445";
#else
        string appKey = "unexpected_platform";
#endif



        Debug.Log("unity-script: IronSource.Agent.validateIntegration");
        IronSource.Agent.validateIntegration();

        Debug.Log("unity-script: unity version" + IronSource.unityVersion());

        // SDK init
        Debug.Log("unity-script: IronSource.Agent.init");
        IronSource.Agent.init(appKey);

    }

    void OnEnable()
    {

        //Add Init Event
        IronSourceEvents.onSdkInitializationCompletedEvent += SdkInitializationCompletedEvent;
        
        IronSourceEvents.onImpressionDataReadyEvent += ImpressionDataReadyEvent;


        //Add AdInfo Rewarded Video Events
        IronSourceRewardedVideoEvents.onAdOpenedEvent += ReardedVideoOnAdOpenedEvent;
        IronSourceRewardedVideoEvents.onAdClosedEvent += ReardedVideoOnAdClosedEvent;
        IronSourceRewardedVideoEvents.onAdAvailableEvent += ReardedVideoOnAdAvailable;
        IronSourceRewardedVideoEvents.onAdUnavailableEvent += ReardedVideoOnAdUnavailable;
        IronSourceRewardedVideoEvents.onAdShowFailedEvent += ReardedVideoOnAdShowFailedEvent;
        IronSourceRewardedVideoEvents.onAdRewardedEvent += ReardedVideoOnAdRewardedEvent;
        IronSourceRewardedVideoEvents.onAdClickedEvent += ReardedVideoOnAdClickedEvent;
        

        //Add AdInfo Interstitial Events
        IronSourceInterstitialEvents.onAdReadyEvent += InterstitialOnAdReadyEvent;
        IronSourceInterstitialEvents.onAdLoadFailedEvent += InterstitialOnAdLoadFailed;
        IronSourceInterstitialEvents.onAdOpenedEvent += InterstitialOnAdOpenedEvent;
        IronSourceInterstitialEvents.onAdClickedEvent += InterstitialOnAdClickedEvent;
        IronSourceInterstitialEvents.onAdShowSucceededEvent += InterstitialOnAdShowSucceededEvent;
        IronSourceInterstitialEvents.onAdShowFailedEvent += InterstitialOnAdShowFailedEvent;
        IronSourceInterstitialEvents.onAdClosedEvent += InterstitialOnAdClosedEvent;

        //Add AdInfo Banner Events
        IronSourceBannerEvents.onAdLoadedEvent += BannerOnAdLoadedEvent;
        IronSourceBannerEvents.onAdLoadFailedEvent += BannerOnAdLoadFailedEvent;
        IronSourceBannerEvents.onAdClickedEvent += BannerOnAdClickedEvent;
        IronSourceBannerEvents.onAdScreenPresentedEvent += BannerOnAdScreenPresentedEvent;
        IronSourceBannerEvents.onAdScreenDismissedEvent += BannerOnAdScreenDismissedEvent;
        IronSourceBannerEvents.onAdLeftApplicationEvent += BannerOnAdLeftApplicationEvent;

    }

    void OnApplicationPause(bool isPaused)
    {
        Debug.Log("unity-script: OnApplicationPause = " + isPaused);
        IronSource.Agent.onApplicationPause(isPaused);
    }

    public void OnGUI()
    {

        GUI.backgroundColor = Color.blue;
        GUI.skin.button.fontSize = (int)(0.035f * Screen.width);





        Rect showRewardedVideoButton = new Rect(0.10f * Screen.width, 0.15f * Screen.height, 0.80f * Screen.width, 0.08f * Screen.height);
        if (GUI.Button(showRewardedVideoButton, "Show Rewarded Video"))
        {
            Debug.Log("unity-script: ShowRewardedVideoButtonClicked");
            if (IronSource.Agent.isRewardedVideoAvailable())
            {
                IronSource.Agent.showRewardedVideo();
            }
            else
            {
                Debug.Log("unity-script: IronSource.Agent.isRewardedVideoAvailable - False");
            }
        }
       
        

        Rect showOfferwallButton = new Rect(0.10f * Screen.width, 0.25f * Screen.height, 0.80f * Screen.width, 0.08f * Screen.height);
        if (GUI.Button(showOfferwallButton, "Show Offerwall"))
        {
            if (IronSource.Agent.isOfferwallAvailable())
            {
                IronSource.Agent.showOfferwall();
            }
            else
            {
                Debug.Log("IronSource.Agent.isOfferwallAvailable - False");
            }
        }

        Rect loadInterstitialButton = new Rect(0.10f * Screen.width, 0.35f * Screen.height, 0.35f * Screen.width, 0.08f * Screen.height);
        if (GUI.Button(loadInterstitialButton, "Load Interstitial"))
        {
            Debug.Log("unity-script: LoadInterstitialButtonClicked");
            IronSource.Agent.loadInterstitial();
        }

        Rect showInterstitialButton = new Rect(0.55f * Screen.width, 0.35f * Screen.height, 0.35f * Screen.width, 0.08f * Screen.height);
        if (GUI.Button(showInterstitialButton, "Show Interstitial"))
        {
            Debug.Log("unity-script: ShowInterstitialButtonClicked");
            if (IronSource.Agent.isInterstitialReady())
            {
                IronSource.Agent.showInterstitial();
            }
            else
            {
                Debug.Log("unity-script: IronSource.Agent.isInterstitialReady - False");
            }
        }

        Rect loadBannerButton = new Rect(0.10f * Screen.width, 0.45f * Screen.height, 0.35f * Screen.width, 0.08f * Screen.height);
        if (GUI.Button(loadBannerButton, "Load Banner"))
        {
            Debug.Log("unity-script: loadBannerButtonClicked");
            IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, IronSourceBannerPosition.BOTTOM);
        }

        Rect destroyBannerButton = new Rect(0.55f * Screen.width, 0.45f * Screen.height, 0.35f * Screen.width, 0.08f * Screen.height);
        if (GUI.Button(destroyBannerButton, "Destroy Banner"))
        {
            Debug.Log("unity-script: loadBannerButtonClicked");
            IronSource.Agent.destroyBanner();
        }




    }
    
    #region Init callback handlers

    void SdkInitializationCompletedEvent()
    {
        Debug.Log("unity-script: I got SdkInitializationCompletedEvent");
    }

    #endregion

    #region AdInfo Rewarded Video
    void ReardedVideoOnAdOpenedEvent(IronSourceAdInfo adInfo) {
         Debug.Log("unity-script: I got ReardedVideoOnAdOpenedEvent With AdInfo " + adInfo.ToString());
    }
    void ReardedVideoOnAdClosedEvent(IronSourceAdInfo adInfo)
    {
        Debug.Log("unity-script: I got ReardedVideoOnAdClosedEvent With AdInfo " + adInfo.ToString());
    }
    void ReardedVideoOnAdAvailable(IronSourceAdInfo adInfo)
    {
        Debug.Log("unity-script: I got ReardedVideoOnAdAvailable With AdInfo " + adInfo.ToString());
    }
    void ReardedVideoOnAdUnavailable()
    {
        Debug.Log("unity-script: I got ReardedVideoOnAdUnavailable");
    }
    void ReardedVideoOnAdShowFailedEvent(IronSourceError ironSourceError,IronSourceAdInfo adInfo)
    {
        Debug.Log("unity-script: I got RewardedVideoAdOpenedEvent With Error"+ironSourceError.ToString() + "And AdInfo " + adInfo.ToString());
    }
    void ReardedVideoOnAdRewardedEvent(IronSourcePlacement ironSourcePlacement,IronSourceAdInfo adInfo)
    {
        Debug.Log("unity-script: I got ReardedVideoOnAdRewardedEvent With Placement" + ironSourcePlacement.ToString()+ "And AdInfo " + adInfo.ToString());
    }
    void ReardedVideoOnAdClickedEvent(IronSourcePlacement ironSourcePlacement, IronSourceAdInfo adInfo)
    {
        Debug.Log("unity-script: I got ReardedVideoOnAdClickedEvent With Placement" + ironSourcePlacement.ToString() + "And AdInfo " + adInfo.ToString());
    }

    #endregion
    
    #region AdInfo Interstitial

    void InterstitialOnAdReadyEvent(IronSourceAdInfo adInfo) {
        Debug.Log("unity-script: I got InterstitialOnAdReadyEvent With AdInfo " + adInfo.ToString());
    }

    void InterstitialOnAdLoadFailed(IronSourceError ironSourceError) {
        Debug.Log("unity-script: I got InterstitialOnAdLoadFailed With Error " + ironSourceError.ToString());
    }

    void InterstitialOnAdOpenedEvent(IronSourceAdInfo adInfo) {
        Debug.Log("unity-script: I got InterstitialOnAdOpenedEvent With AdInfo " + adInfo.ToString());
    }

    void InterstitialOnAdClickedEvent(IronSourceAdInfo adInfo) {
        Debug.Log("unity-script: I got InterstitialOnAdClickedEvent With AdInfo " + adInfo.ToString());
    }

    void InterstitialOnAdShowSucceededEvent(IronSourceAdInfo adInfo) {
        Debug.Log("unity-script: I got InterstitialOnAdShowSucceededEvent With AdInfo " + adInfo.ToString());
    }

    void InterstitialOnAdShowFailedEvent(IronSourceError ironSourceError, IronSourceAdInfo adInfo) {
        Debug.Log("unity-script: I got InterstitialOnAdShowFailedEvent With Error " +ironSourceError.ToString()+ " And AdInfo " + adInfo.ToString());
    }

    void InterstitialOnAdClosedEvent(IronSourceAdInfo adInfo) {
        Debug.Log("unity-script: I got InterstitialOnAdClosedEvent With AdInfo " + adInfo.ToString());
    }


    #endregion
    
    #region Banner AdInfo

    void BannerOnAdLoadedEvent(IronSourceAdInfo adInfo) {
        Debug.Log("unity-script: I got BannerOnAdLoadedEvent With AdInfo " + adInfo.ToString());
    }

    void BannerOnAdLoadFailedEvent(IronSourceError ironSourceError) {
        Debug.Log("unity-script: I got BannerOnAdLoadFailedEvent With Error " + ironSourceError.ToString());
    }

    void BannerOnAdClickedEvent(IronSourceAdInfo adInfo) {
        Debug.Log("unity-script: I got BannerOnAdClickedEvent With AdInfo " + adInfo.ToString());
    }

    void BannerOnAdScreenPresentedEvent(IronSourceAdInfo adInfo) {
        Debug.Log("unity-script: I got BannerOnAdScreenPresentedEvent With AdInfo " + adInfo.ToString());
    }

    void BannerOnAdScreenDismissedEvent(IronSourceAdInfo adInfo) {
        Debug.Log("unity-script: I got BannerOnAdScreenDismissedEvent With AdInfo " + adInfo.ToString());
    }

    void BannerOnAdLeftApplicationEvent(IronSourceAdInfo adInfo) {
        Debug.Log("unity-script: I got BannerOnAdLeftApplicationEvent With AdInfo " + adInfo.ToString());
    }

    #endregion
    
    #region ImpressionSuccess callback handler

    void ImpressionDataReadyEvent(IronSourceImpressionData impressionData)
    {
        Debug.Log("unity - script: I got ImpressionDataReadyEvent ToString(): " + impressionData.ToString());
        Debug.Log("unity - script: I got ImpressionDataReadyEvent allData: " + impressionData.allData);
    }

    #endregion

}
