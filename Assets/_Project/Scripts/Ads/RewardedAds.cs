using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Services.Ads
{
    public class RewardedAds : IUnityAdsLoadListener, IUnityAdsShowListener
    {
        private const string AndroidAdUnitId = "Rewarded_Android";
        private const string IOSAdUnitId = "Rewarded_iOS";
        private readonly string _adUnitId;
        private bool _isReadyToShow;
        private AdRewardsType _currentAdRewardsType;
        public event Action<AdRewardsType> OnRewardedAdCompleted;

        public RewardedAds()
        {
#if UNITY_IOS
        _adUnitId = IOSAdUnitId;
#elif UNITY_ANDROID
            _adUnitId = AndroidAdUnitId;
#endif
        }

        public void LoadAd()
        {
            Debug.Log("Loading Ad: " + _adUnitId);
            Advertisement.Load(_adUnitId, this);
        }

        public void OnUnityAdsAdLoaded(string adUnitId)
        {
            Debug.Log("Ad Loaded: " + adUnitId);

            if (adUnitId.Equals(_adUnitId))
            {
                _isReadyToShow = true;
            }
        }

        public void ShowAd(AdRewardsType type = AdRewardsType.None)
        {
            if (_isReadyToShow)
            {
                _currentAdRewardsType = type;
                Advertisement.Show(_adUnitId, this);
            }
        }

        public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
        {
            if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
            {
                Debug.Log("Unity Ads Rewarded Ad Completed");
                Advertisement.Load(_adUnitId, this);
                OnRewardedAdCompleted?.Invoke(_currentAdRewardsType);
            }
        }

        public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
        {
            Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
        }

        public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
        {
            Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        }

        public void OnUnityAdsShowStart(string adUnitId)
        {
        }

        public void OnUnityAdsShowClick(string adUnitId)
        {
        }
    }
}