using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WavesCreator : EditorWindow
{

    private static WavesCreatorData profile;
    static string profileToLoad;

    [MenuItem("EX NIHILO/WavesCreator")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        WavesCreator window = (WavesCreator)GetWindow(typeof(WavesCreator));
        window.Show();
    }

    SerializedProperty second;
    SerializedProperty prefab;
    bool loadNewProfile = true;

    string profileName = "";

    private void OnEnable()
    {
        this.minSize = new Vector2(600, 500);
    }


    private void OnGUI()
    {
        GUI.skin.font = AssetDatabase.LoadAssetAtPath("Assets/Fonts/MankSans-Medium.ttf", typeof(Font)) as Font;
        
        string commandName = Event.current.commandName;

        #region Profiles
        GUILayout.BeginHorizontal();
        

            GUI.color = Color.cyan;

            if (GUILayout.Button("Load Profile", Style(GUI.skin.button, fontStyle: FontStyle.Bold), BoundsMin(150, 40)))
            {
                int controlID = EditorGUIUtility.GetControlID(FocusType.Passive);
                EditorGUIUtility.ShowObjectPicker<WavesCreatorData>(null, false, "", controlID);
            }


            if (commandName == "ObjectSelectorUpdated")
            {
                profile = EditorGUIUtility.GetObjectPickerObject() as WavesCreatorData;
                Repaint();
            }
            else if (commandName == "ObjectSelectorClosed")
            {
                profile = EditorGUIUtility.GetObjectPickerObject() as WavesCreatorData;
            }




            GUI.color = (profileName.Length > 0 ? Color.green : Color.red);
            if (GUILayout.Button("Create NEW Profile", Style(GUI.skin.button, fontStyle: FontStyle.Bold), BoundsMin(150, 40)))
            {

                WavesCreatorData temp = CreateInstance<WavesCreatorData>();

                if (loadNewProfile)
                {
                    profileToLoad = profileName;
                    profile = temp;
                }

                AssetDatabase.CreateAsset(temp, "Assets/"+ profileName + ".asset");
                AssetDatabase.Refresh();
            }
            GUI.color = Color.white;

            profileName = GUILayout.TextField(profileName, Style(GUI.skin.textField, TextAnchor.MiddleCenter), BoundsMin(100,40));

            GUILayout.BeginVertical();

                EditorGUILayout.LabelField("Current Profile = " + (profile != null ? profile.name : "NONE"));

                GUILayout.BeginHorizontal();

                    LockWidth(55);  
        
                    EditorGUILayout.LabelField("Load New Profile", Style(GUI.skin.label, TextAnchor.MiddleLeft), BoundsMin(110, expandWidth: false));

                    UnlockWidth();

                    loadNewProfile = EditorGUILayout.Toggle("", loadNewProfile, Style(GUI.skin.toggle, TextAnchor.MiddleLeft));

                GUILayout.EndHorizontal();

            GUILayout.EndVertical();
       
        GUILayout.EndHorizontal();
        
        #endregion

        if (profile == null) return;






        SerializedObject serializedObject = new SerializedObject(profile);
        serializedObject.Update();



        EditorGUILayout.Space(10);


        second = serializedObject.FindProperty("second");
        EditorGUILayout.PropertyField(second);
        prefab = serializedObject.FindProperty("prefab");
        EditorGUILayout.PropertyField(prefab);

        serializedObject.ApplyModifiedProperties();
    }



    [InitializeOnLoadMethod]
    private static void OnLoad()
    {
        profile = AssetDatabase.LoadAssetAtPath<WavesCreatorData>("Assets/"+ profileToLoad + ".asset");

        if (profile == null)
        {
            profile = CreateInstance<WavesCreatorData>();
            AssetDatabase.CreateAsset(profile, "Assets/WC-Profile.asset");
            AssetDatabase.Refresh();
        }
    }

    public GUILayoutOption[] BoundsMin(float minWidth = 0, float minHeight = 0, bool expandWidth = true, bool expandHeight = false)
    {
        return new GUILayoutOption[]
        {
            GUILayout.MinWidth(minWidth),
            GUILayout.MinHeight(minHeight),
            GUILayout.ExpandWidth(expandWidth),
            GUILayout.ExpandHeight(expandHeight),
        };
    }

    public GUILayoutOption[] BoundsMax(float maxWidth = 0, float maxHeight = 0, bool expandWidth = true, bool expandHeight = false)
    {
        return new GUILayoutOption[]
        {
            GUILayout.MaxWidth(maxWidth),
            GUILayout.MaxHeight(maxHeight),
            GUILayout.ExpandWidth(expandWidth),
            GUILayout.ExpandHeight(expandHeight),
        };
    }


    public GUIStyle Style(GUIStyle baseStyle = null, TextAnchor alignment = TextAnchor.MiddleCenter, FontStyle fontStyle = FontStyle.Normal, int fontSize = 12)
    {
        GUIStyle style = new GUIStyle(GUI.skin.label);

        if (baseStyle != null) style = new GUIStyle(baseStyle);

        style.alignment = alignment;
        style.fontStyle = fontStyle;
        style.fontSize = fontSize;
        return style;
    }

    public void LockWidth(int value)
    {
        EditorGUIUtility.labelWidth = value;
    }

    public void UnlockWidth()
    {
        EditorGUIUtility.labelWidth = 150;
    }
}