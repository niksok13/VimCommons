namespace VimAds.Runtime.Interstitial
{
    public interface IInterstitial
    {
        void ShowInterstitial();
        void ResetIdle();
        void UpdateLastAd();
    }
}