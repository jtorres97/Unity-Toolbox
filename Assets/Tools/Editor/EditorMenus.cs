using UnityEditor;
using UnityEngine;

namespace Toolbox
{
    public class EditorMenus : MonoBehaviour
    {
        /// <summary>
        /// Adds the Project Setup Tool to the menu
        /// </summary>
        [MenuItem("Toolbox/Project/Project Setup Tool")]
        public static void ProjectSetupTool()
        {
            ProjectSetupEditor.InitializeWindow();
        }

        /// <summary>
        /// Adds the Replace Selected Objects to the menu
        /// </summary>
        [MenuItem("Toolbox/Scene/Replace Selected Objects")]
        public static void ReplaceObjectsTool()
        {
            ReplaceObjectsEditor.LaunchEditor();
        }
        
        /// <summary>
        /// Adds the Rename Selected Objects to the menu
        /// </summary>
        [MenuItem("Toolbox/Scene/Rename Selected Objects")]
        public static void RenameObjectsTool()
        {
            RenameObjectsEditor.LaunchEditor();
        }
        
        /// <summary>
        /// Groups Selected Objects a root object for organization in the hierarchy
        /// </summary>
        [MenuItem("Toolbox/Scene/Group Selected Objects")]
        public static void GroupObjectsTool()
        {
            GroupObjectsEditor.LaunchEditor();
        }
    }
}
