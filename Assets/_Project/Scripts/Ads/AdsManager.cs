

using UnityEngine;
using UnityEngine.UI;

namespace Services.Ads
{
    public class AdsManager : MonoBehaviour, IAdsManager
    {
        public static AdsManager instance;
        [SerializeField]private  bool _isTestMode;
        public RewardedAds RewardedAds { get; private set; }
        public InterstitialAds InterstitialAds { get; private set; }

        public Button test1;
  
        public void Start()
        {
            if(instance == null)
            {
                instance = this;
            }
            Init();
           // test1.onClick.AddListener(() => RewardedAds.ShowAd(AdRewardsType.None));      
        }

        

        public void Init()
        {
            RewardedAds = new RewardedAds();
            InterstitialAds = new InterstitialAds();
            var adsInitializer = new AdsInitializer(RewardedAds, InterstitialAds);
            adsInitializer.InitializeAds(_isTestMode);
        }

    }
}