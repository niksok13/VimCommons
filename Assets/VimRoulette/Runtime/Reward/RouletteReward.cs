using System;
using UnityEngine;

namespace VimRoulette.Runtime.Reward
{
    [CreateAssetMenu]
    public class RouletteReward: ScriptableObject
    {
        public string title;
        public Sprite icon;
        public Action OnReward;
        
        public virtual void Apply()
        {
            OnReward?.Invoke();
            Debug.Log("reward applied");   
        }
    }
}