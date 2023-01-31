using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Growth : MonoBehaviour
{
    private static float bossScale = 1f;

    public static void HealthGrowth()
    {
        Health.maxHealth = Health.maxHealth + Mathf.Log(2, Health.maxHealth + 1f) + 1;
        Debug.Log(Health.maxHealth);
    }

    public static void Boss()
    {
        var addScale = 150 / Health.maxHealth;
        bossScale += addScale;
        Health.maxHealth *= bossScale;
        bossScale = bossScale + Mathf.Log(2, Health.maxHealth + 3f) + 3;
    }

    public static void BossDeath()
    {
        Health.maxHealth /= bossScale;
        Health.maxHealth = Health.maxHealth + Mathf.Log(2, Health.maxHealth + 1f) + 1;
        var scaleChange = new Vector3(0.3f, 0.3f, 0.3f);
        GameObject.FindGameObjectWithTag("Click").GetComponent<RectTransform>().transform.localScale -= scaleChange;
    }

    public static void PlayerDeath()
    {
        Time.timeScale = 0;
        // Death processing
    }
}
