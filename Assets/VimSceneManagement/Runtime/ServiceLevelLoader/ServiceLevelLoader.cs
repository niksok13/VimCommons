using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using VimCore.Runtime.DependencyManagement;

namespace VimSceneManagement.Runtime.ServiceLevelLoader
{
    public class ServiceLevelLoader : MonoBehaviour, ILevelLoader
    {
        private static readonly ServiceContainer<ILevelLoader> Container = Locator.Single<ILevelLoader>();

        public event Action OnUnload;

        private int _levelCount;

        private void Awake()
        {
            Container.Attach(this);
            _levelCount = SceneManager.sceneCountInBuildSettings-1;
        }

        private void OnDestroy()
        {
            Container.Detach(this);
        }

        public async Task LoadLevel(int level)
        {
            var sceneIndex = 1 + level % _levelCount;
            if (SceneManager.GetActiveScene().buildIndex == sceneIndex) return;
            if (SceneManager.GetActiveScene().buildIndex > 0) 
                await Unload();
            var loading = SceneManager.LoadSceneAsync(sceneIndex);
            while (!loading.isDone) 
                await UniTask.Delay(100, DelayType.UnscaledDeltaTime);
            await UniTask.Delay(1000, DelayType.UnscaledDeltaTime);
        }

        public async Task Unload()
        {
            OnUnload?.Invoke();
            var loading = SceneManager.LoadSceneAsync(0);
            while (!loading.isDone) 
                await UniTask.Delay(100, DelayType.UnscaledDeltaTime);
        }

    }
}