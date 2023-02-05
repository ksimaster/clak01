using UnityEngine;

public class Growth
{
    private static float bossScale = 0f;

    public static void Init()
    {
        bossScale = 4f;
    }

    public static void OnPlayerLifeLoss()
    {
        bossScale -= 1;
    }

    public static void HealthGrowth(Health health)
    {
        health.maxHealth = health.maxHealth + Mathf.Log(health.maxHealth + 1f,2) + 2;
    }

    public static void Boss(Health health)
    {
        health.maxHealth *= bossScale;
        bossScale = bossScale + 0.2f;
    }

    public static void BossDeath(Health health)
    {
        health.maxHealth /= bossScale;
        HealthGrowth(health);
        var scaleChange = new Vector3(0.3f, 0.3f, 0.3f);
        GameObject.FindGameObjectWithTag("Click").GetComponent<RectTransform>().transform.localScale -= scaleChange;
    }
}
