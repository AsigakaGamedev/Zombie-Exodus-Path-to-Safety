using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(UIEffectsManager))]
public class EffectsManagerEditor : Editor
{
    private SerializedProperty settings;
    private ReorderableList list;
    private int selectedElement;
    private UIEffectsManager Target;

    private void OnEnable()
    {
        Target = (UIEffectsManager)target;
        settings = serializedObject.FindProperty("Settings");
        list = new ReorderableList(serializedObject, settings, true, true, false, false);
        selectedElement = -1;
        list.onSelectCallback = OnSelected;
        list.drawHeaderCallback = DrawHeader;
        list.elementHeightCallback = DrawElementHeight;
        list.drawElementCallback = DrawListItems;
    }

    private struct TransformData
    {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        list.DoLayoutList();
        if (GUILayout.Button("Manage Effects"))
        {
            UIEM.NodeBasedEditor.OpenWindow(Target);
        }
        if (GUILayout.Button("Paste Vector3"))
        {
            GenericMenu menu = new GenericMenu();
            try
            {
                string[] coords = EditorGUIUtility.systemCopyBuffer.TrimStart("Vector3(".ToCharArray()).Split(new char[] { ',', ')' });
                DisplayMenu(menu, new Vector3(float.Parse(coords[0]), float.Parse(coords[1]), float.Parse(coords[2])));
            }
            catch
            {
                try
                {
                    TransformData tdata = JsonUtility.FromJson<TransformData>(EditorGUIUtility.systemCopyBuffer.TrimStart("UnityEditor.TransformWorldPlacementJSON:".ToCharArray()));
                    DisplayMenu(menu, tdata.position);
                }
                catch
                {
                    menu.AddDisabledItem(new GUIContent("Paste to Start Vector3"));
                    menu.AddDisabledItem(new GUIContent("Paste to Target Vector3"));
                }
            }
            menu.ShowAsContext();
        }

        serializedObject.ApplyModifiedProperties();
        if (GUI.changed && !Application.isPlaying)
        {
            EditorUtility.SetDirty(target);
            EditorSceneManager.MarkSceneDirty(Target.gameObject.scene);
        }
    }

    void DisplayMenu(GenericMenu menu, Vector3 value)
    {
        if (selectedElement != -1)
        {
            menu.AddItem(new GUIContent("Paste to Start Vector3"), false, () => { list.serializedProperty.GetArrayElementAtIndex(selectedElement).FindPropertyRelative("startVector").vector3Value = value; serializedObject.ApplyModifiedProperties(); });
            menu.AddItem(new GUIContent("Paste to Target Vector3"), false, () => { list.serializedProperty.GetArrayElementAtIndex(selectedElement).FindPropertyRelative("targetVector").vector3Value = value; serializedObject.ApplyModifiedProperties(); });
        }
        else
        {
            menu.AddDisabledItem(new GUIContent("Select an effect first!"));
        }
    }

    void OnSelected (ReorderableList l)
    {
        selectedElement = l.index;
    }

    void DrawHeader(Rect rect)
    {
        string name = "Effects Settings";
        EditorGUI.LabelField(rect, name);
    }

