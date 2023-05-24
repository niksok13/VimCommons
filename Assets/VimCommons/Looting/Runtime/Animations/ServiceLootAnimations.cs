using System.Threading.Tasks;
using Commons.Looting.Runtime.Core;
using Core.Runtime.DependencyManagement;
using Core.Runtime.EZTween;
using Core.Runtime.Utils;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Commons.Looting.Runtime.Animations
{
    public class ServiceLootAnimations: MonoBehaviour, ILootAnimations
    {
        private static readonly ServiceContainer<ILootAnimations> Container = Locator.Single<ILootAnimations>();
        private void Awake() => Container.Attach(this);
        private void OnDestroy() => Container.Detach(this);


        public async Task Animate(LootEntry entry, Transform src, Transform dest)
        {
            var count = Mathf.Min(entry.amount, 50);
            var delay = 100;
            for (var i = 0; i < count; i++)
            {
                await UniTask.Delay(delay, DelayType.UnscaledDeltaTime);
                delay -= delay/5;
                Animate(entry.type, src, dest);
            }
        }
        
        private static void Animate(LootableDefinition type, Transform src, Transform dest)
        {
            var item = type.Spawn();
            
            var srcPos = src.position + Vector3.up;
            var srcRot = Random.rotation;

            item.Transform.position = srcPos;
            item.Transform.rotation = srcRot;
            item.Transform.localScale = Vector3.zero;

            var spread = Random.insideUnitSphere;
            spread *= spread.magnitude;

            var middlePos = Vector3.Lerp(src.position, dest.position, 0.5f) + Vector3.up * 4 + spread * 2;
            var middleRot = Random.rotation;
            
            var destPos = dest.position+Vector3.up;
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
            }).Call(_ =>
            {
                item.Remove();
            });
        }
    }
}