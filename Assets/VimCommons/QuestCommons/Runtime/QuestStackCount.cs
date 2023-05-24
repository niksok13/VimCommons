using VimCommons.QuestQueue.Runtime.ServiceQuestQueue;
using VimCommons.Stacking.Runtime;
using VimCommons.Stacking.Runtime.Stackable;

namespace VimCommons.QuestCommons.Runtime
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