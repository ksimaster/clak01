using System;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
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

    private Func<int, int> clickValuePrices = (int i) => i == 0 ? 150 : (int)Mathf.Round(150 * (i + 1) * Mathf.Log(i + 2));
    private Func<int, int> perSecondPrices = (int i) => i == 0 ? 180 : (int)Mathf.Round(180 * (i + 1) * Mathf.Log(i + 2));
    private Func<int, int> critChancePrices = (int i) => (int)Mathf.Round(10 * Mathf.Pow(2, i));
    private Func<int, int> critDamagePrices = (int i) => i == 0 ? 1 : (int)Mathf.Round((i + 1) * (i + 1) * Mathf.Log(i + 2));

    private Func<int, int> clickValueValue = (int i) => i == 0 ? 1 : (int)Mathf.Round((i + 1) * Mathf.Log(i + 2) - (i) * Mathf.Log(i + 1));
    private Func<int, int> perSecondValue = (int i) => i == 0 ? 1 : (int)Mathf.Round((i + 1) * Mathf.Log(2*i + 2) - (i) * Mathf.Log(2*i + 1));
    private Func<int, int> critChanceValue = (int i) => 1;
    private Func<int, int> critDamageValue = (int i) => 1;


    public bool calledUpgrade = false;
    
    private int perSecondIndexRef = 0;
    private int valueIndexRef = 0;
    private int critChanceIndexRef = 0;
    private int сritDamageIndexRef = 0;

    private void Update()
    {
    }

    public void PerSecondUpgrade()
    {
        if (ScoreManager.score < perSecondPrices(perSecondIndexRef))
            return;

        ScoreManager.score -= perSecondPrices(perSecondIndexRef);
        ClickButton.amountPerSecond += perSecondValue(perSecondIndexRef);
        ScoreManager.Increase();
        calledUpgrade = true;
        perSecondIndexRef++;
        perSecondText.text = "Автоклик: +" + perSecondValue(perSecondIndexRef);
        perSecondPriceText.text = "Цена: " + perSecondPrices(perSecondIndexRef);
    }

    public void ClickValueUpgrade()
    {
        if (ScoreManager.score < clickValuePrices(valueIndexRef))
            return;

        ScoreManager.score -= clickValuePrices(valueIndexRef);
        ClickButton.clickValue += clickValueValue(valueIndexRef);
        ScoreManager.Increase();
        calledUpgrade = true;
        valueIndexRef++;
        clickValueText.text = "Сила заклинаний: +" + clickValueValue(valueIndexRef);
        clickValuePriceText.text = "Цена: " + clickValuePrices(valueIndexRef);
    }


    public void CritChanceUpgrade()
    {
        if (ScoreManager.score < critChancePrices(critChanceIndexRef))
            return;

        ScoreManager.score -= critChancePrices(critChanceIndexRef);
        CriticalHit.critChance += critChanceValue(critChanceIndexRef);
        ScoreManager.Increase();
        calledUpgrade = true;
        critChanceIndexRef++;
        critChanceText.text = "Шанс: +" + critChanceValue(critChanceIndexRef);
        critChancePriceText.text = "Цена: " + critChancePrices(critChanceIndexRef);
    }

    public void CritDamageUpgrade()
    {
        if (ScoreManager.score < critDamagePrices(сritDamageIndexRef))
            return;

        ScoreManager.score -= critDamagePrices(сritDamageIndexRef);
        CriticalHit.critDamage += critDamageValue(сritDamageIndexRef);
        ScoreManager.Increase();
        calledUpgrade = true;
        сritDamageIndexRef++;
        critDamageText.text = "Сила криты: +" + critDamageValue(сritDamageIndexRef);
        critDamagePriceText.text = "Цена: " + critDamagePrices(сritDamageIndexRef);
    }
}