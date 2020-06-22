using UnityEditor;
using UnityEngine;

namespace Toolbox
{
    public class ReplaceObjectsEditor : EditorWindow
    {
        #region Variables

        /// <summary>
        /// How many objects are selected
        /// </summary>
        private int m_currentSelectionCount = 0;

        /// <summary>
        /// To be replaced
        /// </summary>
        private GameObject m_wantedObject;
        
        #endregion

        #region Main Methods

        /// <summary>
        /// Launches this tool in the editor
        /// </summary>
        public static void LaunchEditor()
        {
            var editorWindow = GetWindow<ReplaceObjectsEditor>("Replace Selected Objects");
            editorWindow.Show();
        }

        /// <summary>
        /// Unity Event function
        /// </summary>
        private void OnGUI()
        {
            // Check the amount of selected objects
            GetSelection();
            
            EditorGUILayout.BeginVertical();
            EditorGUILayout.Space();
            
            EditorGUILayout.LabelField("Selection Count: " + m_currentSelectionCount, EditorStyles.boldLabel);
            EditorGUILayout.Space();

            m_wantedObject = (GameObject) EditorGUILayout.ObjectField("Replace Object: ", m_wantedObject, typeof(GameObject), true);
            if (GUILayout.Button("Replace Selected Objects", GUILayout.ExpandWidth(true), GUILayout.Height(40)))
            {
                ReplaceSelectedObjects();
            }
            
            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
            
            Repaint();
        }

        #endregion

        #region Custom Methods

        /// <summary>
        /// Keep track of how many objects are selected
        /// </summary>
        private void GetSelection()
        {
            m_currentSelectionCount = 0;
            m_currentSelectionCount = Selection.gameObjects.Length;
        }

        /// <summary>
        /// Replaces objects selected in the hierarchy 
        /// </summary>
        private void ReplaceSelectedObjects()
        {
            // Check for selection count
            if (m_currentSelectionCount == 0)
            {
                EditorUtility.DisplayDialog("Replace Objects Warning",
                    "At least one object needs to be selected to replace with!", "OK");
                return;
            }
            
            // Check for replace Object
            if (!m_wantedObject)
            {
                EditorUtility.DisplayDialog("Replace Objects Warning",
                    "The Replace Object is empty, please assign something!", "OK");
                return;
            }
            
            // Replace objects
            GameObject[] selectedObjects = Selection.gameObjects;
            foreach (var selectedObject in selectedObjects)
            {
                Transform selectTransform = selectedObject.transform;
                GameObject newObject = Instantiate(m_wantedObject, selectTransform.position, selectTransform.rotation);
                newObject.transform.localScale = selectTransform.localScale;
                
                DestroyImmediate(selectedObject);
            }
        }

        #endregion
    }
}
