﻿using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Toolbox
{
    public class ProjectSetupEditor : EditorWindow
    {
        #region Variables
        
        /// <summary>
        /// Name of the root folder
        /// </summary>
        private string m_gameName = "Game";
        
        /// <summary>
        /// Window to show in the Editor
        /// </summary>
        public static ProjectSetupEditor Editor;
        
        #endregion
        
        #region Main Methods
        
        /// <summary>
        /// Initializes the window to show in the editor
        /// </summary>
        public static void InitializeWindow()
        {
            Editor = EditorWindow.GetWindow<ProjectSetupEditor>("Project Setup");
            Editor.Show();
        }

        /// <summary>
        /// Unity Event function
        /// </summary>
        private void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            m_gameName = EditorGUILayout.TextField("Game Name: ", m_gameName);
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("Create Project Structure", GUILayout.Height(35), GUILayout.ExpandWidth(true)))
            {
                CreateProjectFolders();
            }
            
            if (Editor != null)
                Editor.Repaint();
        }
        
        #endregion

        #region Custom Methods

        /// <summary>
        /// Creates the folders for our project structure
        /// </summary>
        private void CreateProjectFolders()
        {
            // No empty names allowed
            if (string.IsNullOrEmpty(m_gameName))
                return;

            // User didn't change the default
            if (m_gameName == "Game")
            {
                if (!EditorUtility.DisplayDialog("Project Setup Warning", "Do you really want to call your project \"Game\"?", "Yes", "No"))
                {
                    CloseWindow();
                    return;
                }
            }
            
            // Create the root for the project structure
            string assetPath = Application.dataPath;
            string rootPath = assetPath + "/" + m_gameName;
            
            var rootInfo = Directory.CreateDirectory(rootPath);

            // Create the subfolders
            if (!rootInfo.Exists)
                return;
            
            CreateSubFolders(rootPath);
            
            // Make sure everything is updated in Unity
            AssetDatabase.Refresh();
            
            // No need to keep showing this window anymore
            CloseWindow();
        }
        
        /// <summary>
        /// Creates all the folders we need for our project
        /// </summary>
        /// <param name="rootPath">Where these subfolders will be made</param>
        private void CreateSubFolders(string rootPath)
        {
            List<string> folderNames = new List<string>();

            var rootInfo = Directory.CreateDirectory(rootPath + "/" + "Art");
            if (rootInfo.Exists)
            {
                folderNames.Clear();
                folderNames.Add("Animation");
                folderNames.Add("Objects");
                folderNames.Add("Materials");
                folderNames.Add("Prefabs");

                CreateFolders(rootPath + "/" + "Art", folderNames);
            }
            
            rootInfo = Directory.CreateDirectory(rootPath + "/" + "Code");
            if (rootInfo.Exists)
            {
                folderNames.Clear();
                folderNames.Add("Editor");
                folderNames.Add("Scripts");
                folderNames.Add("Shaders");

                CreateFolders(rootPath + "/" + "Code", folderNames);
            }
            
            rootInfo = Directory.CreateDirectory(rootPath + "/" + "Resources");
            if (rootInfo.Exists)
            {
                folderNames.Clear();
                folderNames.Add("Characters");
                folderNames.Add("Props");
                folderNames.Add("UI");

                CreateFolders(rootPath + "/" + "Resources", folderNames);
            }
            
            rootInfo = Directory.CreateDirectory(rootPath + "/" + "Prefabs");
            if (rootInfo.Exists)
            {
                folderNames.Clear();
                folderNames.Add("Characters");
                folderNames.Add("Props");
                folderNames.Add("UI");

                CreateFolders(rootPath + "/" + "Prefabs", folderNames);
            }
            
            // Create scenes
            DirectoryInfo sceneInfo = Directory.CreateDirectory(rootPath + "/Scenes");
            if (sceneInfo.Exists)
            {
                CreateScene(rootPath + "/Scenes", m_gameName + "_Main");
                CreateScene(rootPath + "/Scenes", m_gameName + "_Frontend");
                CreateScene(rootPath + "/Scenes", m_gameName + "_Startup");
            }
        }

        /// <summary>
        /// Function to create multiple folders in a directory
        /// </summary>
        /// <param name="path">Root directory</param>
        /// <param name="folders">Folders to create</param>
        private void CreateFolders(string path, List<string> folders)
        {
            foreach (var folder in folders)
            {
                Directory.CreateDirectory(path + "/" + folder);
            }
        }

        /// <summary>
        /// Generates a scene
        /// </summary>
        /// <param name="path">Where the scene is going to be stored</param>
        /// <param name="name">Name of the scene</param>
        private void CreateScene(string path, string name)
        {
            Scene currentScene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
            EditorSceneManager.SaveScene(currentScene, path + "/" + name + ".unity", true);
        }

        /// <summary>
        /// Closes the window in the Editor
        /// </summary>
        private static void CloseWindow()
        {
            if (Editor)
                Editor.Close();
        }

        #endregion
    }
}
