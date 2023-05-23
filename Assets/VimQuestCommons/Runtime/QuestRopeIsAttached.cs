using VimQuestQueue.Runtime.ServiceQuestQueue;
using VimRope.Runtime.JointRope;

namespace VimQuestCommons.Runtime
{
    public class QuestRopeIsAttached : AQuest
    {
        public ModelJointRope rope;
        public bool invert;
        public override bool Done => rope.IsAttached != invert;
    }
}
