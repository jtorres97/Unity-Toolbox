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
        public static void InitializeProjectSetupTool()
        {
            ProjectSetupWindow.InitializeWindow();
        }
    }
}
