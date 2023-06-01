using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace VimCore.Runtime.DependencyManagement
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
        public List<GameObject> debugServices;

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
            
#if DEVELOPMENT_BUILD
                     foreach (var prefab in debugServices)
                {
                    var instance = Instantiate(prefab);
                    DontDestroyOnLoad(instance);
                    instance.name = $"# {prefab.name} #";
                }      
#endif
            
#if UNITY_EDITOR
            foreach (var prefab in debugServices)
            {
                var instance = Instantiate(prefab);
                DontDestroyOnLoad(instance);
                instance.name = $"# {prefab.name} #";
            }      
#endif
            var core = Instantiate(gameCore);
            core.name = $"* {gameCore.name} *";
            DontDestroyOnLoad(core);
        }
    }
}