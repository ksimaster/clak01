using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    private const string ClickValueButton = "ClickValueButton";
    private const string AutoclickButton = "AutoclickButton";
    private const string CritChanceButton = "CritChanceButton";
    private const string CritValueButton = "CritValueButton";

    [Header("Upgrade Buttons")]
    public GameObject perSecondUpgrade;
    public GameObject clickValueUpgrade;
    public GameObject critDamageUpgrade;
    public GameObject critChanceUpgrade;

    [Header("Price Buttons Texts")]
    public Text perSecondPriceText;
    public Text clickValuePriceText;
    public Text critChancePriceText;
    public Text critDamagePriceText;

    [Header("Value Buttons Texts")]
    public Text perSecondText;
    public Text clickValueText;
    public Text critChanceText;
    public Text critDamageText;

    public ScoreManager ScoreManager;
    public ClickMonster ClickMonster;

    public float CritDamage = 2f;
    public float CritChance = 1f;

    private Func<int, int> clickValuePrices = (int i) => i == 0 ? 50 : (int)Mathf.Round(150 * (i + 1) * Mathf.Log(i + 2));
    private Func<int, int> perSecondPrices = (int i) => i == 0 ? 75 : (int)Mathf.Round(100 * (i + 1) * Mathf.Log(i + 2));
    private Func<int, int> critChancePrices = (int i) => (int)Mathf.Round(10 * (i + 5) * Mathf.Pow(2, i / 2f));
    private Func<int, int> critDamagePrices = (int i) => i == 0 ? 200 : (int)Mathf.Round((i + 5) * (i + 5) * (i + 5) * Mathf.Log(i + 7));

    private Func<int, int> clickValueValue = (int i) => i == 0 ? 1 : (int)Math.Max(1, Mathf.Round((i + 1) / 2 * Mathf.Log(i + 2) - i / 2 * Mathf.Log(i + 1)));
    private Func<int, int> perSecondValue = (int i) => i == 0 ? 2 : (int)(2 * (Mathf.Round((i + 1) * Mathf.Log(2 * i + 2) - (i) * Mathf.Log(2 * i + 1))));
    private Func<int, int> critChanceValue = (int i) => 1;
    private Func<int, int> critDamageValue = (int i) => 1;


    public bool calledUpgrade = false;

    private int perSecondIndexRef = 0;
    private int valueIndexRef = 0;
    private int critChanceIndexRef = 0;
    private int сritDamageIndexRef = 0;

    private Dictionary<string, Func<bool>> TagToCheckDict = new Dictionary<string, Func<bool>>();
    private Dictionary<string, Button> TagToButtonDict = new Dictionary<string, Button>();


    private void Start()
    {
        TagToCheckDict.Add(ClickValueButton, CheckClickValue);
        TagToCheckDict.Add(AutoclickButton, CheckPerSecond);
        TagToCheckDict.Add(CritChanceButton, CheckCritChance);
        TagToCheckDict.Add(CritValueButton, CheckCritDamage);

        foreach (var key in TagToCheckDict.Keys)
        {
            TagToButtonDict.Add(key, GameObject.FindGameObjectWithTag(key).GetComponent<Button>());
        }
    }

    private void Update()
    {
    }

    public bool CheckPerSecond() => ScoreManager.score >= perSecondPrices(perSecondIndexRef);

    public void PerSecondUpgrade()
    {
        if (!CheckPerSecond())
            return;

        ScoreManager.score -= perSecondPrices(perSecondIndexRef);
        ClickMonster.amountPerSecond += perSecondValue(perSecondIndexRef);
        ScoreManager.Increase();
        calledUpgrade = true;
        perSecondIndexRef++;
        perSecondText.text = "Авто заклятье +" + perSecondValue(perSecondIndexRef);
        perSecondPriceText.text = "Цена: " + perSecondPrices(perSecondIndexRef);
    }

    public bool CheckClickValue() => ScoreManager.score >= clickValuePrices(valueIndexRef);

    public void ClickValueUpgrade()
    {
        if (!CheckClickValue())
            return;

        ScoreManager.score -= clickValuePrices(valueIndexRef);
        ClickMonster.clickValue += clickValueValue(valueIndexRef);
        ScoreManager.Increase();
        calledUpgrade = true;
        valueIndexRef++;
        clickValueText.text = "Сила заклинаний +" + clickValueValue(valueIndexRef);
        clickValuePriceText.text = "Цена: " + clickValuePrices(valueIndexRef);
    }

    public bool CheckCritChance() => ScoreManager.score >= critChancePrices(critChanceIndexRef);

    public void CritChanceUpgrade()
    {
        if (!CheckCritChance())
            return;

        ScoreManager.score -= critChancePrices(critChanceIndexRef);
        CritChance += critChanceValue(critChanceIndexRef);
        ScoreManager.Increase();
        calledUpgrade = true;
        critChanceIndexRef++;
        critChanceText.text = "Шанс крита +" + critChanceValue(critChanceIndexRef);
        critChancePriceText.text = "Цена: " + critChancePrices(critChanceIndexRef);
    }

    public bool CheckCritDamage() => ScoreManager.score >= critDamagePrices(сritDamageIndexRef);

    public void CritDamageUpgrade()
    {
        if (!CheckCritDamage())
            return;

        ScoreManager.score -= critDamagePrices(сritDamageIndexRef);
        CritDamage += critDamageValue(сritDamageIndexRef);
        ScoreManager.Increase();
        calledUpgrade = true;
        сritDamageIndexRef++;
        critDamageText.text = "Сила крита +" + critDamageValue(сritDamageIndexRef);
        critDamagePriceText.text = "Цена: " + critDamagePrices(сritDamageIndexRef);
    }

    public void UpdateButtons()
    {
        foreach (var key in TagToCheckDict.Keys)
        {
            var button = TagToButtonDict[key];
            var isActive = TagToCheckDict[key]();
            if (button.interactable != isActive)
            {
                button.interactable = isActive;
            }
        }
    }
}