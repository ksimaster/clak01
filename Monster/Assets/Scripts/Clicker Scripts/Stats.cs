using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    private Text _currentClickDamage;
    private Text _currentDPS;
    private Text _currentCritChance;
    private Text _currentCritDamage;
    private float _critDamageHolder;
    
    public UpgradeManager UpgradeManager; 

    // Start is called before the first frame update
    void Start()
    {
        _currentClickDamage = GameObject.FindGameObjectWithTag("ClickDamageText").GetComponent<Text>();
        _currentDPS = GameObject.FindGameObjectWithTag("DPSText").GetComponent<Text>();
        _currentCritChance = GameObject.FindGameObjectWithTag("CritChanceText").GetComponent<Text>();
        _currentCritDamage = GameObject.FindGameObjectWithTag("CritDamageText").GetComponent<Text>();

        UpgradeManager = GameObject.FindGameObjectWithTag("UpgradeManager").GetComponent<UpgradeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (UpgradeManager.calledUpgrade) 
        {
            UpgradeManager.calledUpgrade = false;

            // updates all the text elements
            _currentClickDamage.text = "Сила заклинаний: " + UpgradeManager.ClickMonster.clickValue;
            _currentDPS.text = "Авто заклятье: " + UpgradeManager.ClickMonster.amountPerSecond;
            _currentCritChance.text = "Шанс крита: " + UpgradeManager.CritChance + "%";

            _critDamageHolder = Mathf.Round(UpgradeManager.CritDamage * 10f) / 10f;
            _currentCritDamage.text = "Крит: " + _critDamageHolder + "x";
        }
    }
}