    float DrawElementHeight (int index)
    {
        SerializedProperty element = list.serializedProperty.GetArrayElementAtIndex(index);

        float lines = 1.05f;
        float elementSpace = 0.0f;
        if (element.FindPropertyRelative("showSettings").boolValue)
        {
            if (element.FindPropertyRelative("EffectType").enumValueIndex <= 4)
            {
                elementSpace += 25f;
                if (element.FindPropertyRelative("EffectType").enumValueIndex == 1)
                {
                    elementSpace += 25f;
                    if (element.FindPropertyRelative("RotationType").enumValueIndex == 1)
                    {
                        elementSpace += 25f;
                    }
                    else if (element.FindPropertyRelative("initialState").enumValueIndex == 1)
                    {
                        elementSpace += 25f;
                    }
                }
                else if (element.FindPropertyRelative("initialState").enumValueIndex == 1)
                {
                    elementSpace += 25f;
                }
            }
            else if (element.FindPropertyRelative("EffectType").enumValueIndex == 6)
                elementSpace += 25f;

            element.FindPropertyRelative("spacing").floatValue = elementSpace;
            lines = 25f + (elementSpace / 17f) + ((Target.Settings[index].OnStart.GetPersistentEventCount() > 0 ? Target.Settings[index].OnStart.GetPersistentEventCount() - 1 : 0) + (Target.Settings[index].OnFinished.GetPersistentEventCount() > 0 ? Target.Settings[index].OnFinished.GetPersistentEventCount() - 1 : 0)) * 2.75f;
        }
        return lines * 17f;
    }

