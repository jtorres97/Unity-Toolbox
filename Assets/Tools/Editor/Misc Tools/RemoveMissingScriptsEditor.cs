using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Toolbox
{
    public class RemoveMissingScriptsEditor : EditorWindow
    {
        public static void LaunchEditor()
        {
            var objs = Resources.FindObjectsOfTypeAll<GameObject>();
            int count = objs.Sum(GameObjectUtility.RemoveMonoBehavioursWithMissingScript);
            Debug.Log($"Removed {count} missing scripts");
        }
    }
}
