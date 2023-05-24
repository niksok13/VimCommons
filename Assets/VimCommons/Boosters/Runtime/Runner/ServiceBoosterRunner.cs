using UnityEngine;
using VimCommons.Boosters.Runtime.Core;
using VimCommons.Boosters.Runtime.UICard;
using VimCore.Runtime.DependencyManagement;

namespace VimCommons.Boosters.Runtime.Runner
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