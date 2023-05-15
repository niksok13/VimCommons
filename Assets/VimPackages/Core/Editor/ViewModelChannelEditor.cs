using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using VimPackages.Core.Runtime.MVVM;

namespace VimPackages.Core.Editor
{
    [CustomEditor(typeof(AViewModel<>),true)]
    [CanEditMultipleObjects]
    public class ViewModelChannelEditor: UnityEditor.Editor
    {

        private const BindingFlags BindingAttr = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public;

        private ModelBehaviour _cachedModel;
        public override void OnInspectorGUI() => OnInspectorGUI((dynamic)target);

        private void OnInspectorGUI<TComponent>(AViewModel<TComponent> viewModel)
        {
            var model = _cachedModel ??= viewModel.GetComponentInParent<ModelBehaviour>();
            if (model)
            {
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                var color = GUI.color;
                GUI.color = Color.green;
                if (GUILayout.Button("Model found")) 
                    EditorGUIUtility.PingObject(model);
                GUI.color = color;
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
                GUILayout.Space(4);
                var info = model.GetType().GetProperties(BindingAttr);
                foreach (var item in info)
                {
                    try
                    {
                        var type = item.GetGetMethod(true).ReturnType;
                        if (type != typeof(ObservableChannel)) continue;
                        if (GUILayout.Button(item.Name))
                        {
                            serializedObject.FindProperty("property").stringValue = item.Name;
                            serializedObject.ApplyModifiedProperties();
                        }
//                            viewModel.property = item.Name;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
            else
            {
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                var color = GUI.color;
                GUI.color = Color.red;
                GUILayout.Label("Model not found");
                GUI.color = color;
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
            }
            GUILayout.Space(4);
            DrawDefaultInspector();
            if (GUILayout.Button("Play"))
            {
                viewModel.OnSignal();
            }        
        }
    }
}