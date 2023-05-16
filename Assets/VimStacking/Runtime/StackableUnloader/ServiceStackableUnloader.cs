using UnityEngine;
using VimCore.Runtime.DependencyManagement;
using VimSceneManagement.Runtime.ServiceLevelLoader;
using VimStacking.Runtime.StackableConverter;

namespace VimStacking.Runtime.StackableUnloader
{
    public class ServiceStackableUnloader : MonoBehaviour
    {
        private static ILevelLoader LevelLoader => Locator.Resolve<ILevelLoader>();
        
        private static readonly Filter<StackComponent> Stacks = Locator.Filter<StackComponent>();
        private static readonly Filter<ModelStackableConverter> Converters = Locator.Filter<ModelStackableConverter>();
        
        private void Awake()
        {
            LevelLoader.OnUnload += OnUnload;
        }

        private void OnUnload()
        {
            foreach (var stack in Stacks) stack.Clear();
            foreach (var converter in Converters) converter.Clear();
        }
    }
}