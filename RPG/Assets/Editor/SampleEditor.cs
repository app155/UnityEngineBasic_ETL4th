using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(Sample))]
public class SampleEditor : Editor
{
    Sample sample;
    Object other;

    GUILayoutOption[] options = new GUILayoutOption[]
    {
        GUILayout.Height(40.0f),
        GUILayout.ExpandWidth(true),
    };

    private void OnEnable()
    {
        sample = (Sample)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GUIStyle tmpStyle = new GUIStyle(EditorStyles.label);

        EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal(tmpStyle, options);

            if (GUILayout.Button("Set Random Number"))
            {
                sample.num = Random.Range(0, 100);
            }

            EditorGUILayout.IntField("Number", sample.num);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal(tmpStyle, options);
            sample.state = (States)EditorGUILayout.EnumPopup("Current State", sample.state);
            EditorGUILayout.EndHorizontal();

        other = EditorGUILayout.ObjectField(other, typeof(GameObject), other);

        EditorGUILayout.EndVertical();

        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif