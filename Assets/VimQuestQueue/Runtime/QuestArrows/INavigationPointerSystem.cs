using UnityEngine;

namespace VimQuestQueue.Runtime.QuestArrows
{
    public interface INavigationPointerSystem
    {
        void Add(params Transform[] targets);
        void Remove(params Transform[] targets);
        void SetAnchor(Transform transform);
    }
}