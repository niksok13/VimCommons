using UnityEngine;

namespace Core.Runtime.Utils
{
    public class ComponentDestroyOnAnyKey : MonoBehaviour
    {
        private void Update()
        {
            if(UnityEngine.Input.anyKey)
                Destroy(gameObject);
        }
    }
}
