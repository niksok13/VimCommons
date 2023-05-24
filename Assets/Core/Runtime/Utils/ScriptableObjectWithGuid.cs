using UnityEditor;
using UnityEngine;

namespace Core.Runtime.Utils
{
    public abstract class ScriptableObjectWithGuid : ScriptableObject
    {
        [HideInInspector]
        public string guid;

#if UNITY_EDITOR
        private void OnValidate()
        {
            AssetDatabase.TryGetGUIDAndLocalFileIdentifier(this, out guid, out long _); 
            EditorUtility.SetDirty(this);
        }
#endif
    }
}