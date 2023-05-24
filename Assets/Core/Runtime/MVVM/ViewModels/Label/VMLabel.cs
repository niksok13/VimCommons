using TMPro;
using UnityEngine;

namespace Core.Runtime.MVVM.ViewModels.Label
{
    public class VMLabel : AViewModel<string, TMP_Text>
    {
        [TextArea]
        public string mask = "{0}";

        private void Awake()
        {
            Component.isTextObjectScaleStatic = true;
        }
        
        protected override void OnValue(string value)
        {
            if (Component)
                Component.text = string.Format(mask, value);
            Component.isTextObjectScaleStatic = false;
        }
    }
}