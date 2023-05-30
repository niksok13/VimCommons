using UnityEngine;
using VimCore.Runtime.DependencyManagement;

namespace VimCommons.QuestQueue.Runtime.QuestArrows
{
    public class ArrowAnchor: MonoBehaviour
    {
        private static IQuestArrowSystem QuestArrowSystem => Locator.Resolve<IQuestArrowSystem>();
        private void OnEnable() => QuestArrowSystem?.SetAnchor(transform);
    }
}