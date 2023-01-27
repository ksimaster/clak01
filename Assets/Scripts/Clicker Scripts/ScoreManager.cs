using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static float score;
    private static int roundedScore;
    public static Text scoreDisplay;
    // public float timer;

    private void Start()
    {
        scoreDisplay = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
        InvokeRepeating("Timer", 1, 0.25f);
    }
    public static void Increase()
    {
        roundedScore = Mathf.RoundToInt(score);
        scoreDisplay.text = "" + roundedScore;
    }
    private void Timer()
    {
        score += ClickButton.amountPerSecond;
        Increase();
    }
}
