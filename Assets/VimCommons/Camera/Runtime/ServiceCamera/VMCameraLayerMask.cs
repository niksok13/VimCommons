using UnityEngine;
using VimCore.Runtime.MVVM;

namespace VimCommons.Camera.Runtime.ServiceCamera
{
    public class VMCameraLayerMask : AViewModel<bool,UnityEngine.Camera>
    {
        public LayerMask onTrue, onFalse;
    
        protected override void OnValue(bool value)
        {
            Component.cullingMask = value ? onTrue : onFalse;
        }
    }
}
