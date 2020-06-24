using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Toolbox
{
    public class EditorMenus : MonoBehaviour
    {
        #region Project
        
        /// <summary>
        /// Adds the Project Setup Tool to the menu
        /// </summary>
        [MenuItem("Toolbox/Project/Project Setup Tool")]
        public static void ProjectSetupTool()
        {
            ProjectSetupEditor.InitializeWindow();
        }

        #endregion 
        
        #region GameObjects
        
        /// <summary>
        /// Adds the Replace Selected Objects to the menu
        /// </summary>
        [MenuItem("Toolbox/GameObjects/Replace Selected Objects")]
        public static void ReplaceObjectsTool()
        {
            ReplaceObjectsEditor.LaunchEditor();
        }
        
        /// <summary>
        /// Adds the Rename Selected Objects to the menu
        /// </summary>
        [MenuItem("Toolbox/GameObjects/Rename Selected Objects")]
        public static void RenameObjectsTool()
        {
            RenameObjectsEditor.LaunchEditor();
        }
        
        /// <summary>
        /// Groups Selected Objects a root object for organization in the hierarchy
        /// </summary>
        [MenuItem("Toolbox/GameObjects/Group Selected Objects")]
        public static void GroupObjectsTool()
        {
            GroupObjectsEditor.LaunchEditor();
        }

        #endregion

        #region Reset Transform

        /// <summary>
        /// Resets the position of the transform node(s)
        /// </summary>
        [MenuItem("Toolbox/Reset Transform/Reset Local Position %&w")]
        public static void ResetPosition()
        {
            foreach (var t in Selection.gameObjects)
            {
                Undo.RecordObject(t.transform, "Reset transform");
                t.transform.localPosition = Vector3.zero;
            }
        }
        
        /// <summary>
        /// Resets the rotation of the transform node(s)
        /// </summary>
        [MenuItem("Toolbox/Reset Transform/Reset Local Rotation %&e")]
        public static void ResetRotation()
        {
            foreach (var t in Selection.gameObjects)
            {
                Undo.RecordObject(t.transform, "Reset transform");
                t.transform.localRotation = Quaternion.identity;
            }
        }
        
        /// <summary>
        /// Resets the scale of the transform node(s)
        /// </summary>
        [MenuItem("Toolbox/Reset Transform/Reset Local Scale %&r")]
        public static void ResetScale()
        {
            foreach (var t in Selection.gameObjects)
            {
                Undo.RecordObject(t.transform, "Reset transform");
                t.transform.localScale = Vector3.one;
            }
        }
        
        /// <summary>
        /// Resets the everything in the transform node(s)
        /// </summary>
        [MenuItem("Toolbox/Reset Transform/Reset Everything %&q")]
        public static void ResetEverything()
        {
            foreach (var t in Selection.gameObjects)
            {
                Undo.RecordObject(t.transform, "Reset transform");
                t.transform.localScale = Vector3.one;
                t.transform.localRotation = Quaternion.identity;
                t.transform.localPosition = Vector3.zero;
            }
        }

        #endregion
        
        #region Misc Tools
        
        /// <summary>
        /// Removes missing scripts
        /// </summary>
        [MenuItem("Toolbox/Remove Missing Scripts")]
        public static void RemoveMissingScriptsTool()
        {
            RemoveMissingScriptsEditor.LaunchEditor();
        }

        /// <summary>
        /// Opens a dropdown to select any scene in the project
        /// </summary>
        [MenuItem("Toolbox/Scene Select Dropdown")]
        public static void SceneSelectDropdownTool()
        {
            SceneSelectDropdownEditor.LaunchEditor();
        }

        /// <summary>
        /// Deletes all PlayerPrefs from project
        /// </summary>
        [MenuItem("Toolbox/Delete PlayerPrefs")]
        public static void DeletePlayerPrefsTool()
        {
            if (EditorUtility.DisplayDialog("Delete all PlayerPrefs?", "Are you sure you want to delete all PlayerPrefs?", "Yes", "No"))
            {
                PlayerPrefs.DeleteAll();
            }
        }
        
        #endregion
    }
}
