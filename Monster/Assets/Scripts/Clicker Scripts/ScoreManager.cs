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

#if UNITY_WEBGL
        Vector2 cursorOffset = new Vector2(cursor.width / 2, cursor.height / 2);
        Cursor.SetCursor(cursor, cursorOffset, CursorMode.ForceSoftware);
#else
        Vector2 cursorOffset = new Vector2(cursor.width, cursor.height);
        Cursor.SetCursor(cursor, cursorOffset, CursorMode.Auto);
#endif
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
