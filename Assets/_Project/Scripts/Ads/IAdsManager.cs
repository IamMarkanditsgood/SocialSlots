namespace Services.Ads
{
    public interface IAdsManager
    {
        RewardedAds RewardedAds { get; }
        InterstitialAds InterstitialAds { get; }
    }
}