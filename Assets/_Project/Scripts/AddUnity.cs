using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;


public class AddUnity : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private string iOSGameId = "5854661";
    [SerializeField] private string adUnitId = "Interstitial_iOS"; // або "Interstitial_Android"
    [SerializeField] private bool testMode = true;

    public Button addTest;

    private bool adIsReady = false;

    void Start()
    {
#if UNITY_IOS
        Advertisement.Initialize(iOSGameId, testMode);
        Advertisement.Load(adUnitId, this);
#endif

        addTest.onClick.AddListener(() =>
        {
            if (adIsReady)
                Advertisement.Show(adUnitId, this);
            else
                Debug.LogWarning("Ad not ready yet!");
        });
    }

    // === Load callbacks ===
    public void OnUnityAdsAdLoaded(string placementId)
    {
        if (placementId == adUnitId)
        {
            Debug.Log("Ad Loaded!");
            adIsReady = true;
        }
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.LogError($"Failed to load ad {placementId}: {error.ToString()} - {message}");
    }

    // === Show callbacks ===
    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log("Ad finished: " + showCompletionState);
        adIsReady = false;
        Advertisement.Load(adUnitId, this); // Пере-завантажити після показу
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.LogError($"Ad show failed: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string placementId) { }
    public void OnUnityAdsShowClick(string placementId) { }
}
