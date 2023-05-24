using Commons.QuestQueue.Runtime.ServiceQuestQueue;
using Commons.Rope.Runtime.JointRope;
using UnityEngine;

namespace Commons.QuestCommons.Runtime
{
    public class QuestRopeConnectedTo : AQuest
    {
        public ModelJointRope rope;
        public Transform anchor;
        public override bool Done => rope.IsAttachedTo(anchor);
    }
}