using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemySwitcher : MonoBehaviour
{
    public Button click;
    public Image image;
    public Sprite[] monsters = new Sprite[4];
    
    public bool spawnEnemy = false;
    private int compare = 0;

    // Start is called before the first frame update
    void Start()
    {
        click = GameObject.FindGameObjectWithTag("Click").GetComponent<Button>();
        image = click.GetComponent<Image>();
        monsters[0] = Resources.Load<Sprite>("Monster0");
        monsters[1] = Resources.Load<Sprite>("Monster1");
        monsters[2] = Resources.Load<Sprite>("Monster2");
        monsters[3] = Resources.Load<Sprite>("Monster2");
        image.sprite = monsters[0];
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
        int random = Random.Range(0, monsters.Length);

        while (random == compare)
        {
            random = Random.Range(0, monsters.Length);
        }

        compare = random;
        image.sprite = monsters[random];
    }  
}
