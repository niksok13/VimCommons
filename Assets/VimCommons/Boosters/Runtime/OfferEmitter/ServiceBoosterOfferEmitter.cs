using System.Linq;
using UnityEngine;
using VimCommons.Boosters.Runtime.UICard;
using VimCore.Runtime.DependencyManagement;
using VimCore.Runtime.Utils;

namespace VimCommons.Boosters.Runtime.OfferEmitter
{
    public class ServiceBoosterOfferEmitter : MonoBehaviour
    {
        private static readonly Filter<ModelBoosterCard> Cards = Locator.Filter<ModelBoosterCard>();
        public float firstTimeout;
        public float timeout;

        private float _timer;

        private void Awake()
        {
            _timer = firstTimeout;
        }

        private void Update()
        {
            _timer -= Time.deltaTime;
            if (_timer > 0) return;
            _timer = timeout;
            EmitRandomOffer();
        }

        private void EmitRandomOffer()
        {
            var valid = Cards.Where(v => v.CanOffer).ToArray();
            if (valid.Length < 1) return;
            var card = valid.GetRandomItem();
            card.ShowOffer();
        }
    }
}