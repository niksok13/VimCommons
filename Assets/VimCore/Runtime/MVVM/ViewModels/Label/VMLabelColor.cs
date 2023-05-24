using TMPro;
using UnityEngine;

namespace VimCore.Runtime.MVVM.ViewModels.Label
{
    public class VMLabelColor : AViewModel<Color,TMP_Text>
    {
        protected override void OnValue(Color value)
        {
            Component.color = value;
        }
    }
}