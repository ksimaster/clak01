using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public void ChangeScene(int sceneIndex)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneIndex);
    }
    public void OnApplicationQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
