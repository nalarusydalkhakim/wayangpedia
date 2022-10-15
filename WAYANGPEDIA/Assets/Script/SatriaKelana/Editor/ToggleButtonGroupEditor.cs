using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SatriaKelana
{
    [CustomEditor(typeof(ToggleButtonGroup))]
    public class ToggleButtonGroupEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            base.OnInspectorGUI();
            if (GUILayout.Button("Get from children"))
            {
                var group = target as ToggleButtonGroup;
                var transform = group.transform;
                var length = transform.childCount;
                for (int i = 0; i < length; i++)
                {
                    var toggle = transform.GetChild(i).GetComponent<ToggleButton>();
                    if (toggle == null) break;
                    if (group.Toggles.Contains(toggle)) continue;
                    group.Toggles.Add(toggle);
                }
                EditorUtility.SetDirty(target);
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}