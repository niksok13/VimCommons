using System.Threading.Tasks;
using UnityEngine;
using VimCommons.Looting.Runtime.Core;

namespace VimCommons.Looting.Runtime.Animations
{
    public interface ILootAnimations
    {
        Task Animate(LootEntry prefab, Transform src, Transform dest);
    }
}