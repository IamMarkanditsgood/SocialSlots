using UnityEngine;
using UnityEngine.Advertisements;

namespace Services.Ads
{
    public class AdsInitializer : IUnityAdsInitializationListener
    {
        private readonly RewardedAds _rewardedAds;
        private readonly InterstitialAds _interstitialAds;
        
        private const string IOSGameID = "5854661";

        public AdsInitializer(RewardedAds rewardedAds, InterstitialAds interstitialAds)
        {
            _rewardedAds = rewardedAds;
            _interstitialAds = interstitialAds;
        }

        public void InitializeAds(bool isTestMode)
        {
            string gameId = IOSGameID;
            Advertisement.Initialize(gameId, isTestMode, this);
        }


        public void OnInitializationComplete()
        {
            Debug.Log("Unity Ads initialization complete");
            _interstitialAds.LoadAd();
            _rewardedAds.LoadAd();
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log($"Unity Ads Initialization Failed:{error.ToString()}-{message}");
        }
    }
}