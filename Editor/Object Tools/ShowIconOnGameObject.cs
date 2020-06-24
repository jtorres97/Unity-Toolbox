using UnityEditor;
using UnityEngine;

namespace Toolbox
{
	[InitializeOnLoad]
	public class ShowIconOnGameObject
	{
		/// <summary>
		/// Unity event function
		/// </summary>
		static ShowIconOnGameObject()
		{
			EditorApplication.hierarchyWindowItemOnGUI += DrawIconOnWindowItem;
		}

		/// <summary>
		/// Draws an icon on GameObject in hierarchy if it has a custom script
		/// </summary>
		private static void DrawIconOnWindowItem(int instanceID, Rect rect)
		{
			var gameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
			
			if (gameObject == null)
				return;

			if (!gameObject.TryGetComponent<MonoBehaviour>(out var script))
				return;
			
			if (!IsCustomScript(script))
				return;

			var r = new Rect(
				rect.xMax - 25,
				rect.yMin,
				rect.width,
				rect.height);

			GUI.Label(r, EditorGUIUtility.IconContent("d_cs Script Icon"));
		}

		/// <summary>
		/// Does the GameObject contain custom script?
		/// </summary>
		/// <param name="script"></param>
		/// <returns>True if custom script</returns>
		private static bool IsCustomScript(MonoBehaviour script)
		{
			var nameSpace = script.GetType().Namespace;
			if (string.IsNullOrEmpty(nameSpace))
				return true;
			
			return !nameSpace.Contains("UnityEngine") && !nameSpace.Contains("TMPro");
		}
	}
}