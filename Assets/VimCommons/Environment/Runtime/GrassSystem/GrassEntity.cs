using UnityEngine;

namespace VimCommons.Environment.Runtime.GrassSystem
{
    [RequireComponent(typeof(Renderer))]
    public class GrassEntity : MonoBehaviour
    {
        private void Awake()
        {
            transform.Rotate(Vector3.up, Random.Range(-30, 30));
            Destroy(this);
        }
    }
}