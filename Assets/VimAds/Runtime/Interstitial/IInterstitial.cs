namespace VimAds.ServiceAds.Interstitial
{
    public interface IInterstitial
    {
        void ResetIdle();
        void UpdateLastAd();
    }
}