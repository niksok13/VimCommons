using System.Threading.Tasks;
using UnityEngine;
using VimLooting.Runtime.Core;

namespace VimLooting.Runtime.Animations
{
    public interface ILootAnimations
    {
        Task Animate(LootEntry prefab, Transform src, Transform dest);
    }
}