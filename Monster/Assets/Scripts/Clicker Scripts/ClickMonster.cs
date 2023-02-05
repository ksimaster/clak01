using UnityEngine;

public class ClickMonster: MonoBehaviour
{
    public int clickValue; // Damage per click
    public int amountPerSecond; // Damager per second
    public Health Health;

    private ScoreManager ScoreManager;
    private UpgradeManager UpgradeManager;

    private void Start()
    {
        clickValue = 1; // Starting Click Damage
        amountPerSecond = 0; // Starting DPS
        UpgradeManager = GameObject.FindGameObjectWithTag("UpgradeManager").GetComponent<UpgradeManager>();
        ScoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
    }

    public void ClickerButton()
    {
        if (!Health.monsterIsRespawning) // the player cant hit the monster if it is respawning.
        {
            var dmg = CriticalHit.CritCheck(clickValue, UpgradeManager.CritChance, UpgradeManager.CritDamage); // Check if the hit is a critical hit or not.
            Health.health -= dmg;  // Health.
            ScoreManager.score += dmg; // Increase gold.
            ScoreManager.Increase(); // Update the health slider value.
        }       
    }
}