using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace VimPackages.Core.Runtime.DependencyManagement
{
    [CreateAssetMenu(menuName = "Create GlobalServices", fileName = "GlobalServices", order = 0)]
    public class GlobalServices : ScriptableObject
    {
#if UNITY_EDITOR
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]

#else
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
#endif
        private static void BeforeSceneLoad() => Resources.Load<GlobalServices>("GlobalServices").Init();

        public GameObject gameCore;
        public List<GameObject> globalServices;

        private void Init()
        {
#if UNITY_EDITOR
            var scene = SceneManager.GetActiveScene();
            var inBuild = EditorBuildSettings.scenes.Any(s => s.path == scene.path);
            if (!inBuild) return;
#endif
            foreach (var prefab in globalServices)
            {
                var instance = Instantiate(prefab);
                DontDestroyOnLoad(instance);
                instance.name = $"~ {prefab.name} ~";
            }

            var core = Instantiate(gameCore);
            DontDestroyOnLoad(core);
        }
    }
}