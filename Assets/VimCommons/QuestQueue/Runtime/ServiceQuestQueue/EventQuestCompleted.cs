namespace Commons.QuestQueue.Runtime.ServiceQuestQueue
{
    public class EventQuestCompleted
    {
        public int Step { get; }
        public string Name { get; }

        public EventQuestCompleted(int step, string name)
        {
            Step = step;
            Name = name;
        }
    }
}