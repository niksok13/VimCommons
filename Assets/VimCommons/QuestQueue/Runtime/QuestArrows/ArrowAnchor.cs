using UnityEngine;
using VimCore.Runtime.DependencyManagement;

namespace VimCommons.QuestQueue.Runtime.QuestArrows
{
    public class ArrowAnchor: MonoBehaviour
    {
        private static INavigationPointerSystem NavigationPointerSystem => Locator.Resolve<INavigationPointerSystem>();
        private void OnEnable() => NavigationPointerSystem?.SetAnchor(transform);
    }
}