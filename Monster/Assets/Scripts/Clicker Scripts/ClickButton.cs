using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickButton : MonoBehaviour
{

    public static int clickValue; // Damage per click
    public static int amountPerSecond; // Damager per second

    private void Start()
    {
        clickValue = 1; // Starting Click Damage
        amountPerSecond = 0; // Starting DPS
    }

    public void ClickerButton()
    {
        if (!Health.monsterIsRespawning) // the player cant hit the monster if it is respawning.
        {
            CriticalHit.CritCheck(); // Check if the hit is a critical hit or not.
            ScoreManager.Increase(); // Update the health slider value.
        }
        
    }
    
}