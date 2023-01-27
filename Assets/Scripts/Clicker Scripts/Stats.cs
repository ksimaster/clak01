using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    private Text _currentClickDamage;
    private Text _currentDPS;
    private Text _currentCritChance;
    private Text _currentCritDamage;
    private float _critDamageHolder;
    
    public UpgradeManager upgradeManager; 
    // Start is called before the first frame update
    void Start()
    {
        _currentClickDamage = GameObject.FindGameObjectWithTag("ClickDamageText").GetComponent<Text>();
        _currentDPS = GameObject.FindGameObjectWithTag("DPSText").GetComponent<Text>();
        _currentCritChance = GameObject.FindGameObjectWithTag("CritChanceText").GetComponent<Text>();
        _currentCritDamage = GameObject.FindGameObjectWithTag("CritDamageText").GetComponent<Text>();

        upgradeManager = GameObject.FindGameObjectWithTag("UpgradeManager").GetComponent<UpgradeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (upgradeManager.calledUpgrade) 
        {
            upgradeManager.calledUpgrade = false;

            // updates all the text elements
            _currentClickDamage.text = "Урон: " + ClickButton.clickValue;
            _currentDPS.text = "Автоклик: " + ClickButton.amountPerSecond;
            _currentCritChance.text = "Шанс криты: " + CriticalHit.critChance + "%";

            _critDamageHolder = Mathf.Round(CriticalHit.critDamage * 10f) / 10f;
            _currentCritDamage.text = "Урон криты: " + _critDamageHolder + "x";
        }
    }
}
