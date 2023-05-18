using UnityEngine;
using VimCore.Runtime.DependencyManagement;
using VimCore.Runtime.MVVM;
using VimCore.Runtime.Utils;

namespace VimBoosters.Runtime.Core
{
    [CreateAssetMenu]
    public class Booster : ScriptableObjectWithGuid
    {
        public string title;
        public Sprite icon;

        private static readonly Filter<Booster> Filter = Locator.Filter<Booster>();
        private void OnEnable() => Filter.Add(this);
        private void OnDisable() => Filter.Remove(this);
        
        public float Estimate { get; private set; }
        public bool Active => Estimate > 0;
        public ObservableData<bool> Observable { get; } = new();

        public void AddSeconds(float seconds)
        {
            Estimate += seconds;
        }

        public void Tick()
        {
            Estimate -= Time.deltaTime;
            Observable.Value = Estimate > 0;
        }
    }
}
