using System;
using System.Reflection;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using VimPackages.Core.Runtime.MVVM;

namespace VimPackages.Core.Editor
{
    [CustomEditor(typeof(ModelBehaviour),true)]
    [CanEditMultipleObjects]
    public class ModelBehaviourEditor: UnityEditor.Editor
    {
        private bool visible;
        private const BindingFlags BindingAttr = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public;
        public override void OnInspectorGUI()
        {
            if(!PrefabUtility.IsPartOfPrefabInstance(target))
                ComponentUtility.MoveComponentUp(target as ModelBehaviour);
            GUILayout.BeginHorizontal();
            var color = GUI.color;
            GUI.color = Color.green;
            var mode = visible ? "hide" : "show";
            GUILayout.Label("ModelBehaviour");
            GUILayout.FlexibleSpace();
            if (GUILayout.Button($"{mode}")) 
                visible = !visible;
            GUI.color = color;
            GUILayout.EndHorizontal();
            if (visible) 
                DrawModelGUI();

            GUILayout.Space(8);
            DrawDefaultInspector();
        }

        private void DrawModelGUI()
        {
            DrawDataGUI();
            DrawEventGUI();
        }

        private void DrawDataGUI()
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("~Data~");
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            var properties = target.GetType().GetProperties(BindingAttr);
            foreach (var item in properties)
            {
                try
                {
                    var type = item.GetGetMethod(true).ReturnType;
                    if (type.IsEquivalentTo(typeof(ObservableData<>))) continue;
                    var typeArgument = type.GenericTypeArguments[0];
                    GUILayout.BeginHorizontal(GUILayout.Height(20));
                    GUILayout.Label($"{typeArgument.Name}", GUILayout.MinWidth(100));
                    if (GUILayout.Button($"{item.Name}", GUILayout.MinWidth(100)))
                        EditorGUIUtility.systemCopyBuffer = item.Name;
                    GUILayout.EndHorizontal();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            GUILayout.Space(8);
        }

        private void DrawEventGUI()
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("~Signals~");
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            var methods = target.GetType().GetMethods(BindingAttr);
            foreach (var item in methods)
            {
                try
                {
                    var args = item.GetParameters();
                    if (args.Length != 1) continue;
                    var typeArgument = args[0].ParameterType;
                    if (!typeof(ISignal).IsAssignableFrom(typeArgument)) continue;
                    GUILayout.BeginHorizontal(GUILayout.Height(20));
                    GUILayout.Label($"{typeArgument.Name}", GUILayout.MinWidth(100));
                    if (GUILayout.Button($"{item.Name}", GUILayout.MinWidth(100)))
                        EditorGUIUtility.systemCopyBuffer = item.Name;
                    GUILayout.EndHorizontal();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            GUILayout.Space(8);
        }
    }
}