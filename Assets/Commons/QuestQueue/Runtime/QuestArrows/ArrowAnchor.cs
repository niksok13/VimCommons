using Core.Runtime.DependencyManagement;
using UnityEngine;

namespace Commons.QuestQueue.Runtime.QuestArrows
{
    public class ArrowAnchor: MonoBehaviour
    {
        private static INavigationPointerSystem NavigationPointerSystem => Locator.Resolve<INavigationPointerSystem>();
        private void OnEnable() => NavigationPointerSystem?.SetAnchor(transform);
    }
}