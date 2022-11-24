using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour,IUnityAdsLoadListener, IUnityAdsShowListener, IUnityAdsInitializationListener
{
    private static AdsManager instance;
    public static AdsManager Instance => instance;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }
#if UNITY_IOS
    string gameId = "5030924";
    private const string BANNER_PLACEMENT = "Banner_iOS";
    private const string VIDEO_PLACEMENT = "Interstitial_iOS";
    private const string REWARDED_VIDEO_PLACEMENT = "Rewarded_iOS";
#else
    private string GAME_ID = "5030925";
    private const string BANNER_PLACEMENT = "Banner_Android";
    private const string VIDEO_PLACEMENT = "Interstitial_Android";
    private const string REWARDED_VIDEO_PLACEMENT = "Rewarded_Android";
#endif
    [SerializeField] private BannerPosition bannerPosition = BannerPosition.BOTTOM_CENTER;
    [SerializeField] private bool testMode = true;
    private bool showBanner = false;

    // Start is called before the first frame update
    void Start()
    {
        Advertisement.Initialize(GAME_ID, testMode,this);
        LoadAd();
    }

    public void LoadAd()
    {
        Advertisement.Load(VIDEO_PLACEMENT, this);
    }
    public async Task ShowAd()
    {
        await Task.Delay(1000);
        Advertisement.Show(VIDEO_PLACEMENT,this);
    }
    public void LoadRewardedAd()
    {
        Advertisement.Load(REWARDED_VIDEO_PLACEMENT, this);
    }

    public void ShowRewardedAd()
    {
        Advertisement.Show(REWARDED_VIDEO_PLACEMENT, this);
    }
    public void ToggleBanner() 
    {
        showBanner = !showBanner;

        if (showBanner)
        {
            Advertisement.Banner.SetPosition(bannerPosition);
            Advertisement.Banner.Show(BANNER_PLACEMENT);
        }
        else
        {
            Advertisement.Banner.Hide(false);
        }
    }
    #region Interface Implementations
    public void OnInitializationComplete()
    {
        Debug.Log("Init Success");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Init Failed: [{error}]: {message}");
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log($"Load Success: {placementId}");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Load Failed: [{error}:{placementId}] {message}");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log($"OnUnityAdsShowFailure: [{error}]: {message}");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log($"OnUnityAdsShowStart: {placementId}");
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log($"OnUnityAdsShowClick: {placementId}");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log($"OnUnityAdsShowComplete: [{showCompletionState}]: {placementId}");
    }
    #endregion
}
