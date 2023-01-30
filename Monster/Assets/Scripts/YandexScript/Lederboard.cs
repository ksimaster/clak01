using UnityEngine;


public class Lederboard : MonoBehaviour
{
    public string nameScene;
    void Start()
    {
        if(nameScene == "Menu") SetHighScoreOnLederboard();

    }

    public void SetHighScoreOnLederboard()
    {
        
        int best = PlayerPrefs.GetInt("HighScore");
#if UNITY_WEBGL && !UNITY_EDITOR
    	WebGLPluginJS.SetLeder(best);
#endif
    }

}
