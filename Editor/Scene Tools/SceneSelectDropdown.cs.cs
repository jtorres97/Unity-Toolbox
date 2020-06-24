using System;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace Toolbox
{
    public class SceneSelectDropdownEditor : EditorWindow
    {
        #region Variables

        /// <summary>
        /// Flag to show only the scenes in the build index
        /// </summary>
        private bool m_showOnlyScenesInBuild = false;
        
        #endregion

        #region Main Methods

        /// <summary>
        /// Launches this tool in the editor
        /// </summary>
        public static void LaunchEditor()
        {
            var editorWindow = GetWindow<SceneSelectDropdownEditor>("Scene Select Dropdown");
            editorWindow.minSize = new Vector2( 50.0f, 15.0f );
            editorWindow.Show(); 
        }

        /// <summary>
        /// Unity event function
        /// </summary>
        private void OnEnable()
        {
            m_showOnlyScenesInBuild = EditorPrefs.GetBool("SceneSelectDropdown/ShowOnlyScenesInBuild");
        }

        /// <summary>
        /// Unity event function
        /// </summary>
        private void OnDisable()
        {
            EditorPrefs.SetBool("SceneSelectDropdown/ShowOnlyScenesInBuild", m_showOnlyScenesInBuild);
        }

        /// <summary>
        /// Unity event function
        /// </summary>
        private void OnGUI()
        {
            int current = -1;
            string[] scenePaths;

            if (m_showOnlyScenesInBuild)
            {
                scenePaths = GetScenePathsAddedToBuild();
            }
            else
            {
                scenePaths = GetAllScenePathsInProject();
            }

            string[] sceneNames = new string[scenePaths.Length];
            for (int i = 0; i < scenePaths.Length; ++i)
            {
                // Work out the scene name from it's path
                int lastSlash = scenePaths[i].LastIndexOf("/", StringComparison.Ordinal);
                string sceneName = scenePaths[i].Substring(lastSlash + 1);
                sceneName = sceneName.Replace(".unity", "");

                sceneNames[i] = sceneName;

                if (SceneManager.GetActiveScene().name == sceneName)
                {
                    current = i;
                }
            }
            
            int newSceneIndex = EditorGUILayout.Popup(current, sceneNames);
            if (newSceneIndex != current)
            {
                bool save = EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                if (save)
                {
                    EditorSceneManager.OpenScene( scenePaths[newSceneIndex], OpenSceneMode.Single );
                }
            }

            m_showOnlyScenesInBuild = EditorGUILayout.Toggle("Scenes In Build Only", m_showOnlyScenesInBuild);
        }

        #endregion

        #region Custom Methods

        /// <summary>
        /// Get all the scenes regardless if they're in the build index or not
        /// </summary>
        /// <returns>All scenes</returns>
        private string[] GetAllScenePathsInProject()
        {
            string[] sceneAssets = AssetDatabase.FindAssets("t:Scene");
            string[] scenePaths = new string[sceneAssets.Length];
            for (int i = 0; i < sceneAssets.Length; ++i)
            {
                scenePaths[i] = AssetDatabase.GUIDToAssetPath(sceneAssets[i]);
            }

            return scenePaths;
        }

        /// <summary>
        /// Get only the scenes added to the build index
        /// </summary>
        /// <returns>Scenes in the build index</returns>
        private string[] GetScenePathsAddedToBuild()
        {
            string[] scenePaths = new string[EditorBuildSettings.scenes.Length];
            for (int i = 0; i < EditorBuildSettings.scenes.Length; ++i)
            {
                scenePaths[i] = EditorBuildSettings.scenes[i].path;
            }

            return scenePaths;
        }
        
        #endregion
    }
}