using UnityEngine;
using UnityEngine.UI;

public class BestScoreOnMain : MonoBehaviour
{
    public Text bestScore;
    private void Start()
    {
        bestScore.text = PlayerPrefs.GetInt("HighScore").ToString();
    }

}
