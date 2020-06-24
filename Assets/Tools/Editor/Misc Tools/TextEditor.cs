using System.IO;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Toolbox
{
    public class TextEditor : EditorWindow
{
    private Vector2 m_scrollPos;
    private static string m_content;
    private static Object m_selectedObject;
    private static string m_path;

    [MenuItem("Assets/Edit Text", true)]
    private static bool ValidateTextAsset()
    {
        return CurrentSelectionValidation();
    }

    [MenuItem("Assets/Edit Text")]
    private static void EditTextAsset(MenuCommand menuCommand)
    {
        ReadFile();
        ShowWindow();
    }

    private void OnGUI()
    {
        if (m_selectedObject != null)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(m_path, EditorStyles.boldLabel);
            EditorGUILayout.ObjectField(m_selectedObject, typeof(object), false, GUILayout.Width(200f));
            EditorGUILayout.EndHorizontal();

            m_scrollPos = EditorGUILayout.BeginScrollView(m_scrollPos);
            m_content = EditorGUILayout.TextArea(m_content);
            EditorGUILayout.EndScrollView();

            ShowButtons();
        }
        else
        {

            EditorGUILayout.LabelField("You need to select an asset at first!", EditorStyles.boldLabel);
        }
    }
    
    private void OnSelectionChange()
    {
        Debug.Log("selection changed");
        if (m_selectedObject == null)
        {
            UpdateSelection();
        }
    }

    private static void UpdateSelection()
    {
        if (CurrentSelectionValidation())
            EditTextAsset(null);
    }

    private static bool CurrentSelectionValidation()
    {
        m_path = "";

        var selectedObjects = Selection.objects;
        if (selectedObjects.Length == 1)
        {
            m_selectedObject = selectedObjects[0];
            m_path = AssetDatabase.GetAssetPath(m_selectedObject.GetInstanceID());

            if (m_path.Length > 0)
            {
                if (File.Exists(m_path))
                {
                    return true;
                }
            }
        }

        m_selectedObject = null;
        return false;
    }
    
    private static void ShowWindow()
    {
        var window = GetWindow<TextEditor>();
        window.titleContent = new GUIContent("Text Editor");
        window.Show();
    }
    
    private void ShowButtons()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical();

        if (GUILayout.Button("Update selection"))
        {
            UpdateSelection();
        }

        if (GUILayout.Button("Clean all"))
        {
            m_content = "";
        }

        EditorGUILayout.EndVertical();
        EditorGUILayout.BeginVertical();

        if (GUILayout.Button("Save and Close"))
        {
            WriteToFile();
            Close();
        }

        if (GUILayout.Button("Cancel and Close"))
        {
            Close();
        }

        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
    }

    private static void ReadFile()
    {
        StreamReader reader = new StreamReader(m_path);
        m_content = reader.ReadToEnd();
        reader.Close();
    }

    private static void WriteToFile()
    {
        Undo.RecordObject(m_selectedObject, "Editing Text File");

        StreamWriter writer = new StreamWriter(m_path, false);
        writer.Write(m_content);
        writer.Close();

        AssetDatabase.ImportAsset(m_path);
    }
}
}
