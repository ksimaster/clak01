using System.Collections;
using UnityEngine;

public class CriticalHit
{
    public static float CritCheck(float clickValue, float critChance, float critDamage)
    {
        float critRoll = Random.Range(0f, 100f);

        if (critRoll <= critChance)
        {
            // CRIT CONDITION
            return clickValue * critDamage; // Health.
        }
        return clickValue;
    }
}
