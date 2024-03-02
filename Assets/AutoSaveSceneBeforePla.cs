using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public class AutoSaveSceneBeforePlay : MonoBehaviour
{
    static AutoSaveSceneBeforePlay()
    {
        EditorApplication.playModeStateChanged += saveCurrentScene;
    }

    private static void saveCurrentScene(PlayModeStateChange state)
    {
        // Debug.Log("state : " + state
        //     + "/ isPlaying : " + EditorApplication.isPlaying
        //     + "/ isPlayingOrWillChangePlaymode : " + EditorApplication.isPlayingOrWillChangePlaymode);

        if (EditorApplication.isPlaying == false
                    && EditorApplication.isPlayingOrWillChangePlaymode == true)
            EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());

    }
}