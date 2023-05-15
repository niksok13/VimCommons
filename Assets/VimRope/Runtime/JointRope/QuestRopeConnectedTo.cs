using UnityEngine;
using VimQuestQueue.Runtime.ServiceQuestQueue;

namespace VimRope.Runtime.JointRope
{
    public class QuestRopeConnectedTo : AQuest
    {
        public ModelJointRope rope;
        public Transform anchor;
        public override bool Done => rope.IsAttachedTo(anchor);
    }
}