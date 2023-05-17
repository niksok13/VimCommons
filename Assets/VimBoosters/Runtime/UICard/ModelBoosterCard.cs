using System;
using UnityEngine;
using VimBoosters.Runtime.Core;
using VimCore.Runtime.DependencyManagement;
using VimCore.Runtime.MVVM;
using VimCore.Runtime.MVVM.ViewModels.Input;

namespace VimBoosters.Runtime.UICard
{
    public class ModelBoosterCard : ModelBehaviour
    {
        public Booster booster;
        public bool offerAllowed = true;
        public float offerDuration = 30;
        public float rewardedDuration = 60;
        
        private static readonly Filter<ModelBoosterCard> Filter = Locator.Filter<ModelBoosterCard>();
        private void OnEnable() => Filter.Add(this);
        private void OnDisable() => Filter.Remove(this);
        
        public ObservableData<Sprite> Icon { get; } = new();
        public ObservableData<string> Title { get; } = new();

        public ObservableData<bool> Collapse { get; } = new();

        public ObservableData<bool> BoosterActive { get; } = new();
        public ObservableData<string> BoosterEstimate { get; } = new();

        public ObservableData<bool> OfferActive { get; } = new();
        public ObservableData<float> OfferProgress { get; } = new();


        private float _offerEstimate;
        
        private void Awake()
        {
            Icon.Value = booster.icon;
            Title.Value = booster.title;
        }

        public void Tick()
        {
            BoosterActive.Value = booster.Active;
            if (BoosterActive.Value)
            {
                var span = TimeSpan.FromSeconds(booster.Estimate);
                var time = new DateTime(span.Ticks);
                BoosterEstimate.Value = time.ToString("mm:ss");
            }
            OfferActive.Value = _offerEstimate > 0;
            if (OfferActive.Value)
            {
                _offerEstimate -= Time.deltaTime;
                OfferProgress.Value = _offerEstimate / offerDuration;
            }

            Collapse.Value = !booster.Active && !(_offerEstimate > 0);
        }
        
        public bool CanOffer => offerAllowed && !booster.Active && !(_offerEstimate > 0);

        public void ShowOffer()
        {
            if(CanOffer)
                _offerEstimate = offerDuration;
        }

        private void Activate(SignalClick _)
        {
            _offerEstimate = 0;
            booster.AddSeconds(rewardedDuration);
        }
    }
}