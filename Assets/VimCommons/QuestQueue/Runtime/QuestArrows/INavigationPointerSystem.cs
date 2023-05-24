using UnityEngine;

namespace VimCommons.QuestQueue.Runtime.QuestArrows
{
    public interface INavigationPointerSystem
    {
        void Add(params Transform[] targets);
        void Remove(params Transform[] targets);
        void SetAnchor(Transform transform);
    }
}