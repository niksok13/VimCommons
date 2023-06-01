using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace VimCore.Runtime.DependencyManagement
{
    [CreateAssetMenu]
    public class GlobalServices : ScriptableObject
    {
        public GameObject core;
        public GameObject[] global;
        public GameObject[] debug;

#if UNITY_EDITOR
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
#else
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
#endif
        private static void Init()
        {
#if UNITY_EDITOR
            var scene = SceneManager.GetActiveScene();
            var inBuild = EditorBuildSettings.scenes.Any(s => s.path == scene.path);
            if (!inBuild) return;
#endif
            var services = Resources.Load<GlobalServices>("GlobalServices");  
#if DEVELOPMENT_BUILD || UNITY_EDITOR
            SpawnServices(services.debug);
#endif
            SpawnServices(services.global);
            Spawn(services.core);
        }
        private static void SpawnServices(IEnumerable<GameObject> services)
        {
            foreach (var service in services) 
                Spawn(service);
        }

        private static void Spawn(GameObject service)
        {
            var instance = Instantiate(service);
            instance.name = $"~ {service.name} ~";
            DontDestroyOnLoad(instance);
        }
    }
}