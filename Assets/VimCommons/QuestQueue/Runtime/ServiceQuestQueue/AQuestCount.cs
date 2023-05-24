namespace VimCommons.QuestQueue.Runtime.ServiceQuestQueue
{
    public abstract class AQuestCount : AQuest
    {
        
        public override bool Done => Current >= Target;
        public abstract int Current { get; }
        public abstract int Target { get; }
    }
}