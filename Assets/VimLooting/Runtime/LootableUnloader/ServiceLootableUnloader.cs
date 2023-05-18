using UnityEngine;
using VimCore.Runtime.DependencyManagement;
using VimLooting.Runtime.Core;
using VimSceneManagement.Runtime.ServiceLevelLoader;

namespace VimLooting.Runtime.LootableUnloader
{
    public class ServiceLootableUnloader: MonoBehaviour
    {
        private static ILevelLoader Loader => Locator.Resolve<ILevelLoader>();

        private static readonly Filter<Lootable> Lootables = Locator.Filter<Lootable>();
        public void Awake() => Loader.OnUnload += OnUnload;


        private static void OnUnload()
        {
            foreach (var item in Lootables) 
                item.Remove();
        }
    }
}