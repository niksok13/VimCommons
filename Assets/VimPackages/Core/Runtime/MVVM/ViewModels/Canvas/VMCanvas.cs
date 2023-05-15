using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace VimPackages.Core.Runtime.MVVM.ViewModels.Canvas
{
    
    public class VMCanvas : AViewModel<bool,UnityEngine.Canvas>
    {
        protected CanvasGroup Cg;
        protected CanvasScaler Cs;
        protected BaseRaycaster Gr;

        private void Awake()
        {
            if(!gameObject.TryGetComponent(out Cg))
                Cg = gameObject.AddComponent<CanvasGroup>();
            gameObject.TryGetComponent(out Cs);
            gameObject.TryGetComponent(out Gr);
            Component.enabled = false;
            Cg.enabled = false;
            if(Cs)
                Cs.enabled = false;
            if (Gr) 
                Gr.enabled = false;
        }

        protected override void OnValue(bool value)
        {
            if(Component.enabled == value) return;
            Component.enabled = value;
            Cg.enabled = value;
            if(Cs)
                Cs.enabled = value;
            if (Gr) 
                Gr.enabled = value;
        }
    }
}