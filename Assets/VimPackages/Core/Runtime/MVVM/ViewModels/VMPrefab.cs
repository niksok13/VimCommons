using UnityEngine;

namespace VimPackages.Core.Runtime.MVVM.ViewModels
{
    public class VMPrefab : AViewModel<GameObject, Transform>
    {
        private GameObject _child;
        protected override void OnValue(GameObject value)
        {
            if(_child)
                Destroy(_child);
            if (!value) return;
            _child = Instantiate(value, transform);
            _child.transform.localPosition = Vector3.zero;
            _child.transform.localRotation = Quaternion.identity;
            _child.transform.localScale = Vector3.one;
        }
    }
}
