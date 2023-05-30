using UnityEngine;

namespace VimCore.Runtime.Utils
{
    public class DestroyOnAnyKey : MonoBehaviour
    {
        private void Update()
        {
            if(Input.anyKey)
                Destroy(gameObject);
        }
    }
}