    void DrawListItems(Rect rect, int index, bool isActive, bool isFocused)
    {
        SerializedProperty element = list.serializedProperty.GetArrayElementAtIndex(index);

        element.FindPropertyRelative("showSettings").boolValue = EditorGUI.Foldout(new Rect(rect.x + 10, rect.y, 100, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("showSettings").boolValue, element.FindPropertyRelative("Name").stringValue);
        if (element.FindPropertyRelative("showSettings").boolValue)
        {
            EditorGUI.LabelField(new Rect(rect.x, rect.y + 20, 100, EditorGUIUtility.singleLineHeight), "Target Obj");
            EditorGUI.PropertyField(
                new Rect(rect.x + 75, rect.y + 20, 55, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("TargetType"),
                GUIContent.none
            );
            if (element.FindPropertyRelative("TargetType").enumValueIndex == 1)
            {
                EditorGUI.PropertyField(
                    new Rect(rect.x + 135, rect.y + 20, 75, EditorGUIUtility.singleLineHeight),
                    element.FindPropertyRelative("targetObj"),
                    GUIContent.none
                );
            }

            EditorGUI.LabelField(new Rect(rect.x, rect.y + 45, 100, EditorGUIUtility.singleLineHeight), "Effect Type");
            EditorGUI.PropertyField(
                new Rect(rect.x + 75, rect.y + 45, 100, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("EffectType"),
                GUIContent.none
            );

            EditorGUI.LabelField(new Rect(rect.x, rect.y + 70, 100, EditorGUIUtility.singleLineHeight), "RunAtStart");
            EditorGUI.PropertyField(
                new Rect(rect.x + 75, rect.y + 70, 40, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("RunAtStart"),
                GUIContent.none
            );

            EditorGUI.LabelField(new Rect(rect.x + 150, rect.y + 70, 100, EditorGUIUtility.singleLineHeight), "RunOnEnable");
            EditorGUI.PropertyField(
                new Rect(rect.x + 250, rect.y + 70, 40, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("RunOnEnable"),
                GUIContent.none
            );

            EditorGUI.LabelField(new Rect(rect.x, rect.y + 95, 100, EditorGUIUtility.singleLineHeight), "Loop");
            EditorGUI.PropertyField(
                new Rect(rect.x + 75, rect.y + 95, 40, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("Loop"),
                GUIContent.none
            );

            EditorGUI.LabelField(new Rect(rect.x, rect.y + 120, 100, EditorGUIUtility.singleLineHeight), "Delay");
            EditorGUI.PropertyField(
                new Rect(rect.x + 60, rect.y + 120, 50, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("Delay"),
                GUIContent.none
            );

            EditorGUI.LabelField(new Rect(rect.x, rect.y + 145, 100, EditorGUIUtility.singleLineHeight), "Speed");
            if (element.FindPropertyRelative("Speed").floatValue == 0)
                element.FindPropertyRelative("Speed").floatValue = 5.0f;
            element.FindPropertyRelative("Speed").floatValue =
            EditorGUI.Slider(new Rect(rect.x + 50, rect.y + 145, 150, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("Speed").floatValue, 0.2f, 10.0f);

            float space = element.FindPropertyRelative("spacing").floatValue;

            switch (element.FindPropertyRelative("EffectType").enumValueIndex)
            {
                case 0:
                    EditorGUI.LabelField(new Rect(rect.x, rect.y + 170, 100, EditorGUIUtility.singleLineHeight), "Start Position");
                    EditorGUI.PropertyField(
                        new Rect(rect.x + 90, rect.y + 170, 100, EditorGUIUtility.singleLineHeight),
                        element.FindPropertyRelative("initialState"),
                        GUIContent.none
                    );
                    if (element.FindPropertyRelative("initialState").enumValueIndex == 1)
                    {
                        EditorGUI.PropertyField(
                            new Rect(rect.x + 10, rect.y + 195, 200, EditorGUIUtility.singleLineHeight),
                            element.FindPropertyRelative("startVector"),
                            GUIContent.none
                        );
                    }

                    EditorGUI.LabelField(new Rect(rect.x, rect.y + 170 + space, 100, EditorGUIUtility.singleLineHeight), "Target Position");
                    EditorGUI.PropertyField(
                        new Rect(rect.x + 10, rect.y + 195 + space, 200, EditorGUIUtility.singleLineHeight),
                        element.FindPropertyRelative("targetVector"),
                        GUIContent.none
                    );
                    break;
                case 1:
                    EditorGUI.LabelField(new Rect(rect.x, rect.y + 170, 100, EditorGUIUtility.singleLineHeight), "Rotation Type");
                    EditorGUI.PropertyField(
                        new Rect(rect.x + 90, rect.y + 170, 100, EditorGUIUtility.singleLineHeight),
                        element.FindPropertyRelative("RotationType"),
                        GUIContent.none
                    );
                    if (element.FindPropertyRelative("RotationType").enumValueIndex == 1)
                    {
                        EditorGUI.LabelField(new Rect(rect.x, rect.y + 195, 100, EditorGUIUtility.singleLineHeight), "Direction");
                        EditorGUI.PropertyField(
                           new Rect(rect.x + 75, rect.y + 195, 100, EditorGUIUtility.singleLineHeight),
                           element.FindPropertyRelative("RotationDirection"),
                           GUIContent.none
                       );

                        EditorGUI.LabelField(new Rect(rect.x, rect.y + 220, 100, EditorGUIUtility.singleLineHeight), "Duration");
                        EditorGUI.PropertyField(
                            new Rect(rect.x + 60, rect.y + 220, 70, EditorGUIUtility.singleLineHeight),
                            element.FindPropertyRelative("Duration"),
                            GUIContent.none
                        );
                    }
                    else
                    {
                        EditorGUI.LabelField(new Rect(rect.x, rect.y + 195, 100, EditorGUIUtility.singleLineHeight), "Start Rotation");
                        EditorGUI.PropertyField(
                            new Rect(rect.x + 90, rect.y + 195, 100, EditorGUIUtility.singleLineHeight),
                            element.FindPropertyRelative("initialState"),
                            GUIContent.none
                        );
                        if (element.FindPropertyRelative("initialState").enumValueIndex == 1)
                        {
                            EditorGUI.PropertyField(
                                new Rect(rect.x + 10, rect.y + 145 + space, 200, EditorGUIUtility.singleLineHeight),
                                element.FindPropertyRelative("startVector"),
                                GUIContent.none
                            );
                        }
                    }

                    EditorGUI.LabelField(new Rect(rect.x, rect.y + 170 + space, 100, EditorGUIUtility.singleLineHeight), "Target Rotation");
                    EditorGUI.PropertyField(
                        new Rect(rect.x + 10, rect.y + 195 + space, 200, EditorGUIUtility.singleLineHeight),
                        element.FindPropertyRelative("targetVector"),
                        GUIContent.none
                    );
                    break;
                case 2:
                    EditorGUI.LabelField(new Rect(rect.x, rect.y + 170, 100, EditorGUIUtility.singleLineHeight), "Start Scale");
                    EditorGUI.PropertyField(
                        new Rect(rect.x + 80, rect.y + 170, 100, EditorGUIUtility.singleLineHeight),
                        element.FindPropertyRelative("initialState"),
                        GUIContent.none
                    );
                    if (element.FindPropertyRelative("initialState").enumValueIndex == 1)
                    {
                        element.FindPropertyRelative("startVector").vector3Value = EditorGUI.Vector2Field(
                            new Rect(rect.x + 10, rect.y + 195, 130, EditorGUIUtility.singleLineHeight),
                            "",
                            element.FindPropertyRelative("startVector").vector3Value
                        );
                    }

                    EditorGUI.LabelField(new Rect(rect.x, rect.y + 170 + space, 100, EditorGUIUtility.singleLineHeight), "Target Scale");
                    element.FindPropertyRelative("targetVector").vector3Value = EditorGUI.Vector2Field(
                        new Rect(rect.x + 10, rect.y + 195 + space, 130, EditorGUIUtility.singleLineHeight),
                        "",
                        element.FindPropertyRelative("targetVector").vector3Value
                    );
                    break;
                case 3:
                    EditorGUI.LabelField(new Rect(rect.x, rect.y + 170, 100, EditorGUIUtility.singleLineHeight), "Fade Type");
                    EditorGUI.PropertyField(
                        new Rect(rect.x + 75, rect.y + 170, 100, EditorGUIUtility.singleLineHeight),
                        element.FindPropertyRelative("FadeType"),
                        GUIContent.none
                    );

                    EditorGUI.LabelField(new Rect(rect.x, rect.y + 195, 100, EditorGUIUtility.singleLineHeight), "Start Alpha");
                    EditorGUI.PropertyField(
                        new Rect(rect.x + 90, rect.y + 195, 100, EditorGUIUtility.singleLineHeight),
                        element.FindPropertyRelative("initialState"),
                        GUIContent.none
                    );
                    if (element.FindPropertyRelative("initialState").enumValueIndex == 1)
                    {
                        element.FindPropertyRelative("startAlpha").floatValue =
                        EditorGUI.Slider(new Rect(rect.x + 10, rect.y + 220, 150, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("startAlpha").floatValue, 0.0f, 1.0f);
                    }

                    EditorGUI.LabelField(new Rect(rect.x, rect.y + 195 + space, 100, EditorGUIUtility.singleLineHeight), "ApplyToChildren");
                    EditorGUI.PropertyField(
                        new Rect(rect.x + 110, rect.y + 195 + space, 40, EditorGUIUtility.singleLineHeight),
                        element.FindPropertyRelative("ApplyToChildren"),
                        GUIContent.none
                    );
                    break;
                case 4:
                    EditorGUI.LabelField(new Rect(rect.x, rect.y + 170, 100, EditorGUIUtility.singleLineHeight), "Start Color");
                    EditorGUI.PropertyField(
                        new Rect(rect.x + 90, rect.y + 170, 100, EditorGUIUtility.singleLineHeight),
                        element.FindPropertyRelative("initialState"),
                        GUIContent.none
                    );
                    if (element.FindPropertyRelative("initialState").enumValueIndex == 1)
                    {
                        EditorGUI.PropertyField(
                            new Rect(rect.x + 10, rect.y + 195, 100, EditorGUIUtility.singleLineHeight),
                            element.FindPropertyRelative("startColor"),
                            GUIContent.none
                        );
                    }

                    EditorGUI.LabelField(new Rect(rect.x, rect.y + 170 + space, 100, EditorGUIUtility.singleLineHeight), "Target Color");
                    EditorGUI.PropertyField(
                        new Rect(rect.x + 85, rect.y + 170 + space, 100, EditorGUIUtility.singleLineHeight),
                        element.FindPropertyRelative("color"),
                        GUIContent.none
                    );

                    EditorGUI.LabelField(new Rect(rect.x, rect.y + 195 + space, 100, EditorGUIUtility.singleLineHeight), "ApplyToChildren");
                    EditorGUI.PropertyField(
                        new Rect(rect.x + 110, rect.y + 195 + space, 40, EditorGUIUtility.singleLineHeight),
                        element.FindPropertyRelative("ApplyToChildren"),
                        GUIContent.none
                    );
                    break;
                case 5:
                    EditorGUI.LabelField(new Rect(rect.x, rect.y + 170, 100, EditorGUIUtility.singleLineHeight), "Color");
                    EditorGUI.PropertyField(
                        new Rect(rect.x + 70, rect.y + 170, 100, EditorGUIUtility.singleLineHeight),
                        element.FindPropertyRelative("color"),
                        GUIContent.none
                    );

                    EditorGUI.LabelField(new Rect(rect.x, rect.y + 195, 120, EditorGUIUtility.singleLineHeight), "Brightness Duration");
                    EditorGUI.PropertyField(
                        new Rect(rect.x + 125, rect.y + 195, 50, EditorGUIUtility.singleLineHeight),
                        element.FindPropertyRelative("BrightnessDuration"),
                        GUIContent.none
                    );
                    break;
                case 6:
                    EditorGUI.LabelField(new Rect(rect.x, rect.y + 170, 120, EditorGUIUtility.singleLineHeight), "Duration");
                    EditorGUI.PropertyField(
                        new Rect(rect.x + 75, rect.y + 170, 70, EditorGUIUtility.singleLineHeight),
                        element.FindPropertyRelative("Duration"),
                        GUIContent.none
                    );

                    EditorGUI.LabelField(new Rect(rect.x, rect.y + 195, 100, EditorGUIUtility.singleLineHeight), "Direction");
                    EditorGUI.PropertyField(
                       new Rect(rect.x + 75, rect.y + 195, 100, EditorGUIUtility.singleLineHeight),
                       element.FindPropertyRelative("ShakeOrJellyDirection"),
                       GUIContent.none
                   );

                    EditorGUI.LabelField(new Rect(rect.x, rect.y + 220, 120, EditorGUIUtility.singleLineHeight), "Amplitude");
                    EditorGUI.PropertyField(
                        new Rect(rect.x + 75, rect.y + 220, 50, EditorGUIUtility.singleLineHeight),
                        element.FindPropertyRelative("Amplitude"),
                        GUIContent.none
                    );
                    break;
                case 7:
                    EditorGUI.LabelField(new Rect(rect.x, rect.y + 170, 100, EditorGUIUtility.singleLineHeight), "Direction");
                    EditorGUI.PropertyField(
                       new Rect(rect.x + 75, rect.y + 170, 100, EditorGUIUtility.singleLineHeight),
                       element.FindPropertyRelative("ShakeOrJellyDirection"),
                       GUIContent.none
                   );

                    EditorGUI.LabelField(new Rect(rect.x, rect.y + 195, 120, EditorGUIUtility.singleLineHeight), "Amplitude");
                    EditorGUI.PropertyField(
                        new Rect(rect.x + 75, rect.y + 195, 50, EditorGUIUtility.singleLineHeight),
                        element.FindPropertyRelative("Amplitude"),
                        GUIContent.none
                    );
                    break;
            }

            EditorGUI.PropertyField(
                new Rect(rect.x, rect.y + 225 + space, rect.width, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("OnStart"),
                new GUIContent("OnStart")
            );

            EditorGUI.PropertyField(
                new Rect(rect.x, rect.y + 325 + + space + (Target.Settings[index].OnStart.GetPersistentEventCount() > 0 ? Target.Settings[index].OnStart.GetPersistentEventCount() - 1 : 0) * 46.75f, rect.width, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("OnFinished"),
                new GUIContent("OnFinished")
            );
        }
    }
}