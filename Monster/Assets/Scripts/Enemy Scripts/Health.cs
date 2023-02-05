using UnityEngine;
using UnityEngine.UI;
public class Health : MonoBehaviour
{
    public const float respawnDelay = 0.1f;
    private const float TimesPerSecond = 4f;
    private const int BackgroundCount = 9;
    private const int BossPerMobs = 6;
    private const float TimePerBoss = 30f;

    private int bossCounter = BossPerMobs;
    private int playerHealth = 3;
    private int startHealth = 10;

    public Text playerHealthText;
    public float health;
    public float maxHealth;
    public bool monsterIsRespawning;
    public bool isBoss;
    public Slider healthBar;
    public BackgroundSwitch backgroundSwitch;
    public Text healthText;
    public int roundedHealth;


    public EnemySwitcher enemySwitcher;
    private int backgroundCounter = 0;

    public float bossTimer;
    public Text bossTimerText;

    public Text TotalMonstersText;
    private int totalMonsters = 0;

    public GameObject DeathPanel;
    private ScoreManager ScoreManager;
    private UpgradeManager UpgradeManager;


    // Start is called before the first frame update
    void Start()
    {
        Growth.Init();
        health = startHealth;
        maxHealth = startHealth;
        monsterIsRespawning = false;
        InvokeRepeating("Timer", 1, 1/TimesPerSecond);
        healthText = GameObject.FindGameObjectWithTag("Health").GetComponent<Text>();
        bossTimerText.gameObject.SetActive(false);

        UpgradeManager = GameObject.FindGameObjectWithTag("UpgradeManager").GetComponent<UpgradeManager>();
        ScoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0 && !monsterIsRespawning) // if the monster is dead
        {
            monsterIsRespawning = true;
            MonsterKilled();
            totalMonsters++;
            TotalMonstersText.text = $"{totalMonsters}";
        }
        else if (health >= 0 && isBoss) // if the monster is alive and is a boss monster
        {
            bossTimerText.text = "Осталось времени: " + Mathf.RoundToInt((bossTimer - Time.time) * 10) / 10;
        }

        if ((bossTimer - Time.time) < 0 && isBoss)
        {
            bossTimerText.gameObject.SetActive(false);            
            isBoss = false;

            Growth.BossDeath(this);
            Growth.OnPlayerLifeLoss();
            playerHealth -= 1;
            playerHealthText.text = $"{playerHealth}";
            if (playerHealth == 0)
            {
                // Меню, сброс нужно добавить
                PlayerDeath();
                return;
            }

            health = maxHealth;
            health -= 0.01f; // this is enough to move the health bar slightly.
            // Switch monster art
            enemySwitcher.ChangeSprite();
            backgroundCounter--;
            if (backgroundCounter < 0)
            {
                backgroundCounter = 0;
            }
            BackgroundSwitch();
        }
    }
    private void LateUpdate()
    {
        if (health < 0)
        {
            health = 0; // health clamp
        }

        if (healthBar.value != Mathf.Clamp01(health / maxHealth))
        {
            healthBar.value = Mathf.Clamp01(health / maxHealth);
            roundedHealth = Mathf.RoundToInt(health);

            if (health < 1 && health > 0)
            {
                roundedHealth = 1;
            }
            healthText.text = "" + roundedHealth;
        }
    }

    void MonsterKilled()
    {
        // if the monster which died is also a boss run the script
        if (isBoss)
        {
            BossKill();
            enemySwitcher.NextLevel();
        }

        // if the monster which died is not the last monster before the boss, spawn a normal monster
        if (bossCounter > 0)
        {
            bossCounter--;
            Growth.HealthGrowth(this);
        }
        else // otherwise spawn the boss 
        {                
            Growth.Boss(this); // give the boss 10x health
            isBoss = true;
            enemySwitcher.IsBoss = true;
            bossCounter = BossPerMobs; // reset the counter
            bossTimer = Time.time + TimePerBoss; // set the timer to 30 seconds in the future.
            bossTimerText.gameObject.SetActive(true); // enable the timer
        }

        
        // call respawn script after a certain delay to set health back to full
        Invoke("Respawn", respawnDelay);
    }

    void Respawn()
    {
        health = maxHealth; // Sets health to the max health of the enemy.
        monsterIsRespawning = false; // The monster has respawned so the enemy is no longer respwaning.
    }

    void Timer()
    {
        if(!monsterIsRespawning)
        {
            var dmg = UpgradeManager.ClickMonster.amountPerSecond;
            ScoreManager.score += dmg/TimesPerSecond;
            ScoreManager.Increase(); // Update the health slider value.
            health -= dmg/ TimesPerSecond; // decreases the mosnter health if the mosnter is alive
        }
    }

    void BossKill() // called if the boss dies
    {
        isBoss = false;
        Growth.BossDeath(this); // lowers health so all the monster arent bloody bosses
        backgroundCounter++; // switches background
        bossTimerText.gameObject.SetActive(false); // disable the timer

        if (backgroundCounter < BackgroundCount)
        {
            Invoke("BackgroundSwitch", respawnDelay);
        }
        else
        {
            backgroundCounter = 0; // resets counter
            Invoke("BackgroundSwitch", respawnDelay);
        }
    }

    void BackgroundSwitch()
    {
        backgroundSwitch.SelectBackground(backgroundCounter); // switches to the next background
    }

    void PlayerDeath()
    {
        Time.timeScale = 0;
        DeathPanel.SetActive(true);
        // Death processing
    }
}
