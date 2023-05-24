using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VimCore.Runtime.MVVM;
using VimCore.Runtime.Utils;

namespace VimCommons.Progression.Runtime.Node
{
    [CreateAssetMenu]
    public class ProgressionNode : ScriptableObjectWithGuid
    {
        private List<ProgressionNode> ChildNodes { get; } = new();

        public ProgressionNode parent;
        public int parentLevel = 1;
        public int initialLevel;
        public List<NodeLevelInfo> upgrades;
    
        public ObservableData<int> NodeLevel { get; } = new();
        public ObservableChannel NodeUpgraded { get; } = new();
        public NodeLevelInfo NextUpgrade => upgrades.ElementAtOrDefault(NodeLevel.Value);

        private void OnEnable()
        {
            if (parent) 
                parent.ChildNodes.Add(this);
            NodeLevel.ConnectPref($"node_{guid}");
            
            var unlocked = parent == null || parent.NodeLevel.Value >= parentLevel;
            if (unlocked && NodeLevel.Value < initialLevel)
                NodeLevel.Value = initialLevel;
            
            NodeLevel.OnValue += OnProgress;
        }
        
        private void OnProgress(int obj)
        {
            ChildNodes.ForEach(i => i.NodeLevel.Touch());
        }
        
        public void Upgrade()
        {
            if (NodeLevel.Value > upgrades.Count - 1) return;
            NodeUpgraded.Invoke();
            NodeLevel.Value += 1;
        }
    }
}