using System;
using UnityEditor;
using UnityEngine;

namespace Toolbox
{
    public class RenameObjectsEditor : EditorWindow
    {
        #region Variables

        /// <summary>
        /// Selected objects in the scene
        /// </summary>
        private GameObject[] m_selected = new GameObject[0];
        
        /// <summary>
        /// Naming prefix
        /// </summary>
        private string m_wantedPrefix;
        
        /// <summary>
        /// General name
        /// </summary>
        private string m_wantedName;
        
        /// <summary>
        /// Naming suffix
        /// </summary>
        private string m_wantedSuffix;
        
        /// <summary>
        /// Determines whether or not to concatenate a number
        /// </summary>
        private bool m_addNumbering;
        
        #endregion
        
        #region Main Methods

        /// <summary>
        /// Launches this tool in the editor
        /// </summary>
        public static void LaunchEditor()
        {
            var window = GetWindow<RenameObjectsEditor>("Rename Selected Objects");
            window.Show();
        }

        /// <summary>
        /// Unity Event function
        /// </summary>
        private void OnGUI()
        {
            // Get current selected objects
            m_selected = Selection.gameObjects;
            EditorGUILayout.LabelField("Selected: " + m_selected.Length);
            
            // Display our User UI
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(10);
            EditorGUILayout.BeginVertical();
            GUILayout.Space(10);
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            GUILayout.Space(10);
    
            m_wantedPrefix = EditorGUILayout.TextField("Prefix: ", m_wantedPrefix, EditorStyles.miniTextField, GUILayout.ExpandWidth(true));
            m_wantedName = EditorGUILayout.TextField("Name: ", m_wantedName, EditorStyles.miniTextField, GUILayout.ExpandWidth(true));
            m_wantedSuffix = EditorGUILayout.TextField("Suffix: ", m_wantedSuffix, EditorStyles.miniTextField, GUILayout.ExpandWidth(true));
            m_addNumbering = EditorGUILayout.Toggle("Add Numbering?", m_addNumbering);
            
            GUILayout.Space(10);
            EditorGUILayout.EndVertical();

            if (GUILayout.Button("Rename Selected Objects", GUILayout.Height(45), GUILayout.ExpandWidth(true)))
            {
                RenameSelectedObjects();
            }
            
            GUILayout.Space(10);
            EditorGUILayout.EndVertical();

            GUILayout.Space(10);
            EditorGUILayout.EndHorizontal();
            
            Repaint();
        }

        #endregion

        #region Custom Methods

        /// <summary>
        /// Renames the selected objects
        /// </summary>
        private void RenameSelectedObjects()
        {
            Array.Sort(m_selected, (a, b) => string.Compare(a.name, b.name, StringComparison.Ordinal));
            
            for (int i = 0; i < m_selected.Length; i++)
            {
                string finalName = string.Empty;
                if (m_wantedPrefix.Length > 0)
                {
                    finalName += m_wantedPrefix;
                }

                if (m_wantedName.Length > 0)
                {
                    finalName += "_" + m_wantedName;
                }

                if (m_wantedSuffix.Length > 0)
                {
                    finalName += "_" + m_wantedSuffix;
                }

                if (m_addNumbering)
                {
                    finalName += "_" + (i + 1);
                }

                m_selected[i].name = finalName;
            }
        }
        
        #endregion
    }
}
