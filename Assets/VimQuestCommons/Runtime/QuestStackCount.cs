using VimQuestQueue.Runtime.ServiceQuestQueue;
using VimStacking.Runtime;
using VimStacking.Runtime.Stackable;

namespace VimQuestCommons.Runtime
{
    public class QuestStackCount : AQuestCount
    {
        public StackComponent stack;
        public int count;
        
        public StackableDefinition definition;

        public override int Current => stack.CountTyped(definition);
        public override int Target => count;
    }
}