using Cysharp.Threading.Tasks;
using UnityEngine;
using VimCommons.SceneManagement.Runtime.UISplashscreen;
using VimCore.Runtime.DependencyManagement;

namespace VimCommons.SceneManagement.Runtime.GameCore.States
{
    public class SGameInit : SGameAbstract
    {
        private static ISplashscreen Splashscreen => Locator.Resolve<ISplashscreen>();

        public override async void Enter()
        {
            Splashscreen?.Show("PLEASE WAIT");
#if !UNITY_IOS
            var height = (int)(Screen.height * 720f / Screen.width);
            Screen.SetResolution(720, height, true, 60);
#endif
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
            Application.backgroundLoadingPriority = ThreadPriority.Low;
            await UniTask.Delay(50);
#if UNITY_IOS
            ChangeState<SGameATTRequest>();
#else
            ChangeState<SGameLevelLoad>();
#endif
        }
    }
}