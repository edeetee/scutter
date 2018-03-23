using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public class AutosaveOnRun
{
	static AutosaveOnRun()
	{
		EditorApplication.playModeStateChanged += (state) =>
		{
			if(state == PlayModeStateChange.ExitingEditMode){
				Debug.Log("Auto-Saving scene before entering Play mode: " + state);
				AssetDatabase.SaveAssets();
				EditorSceneManager.SaveOpenScenes();
			}
		};
	}
}