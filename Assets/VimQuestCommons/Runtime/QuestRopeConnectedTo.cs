using UnityEngine;
using VimQuestQueue.Runtime.ServiceQuestQueue;
using VimRope.Runtime.JointRope;

namespace VimQuestCommons.Runtime
{
    public class QuestRopeConnectedTo : AQuest
    {
        public ModelJointRope rope;
        public Transform anchor;
        public override bool Done => rope.IsAttachedTo(anchor);
    }
}