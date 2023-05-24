using Commons.Progression.Runtime.Node;
using Commons.QuestQueue.Runtime.ServiceQuestQueue;

namespace Commons.QuestCommons.Runtime
{
    public class QuestNodeLevel: AQuestCount
    {
        public ProgressionNode node;
        public int nodeLevelReq = 1;
        public override int Current => node.NodeLevel.Value;
        public override int Target => nodeLevelReq;
    }
}