using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Services.Ads
{
    public class InterstitialAds : IUnityAdsLoadListener, IUnityAdsShowListener
    {
        private const string AndroidAdID = "Interstitial_Android";
        private const string IOSAdID = "Interstitial_iOS";
        private readonly string _adID;

        public InterstitialAds()
        {
            _adID = IOSAdID;
            LoadAd();
        }

        public void LoadAd()
        {
            Debug.Log($"Loading Ad:{_adID}");
            Advertisement.Load(_adID, this);
        }

        public void ShowAd()
        {
            Debug.Log($"Loading Ad:{_adID}");
            Advertisement.Show(_adID, this);
        }

        public void OnUnityAdsAdLoaded(string placementId)
        {
        }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
        }

        public void OnUnityAdsShowStart(string placementId)
        {
        }

        public void OnUnityAdsShowClick(string placementId)
        {
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            LoadAd();
        }
    }
}