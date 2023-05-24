using System;
using System.Collections.Generic;
using UnityEngine;
using VimCommons.Looting.Runtime.Core;

namespace VimCommons.Progression.Runtime.Node
{
    [Serializable]
    public class NodeLevelInfo
    {
        public List<LootEntry> cost;

        public string BuildUpgradeLabel()
        {
            if (cost == null) return "";
            var result = "";
            foreach (var entry in cost)
            {
                if (entry.amount < 1) continue;
                result += $"{entry.amount}<sprite name={entry.type.iconLabel}>\n";
            }
            return result.Trim();
        }

        public string BuildRewardedBonus(float part = 1)
        {
            if (cost == null) return "";
            var result = "";
            foreach (var entry in cost)
            {
                if (entry.amount < 1) continue;
                result += $"{Mathf.CeilToInt(entry.amount * part)}<sprite name={entry.type.ToString().ToLower()}>\n";
            }
            return result.Trim();
        }
    }
}