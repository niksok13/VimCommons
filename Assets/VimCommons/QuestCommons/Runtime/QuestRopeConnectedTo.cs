using UnityEngine;
using VimCommons.QuestQueue.Runtime.ServiceQuestQueue;
using VimCommons.Rope.Runtime.JointRope;

namespace VimCommons.QuestCommons.Runtime
{
    public class QuestRopeConnectedTo : AQuest
    {
        public ModelJointRope rope;
        public Transform anchor;
        public override bool Done => rope.IsAttachedTo(anchor);
    }
}