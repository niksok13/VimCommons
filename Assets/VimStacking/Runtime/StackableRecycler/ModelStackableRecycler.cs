using UnityEngine;
using VimStacking.Runtime.Stackable;

namespace VimStacking.Runtime.StackableRecycler
{
    public class ModelStackableRecycler : MonoBehaviour
    {
        public StackableDefinition[] recyclable;
        private UnstackPoint _point;
        private UnstackPoint Point => _point ??= GetComponentInChildren<UnstackPoint>();

        private void Awake()
        {
            Point.Needs = recyclable;
        }
    }
}
