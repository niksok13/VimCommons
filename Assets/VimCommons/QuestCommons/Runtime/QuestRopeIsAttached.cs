using Commons.QuestQueue.Runtime.ServiceQuestQueue;
using Commons.Rope.Runtime.JointRope;

namespace Commons.QuestCommons.Runtime
{
    public class QuestRopeIsAttached : AQuest
    {
        public ModelJointRope rope;
        public bool invert;
        public override bool Done => rope.IsAttached != invert;
    }
}
