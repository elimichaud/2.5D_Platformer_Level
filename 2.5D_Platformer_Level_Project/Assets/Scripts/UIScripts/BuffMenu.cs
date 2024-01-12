using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuffMenu : MonoBehaviour
{
    //Buttons
    [Header("Button Variables")] 
    [SerializeField] private Button closeMenu;
    [SerializeField] private Button getAttackBuff;
    [SerializeField] private Button getDefenseBuff;
    [SerializeField] private Button getHpBuff;
    
    //Managers
    [Space(5)]
    [Header("Managers")]
    [SerializeField] private LootsManager lootsManager;
    
    //Texts
    [Space(5)]
    [Header("Text Variables")]
    [SerializeField] private TMP_Text attackBuffValueText;
    [SerializeField] private TMP_Text defenseBuffValueText;
    [SerializeField] private TMP_Text hpBuffValueText;
    [SerializeField] private TMP_Text glugluCostAttackBuffText;
    [SerializeField] private TMP_Text picpicCostAttackBuffText;
    [SerializeField] private TMP_Text heartCostAttackBuffText;
    [SerializeField] private TMP_Text glugluCostDefenseBuffText;
    [SerializeField] private TMP_Text picpicCostDefenseBuffText;
    [SerializeField] private TMP_Text heartCostDefenseBuffText;
    [SerializeField] private TMP_Text glugluCostHpBuffText;
    [SerializeField] private TMP_Text picpicCostHpBuffText;
    [SerializeField] private TMP_Text heartCostHpBuffText;
    
    //Number Valuers
    [Space(5)]
    [Header("Number Values")]
    public int attackBuffValue = 1;
    public int defenseBuffValue = 1;
    public int HpBuffValue = 5;
    public int glugluCostAttackBuff = 1;
    public int picpicCostAttackBuff = 3;
    public int heartCostAttackBuff = 0;
    public int glugluCostDefenseBuff = 3;
    public int picpicCostDefenseBuff = 1;
    public int heartCostDefenseBuff = 0;
    public int glugluCostHpBuff = 2;
    public int picpicCostHpBuff = 2;
    public int heartCostHpBuff = 2; 
    
    //Sounds
    [Space(5)]
    [Header("Sound variables")]
    [SerializeField] private AudioSource m_ClickSound;

    // Start is called before the first frame update
    void Start()
    {
        closeMenu.onClick.AddListener(CloseBuffMenu);
        getAttackBuff.onClick.AddListener(GetAttackBuff);
        getDefenseBuff.onClick.AddListener(GetDefenseBuff);
        getHpBuff.onClick.AddListener(GetHPBuff);
        InitializeCostAndBuffValueUI();
    }

    void CloseBuffMenu()
    {
        m_ClickSound.Play();
        gameObject.SetActive(false);
    }

    void GetAttackBuff()
    {
        if(hasEnoughLootToGetBuff(glugluCostAttackBuff, picpicCostAttackBuff, heartCostAttackBuff))
        {
            m_ClickSound.Play();
            lootsManager.SpendLoot(glugluCostAttackBuff, picpicCostAttackBuff, heartCostAttackBuff);
            lootsManager.IncreaseAttack(attackBuffValue);
        }
            
        else
            Debug.Log("You don't have the required material to get this buff");
    }
    void GetDefenseBuff()
    {
        if (hasEnoughLootToGetBuff(glugluCostDefenseBuff, picpicCostDefenseBuff, heartCostDefenseBuff))
        {
            m_ClickSound.Play();
            lootsManager.IncreaseDefense(defenseBuffValue);
            lootsManager.SpendLoot(glugluCostDefenseBuff, picpicCostDefenseBuff, heartCostDefenseBuff);
        }

        else
            Debug.Log("You don't have the required material to get this buff");
    }
    void GetHPBuff()
    {
        if (hasEnoughLootToGetBuff(glugluCostHpBuff, picpicCostHpBuff, heartCostHpBuff))
        {
            m_ClickSound.Play();
            lootsManager.IncreaseMaxHP(HpBuffValue);
            lootsManager.SpendLoot(glugluCostHpBuff, picpicCostHpBuff, heartCostHpBuff);
        }

        else
            Debug.Log("You don't have the required material to get this buff");
    }

    private bool hasEnoughLootToGetBuff(int requiredGluGluBall, int requiredSpike, int requiredHeart)
    {
        return lootsManager.GetGluGluBallCount() >= requiredGluGluBall &&
            lootsManager.GetSpikeCount() >= requiredSpike &&
            lootsManager.GetHeartCount() >= requiredHeart;
    }

    private void InitializeCostAndBuffValueUI()
    {
        attackBuffValueText.text = "+ " + attackBuffValue.ToString() + " :";
        defenseBuffValueText.text = "+ " + defenseBuffValue.ToString() + " :";
        hpBuffValueText.text = "+ " + HpBuffValue.ToString() + " :";

        glugluCostAttackBuffText.text = glugluCostAttackBuff.ToString();
        picpicCostAttackBuffText.text = picpicCostAttackBuff.ToString();
        heartCostAttackBuffText.text = heartCostAttackBuff.ToString();

        glugluCostDefenseBuffText.text = glugluCostDefenseBuff.ToString();
        picpicCostDefenseBuffText.text = picpicCostDefenseBuff.ToString();
        heartCostDefenseBuffText.text = heartCostDefenseBuff.ToString();

        glugluCostHpBuffText.text = glugluCostHpBuff.ToString();
        picpicCostHpBuffText.text = picpicCostHpBuff.ToString();
        heartCostHpBuffText.text = heartCostHpBuff.ToString();
    }
}
