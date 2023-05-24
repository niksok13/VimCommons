using VimCommons.QuestQueue.Runtime.ServiceQuestQueue;
using VimCommons.Rope.Runtime.JointRope;

namespace VimCommons.QuestCommons.Runtime
{
    public class QuestRopeIsAttached : AQuest
    {
        public ModelJointRope rope;
        public bool invert;
        public override bool Done => rope.IsAttached != invert;
    }
}
