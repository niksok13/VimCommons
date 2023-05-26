using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using VimCore.Runtime.MVVM;

namespace VimCore.Editor
{
    [CustomEditor(typeof(ASignalEmitter<>),true)]
    [CanEditMultipleObjects]
    public class ModelInvokerArgEditor: UnityEditor.Editor
    {
        private const BindingFlags BindingAttr = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public;

        private ModelBehaviour _cachedModel;
        public override void OnInspectorGUI() => OnInspectorGUI((dynamic)target);

        private void OnInspectorGUI<TPayload>(ASignalEmitter<TPayload> invoker) where TPayload : ISignal
        {
            var model = _cachedModel ??= invoker.GetComponentInParent<ModelBehaviour>();
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
                var info = model.GetType().GetMethods(BindingAttr);
                foreach (var item in info)
                {
                    try
                    {
                        var args = item.GetParameters();
                        if(args.Length!=1)continue;
                        var typeArgument = args[0].ParameterType;
                        if (typeof(TPayload) != typeArgument) continue;
                        if (GUILayout.Button(item.Name))
                        {
                            serializedObject.FindProperty("method").stringValue = item.Name;
                            serializedObject.ApplyModifiedProperties();
                        }
//                            invoker.key = item.Name;
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
        }
    }
}