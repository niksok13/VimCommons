using VimProgression.Runtime.Node;
using VimQuestQueue.Runtime.ServiceQuestQueue;

namespace VimQuestCommons.Runtime
{
    public class QuestNodeLevel: AQuestCount
    {
        public ProgressionNode node;
        public int nodeLevelReq = 1;
        public override int Current => node.NodeLevel.Value;
        public override int Target => nodeLevelReq;
    }
}