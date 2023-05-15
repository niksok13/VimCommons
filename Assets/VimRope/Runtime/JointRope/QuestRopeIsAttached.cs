using VimQuestQueue.Runtime.ServiceQuestQueue;

namespace VimRope.Runtime.JointRope
{
    public class QuestRopeIsAttached : AQuest
    {
        public ModelJointRope rope;
        public bool invert;
        public override bool Done => rope.IsAttached != invert;
    }
}
