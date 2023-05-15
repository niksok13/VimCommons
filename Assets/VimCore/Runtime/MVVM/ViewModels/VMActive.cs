using UnityEngine;

namespace VimCore.Runtime.MVVM.ViewModels
{
    public class VMActive : AViewModel<bool,Transform>
    {
        public bool invert;
        protected override void OnValue(bool value)
        {
            gameObject.SetActive(value != invert);
        }
    }
}