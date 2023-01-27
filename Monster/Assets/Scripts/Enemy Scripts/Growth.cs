using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Growth : MonoBehaviour
{
    public static void HealthGrowth()
    {
        Health.maxHealth *= 1.2f;
    }

    public static void Boss()
    {
        Health.maxHealth *= 12f;
    }

    public static void BossDeath()
    {
        Health.maxHealth *= 0.1f;
    }

    public static void PlayerDeath()
    {
        Health.maxHealth *= 0.1f / (float)(1.2 * 1.2 * 1.2 * 1.2 * 1.2 * 1.2 * 1.2 * 1.2 * 1.2 * 1.2);
    }
}
