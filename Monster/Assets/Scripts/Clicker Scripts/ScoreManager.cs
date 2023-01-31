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
    public Texture2D cursor;

    private void Start()
    {
        Vector2 cursorOffset = new Vector2(cursor.width / 2, cursor.height / 2);

        //Sets the cursor to the Crosshair sprite with given offset 
        //and automatic switching to hardware default if necessary
        Cursor.SetCursor(cursor, cursorOffset, CursorMode.Auto);

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
