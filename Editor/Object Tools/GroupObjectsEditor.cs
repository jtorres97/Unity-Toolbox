using System;
using UnityEditor;
using UnityEngine;

namespace Toolbox
{
    public class GroupObjectsEditor : EditorWindow
    {
        #region Variables

        /// <summary>
        /// General name
        /// </summary>
        private string m_wantedName;
        
        /// <summary>
        /// How many objects are selected
        /// </summary>
        private int m_currentSelectionCount;
        
        /// <summary>
        /// Selected objects in the scene
        /// </summary>
        private GameObject[] m_selected = new GameObject[0];
        
        #endregion
        
        #region Main Methods

        /// <summary>
        /// Launches this tool in the editor
        /// </summary>
        public static void LaunchEditor()
        {
            var editorWindow = GetWindow<GroupObjectsEditor>("Group Selected Objects");
            editorWindow.Show(); 
        }

        /// <summary>
        /// Unity Event function
        /// </summary>
        private void OnGUI()
        {
            m_selected = Selection.gameObjects;
            m_currentSelectionCount = m_selected.Length;
            
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space();

            EditorGUILayout.BeginVertical();
            EditorGUILayout.Space();
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Selection Count: " + m_currentSelectionCount);
            
            GUILayout.Space(5);

            EditorGUILayout.LabelField("Group Name", EditorStyles.boldLabel);
            m_wantedName = EditorGUILayout.TextField(m_wantedName);

            GUILayout.Space(5);

            if (GUILayout.Button("Group Selected", GUILayout.ExpandWidth(true), GUILayout.Height(45)))
            {
                GroupSelectedObjects();
            }
            
            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
            
            EditorGUILayout.Space();
            EditorGUILayout.EndHorizontal();
            
            Repaint();
        }

        #endregion
        
        #region Custom Methods

        private void GroupSelectedObjects()
        {
            if (m_selected.Length > 0)
            {
                if (m_wantedName != "")
                {
                    GameObject groupGO = new GameObject(m_wantedName + "_Group");
                    foreach (var go in m_selected)
                    {
                        go.transform.SetParent(groupGO.transform);
                    }
                }
                else
                {
                    EditorUtility.DisplayDialog("Grouper Message", "You must provide a name for your group!", "OK");
                }
            }
        }
        
        #endregion
    }
}
