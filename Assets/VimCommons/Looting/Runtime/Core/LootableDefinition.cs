using Cysharp.Threading.Tasks;
using UnityEngine;
using VimCore.Runtime.DependencyManagement;
using VimCore.Runtime.EZTween;
using VimCore.Runtime.Pooling;
using VimCore.Runtime.Utils;

namespace VimCommons.Looting.Runtime.Core
{
    [CreateAssetMenu]
    public class LootableDefinition: ScriptableObjectWithGuid
    {
        public ModelLootable prefab;
        public Sprite icon;
        public string iconLabel;
        
        private static readonly Filter<ModelLootable> Filter = Locator.Filter<ModelLootable>();

        private PrefabPool<ModelLootable> _pool;
        public PrefabPool<ModelLootable> Pool => _pool ??= PrefabPool<ModelLootable>.Instance(prefab);
        
        public ModelLootable Spawn()
        {
            var instance = Pool.Spawn();
            instance.Init(this);
            return instance;
        }
        
        public void Spawn(Vector3 posFrom, Vector3 posTo)
        {
            var result = Spawn();
            result.Transform.position = posFrom;
            result.Transform.localScale = Vector3.zero;
            
            EZ.Spawn().Tween(0.3f, ez => {
                result.Transform.localPosition = Helper.LerpParabolic(posFrom, posTo, ez.Linear, 2);
                result.Transform.localScale = Vector3.one * ez.BackOut;
            }).Call(_ => {
                Filter.Add(result);
            });
        }

        public void Remove(ModelLootable lootable, Transform target)
        {
            Filter.Remove(lootable);
            var from = lootable.Transform.position;
            EZ.Spawn().Tween(ez => {
                lootable.Transform.localScale = Vector3.one*(1-ez.BackOut);
                lootable.Transform.position = Helper.LerpParabolic(from, target.position + Vector3.up, ez.Linear);
            }).Call(_ =>  {
                Pool.Remove(lootable);
            });
        }
        

        public async void Animate(int amount, Transform src, Transform dest)
        {
            var count = Mathf.Min(amount, 50);
            var delay = 100;
            for (var i = 0; i < count; i++)
            {
                await UniTask.Delay(delay, DelayType.UnscaledDeltaTime);
                delay -= delay/5;
                Animate(src, dest);
            }
        }

        private void Animate(Transform from, Transform to)
        {
            var item = Spawn();

            var srcPos = from.position + Vector3.up;
            var srcRot = Random.rotation;

            item.Transform.position = srcPos;
            item.Transform.rotation = srcRot;
            item.Transform.localScale = Vector3.zero;

            var spread = Random.insideUnitSphere;
            spread *= spread.magnitude;

            var middlePos = Vector3.Lerp(from.position, to.position, 0.5f) + Vector3.up * 4 + spread * 2;
            var middleRot = Random.rotation;

            var destPos = to.position + Vector3.up;
            var destRot = Random.rotation;

            EZ.Spawn().Tween(ez =>
            {
                item.Transform.rotation = Quaternion.SlerpUnclamped(srcRot, middleRot, ez.QuadOut);
                item.Transform.position = Helper.LerpParabolic(srcPos, middlePos, ez.QuadOut);
                item.Transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, ez.Linear);
            }).Tween(ez =>
            {
                item.Transform.position = Helper.LerpParabolic(middlePos, destPos, ez.QuadOut);
                item.Transform.rotation = Quaternion.SlerpUnclamped(middleRot, destRot, ez.QuadOut);
                item.Transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, ez.Linear);
            }).Call(_ => { item.Remove(); });
        }
    }
}