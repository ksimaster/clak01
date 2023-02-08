using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public float score;
    private int roundedScore;
    public Text scoreDisplay;
    // public float timer;
    public Texture2D cursor;

    private UpgradeManager UpgradeManager;

    private void Start()
    {
        Vector2 cursorOffset = new Vector2(cursor.width / 2, cursor.height / 2);

        //Sets the cursor to the Crosshair sprite with given offset 
        //and automatic switching to hardware default if necessary
        Cursor.SetCursor(cursor, cursorOffset, CursorMode.Auto);

        scoreDisplay = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
        UpgradeManager = GameObject.FindGameObjectWithTag("UpgradeManager").GetComponent<UpgradeManager>();
    }

    public void Increase()
    {
        roundedScore = Mathf.RoundToInt(score);
        scoreDisplay.text = "" + roundedScore;
        UpgradeManager.UpdateButtons();
    }
}
