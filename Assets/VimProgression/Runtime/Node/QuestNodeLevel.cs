using VimQuestQueue.Runtime.ServiceQuestQueue;

namespace VimProgression.Runtime.Node
{
    public class QuestNodeLevel: AQuestCount
    {
        public ProgressionNode node;
        public int nodeLevelReq = 1;
        public override int Current => node.NodeLevel.Value;
        public override int Target => nodeLevelReq;
    }
}