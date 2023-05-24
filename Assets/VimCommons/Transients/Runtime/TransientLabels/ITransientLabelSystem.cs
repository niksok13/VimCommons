using UnityEngine;

namespace VimCommons.Transients.Runtime.TransientLabels
{
    public interface ITransientLabelSystem
    {
        void Emit(Vector3 position, string text, Color color = default, float size = 4);
    }
}