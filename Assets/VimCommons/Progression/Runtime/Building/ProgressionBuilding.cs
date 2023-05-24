using System.Linq;
using Commons.Progression.Runtime.Node;
using Core.Runtime.MVVM;

namespace Commons.Progression.Runtime.Building
{
    public class ProgressionBuilding : ModelBehaviour
    {
        public ProgressionNode node;
        protected ObservableData<int> NodeLevel => node.NodeLevel;
        protected ObservableChannel NodeUpgraded => node.NodeUpgraded;
    }
    
    public abstract class ProgressionBuilding<TLevelData> : ProgressionBuilding
    {
        public TLevelData[] levels;
        protected TLevelData LevelData => levels.ElementAtOrDefault(NodeLevel.Value - 1);
    }
}
