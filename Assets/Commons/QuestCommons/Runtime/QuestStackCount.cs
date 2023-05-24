using Commons.QuestQueue.Runtime.ServiceQuestQueue;
using Commons.Stacking.Runtime;
using Commons.Stacking.Runtime.Stackable;

namespace Commons.QuestCommons.Runtime
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