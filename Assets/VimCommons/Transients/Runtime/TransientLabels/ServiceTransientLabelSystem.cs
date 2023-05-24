using Commons.Camera.Runtime.ServiceCamera;
using Core.Runtime.DependencyManagement;
using Core.Runtime.EZTween;
using Core.Runtime.Pooling;
using TMPro;
using UnityEngine;

namespace Commons.Transients.Runtime.TransientLabels
{
    public class ServiceTransientLabelSystem : MonoBehaviour, ITransientLabelSystem
    {
        private static readonly ServiceContainer<ITransientLabelSystem> Container = Locator.Single<ITransientLabelSystem>();

        private static ICamera Camera => Locator.Resolve<ICamera>();
        public TMP_Text prefab;

        private PrefabPool<TMP_Text> _pool;
        public PrefabPool<TMP_Text> Pool => _pool ??= PrefabPool<TMP_Text>.Instance(prefab);

        private void Awake() => Container.Attach(this);

        public void Emit(Vector3 position, string text, Color color, float size)
        {
            var from = position;
            var to = from + Vector3.up;

            var colorFrom = color;
            colorFrom.a = 0;
            var colorTo = color;
            
            var label = Pool.Spawn();
            label.transform.position = from;
            label.text = text;
            label.fontSize = size;
            label.transform.rotation = Camera.Transform.rotation;
            EZ.Spawn().Tween(ez =>
            {
                label.color = Color.Lerp(colorFrom, colorTo, ez.QuintOut);
                label.transform.position = Vector3.LerpUnclamped(from, to, ez.BackOut);
            }).Delay(1).Tween(ez =>
            {
                var t = ez.Linear;
                label.color = Color.Lerp(colorFrom, colorTo, 1-t);
            }).Call(_ =>
            {
                _pool.Remove(label);
            });
        }
    }
}