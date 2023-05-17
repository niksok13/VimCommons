using UnityEngine;
using VimBoosters.Runtime.Core;
using VimBoosters.Runtime.UICard;
using VimCore.Runtime.DependencyManagement;

namespace VimBoosters.Runtime.Runner
{
    public class ServiceBoosterRunner : MonoBehaviour
    {
        
        private static readonly Filter<Booster> Boosters = Locator.Filter<Booster>();
        private static readonly Filter<ModelBoosterCard> Cards = Locator.Filter<ModelBoosterCard>();

        private void Update()
        {
            foreach (var booster in Boosters) booster.Tick();
            foreach (var card in Cards) card.Tick();
        }
    }
}