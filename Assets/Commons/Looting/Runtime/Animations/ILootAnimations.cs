using System.Threading.Tasks;
using Commons.Looting.Runtime.Core;
using UnityEngine;

namespace Commons.Looting.Runtime.Animations
{
    public interface ILootAnimations
    {
        Task Animate(LootEntry prefab, Transform src, Transform dest);
    }
}