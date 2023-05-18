namespace VimAds.Runtime.InterstitialRunner
{
    public interface IInterstitialRunner
    {
        void AddCooldown();
        
        void ResetIdle();
        void UpdateLastAd();
    }
}