using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(TimeLine))]
public class TimeLineDrawer : PropertyDrawer
{
    SerializedProperty name;
    SerializedProperty test;

    int linesCount = 2;

    private void OnEnable()
    {



    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight * linesCount + EditorGUIUtility.standardVerticalSpacing * (linesCount - 1);
    }

    public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
    {
        name = property.FindPropertyRelative("name");
        test = property.FindPropertyRelative("test");
        Rect nameRect = new Rect(0, 0, 50, 50);
        EditorGUI.PropertyField(nameRect, name);
        EditorGUILayout.PropertyField(test);

        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("+", GUILayout.Height(50), GUILayout.Width(50))) return;

        EditorGUILayout.LabelField("hey");

        EditorGUILayout.BeginHorizontal();
    }
}
