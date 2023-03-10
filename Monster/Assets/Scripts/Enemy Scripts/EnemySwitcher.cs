using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySwitcher : MonoBehaviour
{
    private const int MaxLevel = 9;

    public Button click;
    public Image image;
    public Sprite[] monsters;
    
    public bool spawnEnemy = false;
    private string compare = "";
    private int currentLevel = 0;
    public bool IsBoss = false;

    public Health Health;
    private Dictionary<int, List<Sprite>> monsterDict = new Dictionary<int, List<Sprite>>();


    public void NextLevel()
    {
        currentLevel++;
        if (currentLevel > MaxLevel)
        {
            currentLevel = 0;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        click = GameObject.FindGameObjectWithTag("Click").GetComponent<Button>();
        image = click.GetComponent<Image>();
        foreach(var monster in monsters)
        {
            var name = monster.name.Replace("m", "");
            var level = int.Parse(name.Split('_')[0]);
            if (!monsterDict.ContainsKey(level))
            {
                monsterDict.Add(level, new List<Sprite>());
            }
            monsterDict[level].Add(monster);
        }

        var randomId = Random.Range(0, monsterDict[currentLevel].Count);
        image.sprite = monsterDict[currentLevel][randomId];
    }

    // Update is called once per frame
    void Update()
    {
        if (Health.monsterIsRespawning && !spawnEnemy)
        {
            Invoke("ChangeSprite", Health.respawnDelay);
            spawnEnemy = true;
        }

        if (!Health.monsterIsRespawning && spawnEnemy)
        {
            spawnEnemy = false;
        }
    }

    public void ChangeSprite()
    {
        Sprite randomMonster;
        while (true)
        {

            var randomLevel = currentLevel > 0 ? Random.Range(0, 2) : 0;
            var level = currentLevel - randomLevel;
            randomMonster = monsterDict[level][Random.Range(0, monsterDict[level].Count)];
            if (randomMonster.name != compare)
            {
                break;
            }
        }

        compare = randomMonster.name;
        image.sprite = randomMonster;

        if (IsBoss)
        {
            var scaleChange = new Vector3(0.3f, 0.3f, 0.3f);
            GameObject.FindGameObjectWithTag("Click").GetComponent<RectTransform>().transform.localScale += scaleChange;
            IsBoss = false;
        }
    }  
}
