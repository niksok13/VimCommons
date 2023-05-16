using System.Linq;
using VimCore.Runtime.MVVM;
using VimProgression.Runtime.Node;

namespace VimProgression.Runtime.Building
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
