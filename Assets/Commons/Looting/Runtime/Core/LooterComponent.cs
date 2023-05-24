using System;
using Core.Runtime.DependencyManagement;
using UnityEngine;

namespace Commons.Looting.Runtime.Core
{
    public class LooterComponent: MonoBehaviour
    {
        private static readonly Filter<LooterComponent> Filter = Locator.Filter<LooterComponent>();
        private void OnEnable() => Filter.Add(this);
        private void OnDisable() => Filter.Remove(this);

        private Transform _transform;
        public Transform Transform => _transform ??= transform;

        public float lootDistance = 2;

        public event Action<LootableDefinition> OnLoot;
        internal void Loot(LootableDefinition type) => OnLoot?.Invoke(type);
    }
}