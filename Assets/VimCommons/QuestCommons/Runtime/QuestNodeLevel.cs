using VimCommons.Progression.Runtime.Node;
using VimCommons.QuestQueue.Runtime.ServiceQuestQueue;

namespace VimCommons.QuestCommons.Runtime
{
    public class QuestNodeLevel: AQuestCount
    {
        public ProgressionNode node;
        public int nodeLevelReq = 1;
        public override int Current => node.NodeLevel.Value;
        public override int Target => nodeLevelReq;
    }
}