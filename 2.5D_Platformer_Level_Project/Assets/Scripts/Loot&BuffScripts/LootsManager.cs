using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LootsManager : MonoBehaviour
{
    public GameObject gluGluLoot;
    public GameObject picPicLoot;
    public GameObject heartLoot;
    public TMP_Text UIGluGluBallCount;
    public TMP_Text UISpikeCount;
    public TMP_Text UIHeartCount;
    public TMP_Text UIAttack;
    public TMP_Text UIDefense;
    //public TMP_Text UIMaxHP;
    public PlayerController playerController;
    public PlayerHealth playerHealth;
    private int gluGluBallCount;
    private int spikeCount;
    private int heartCount;
    private int attack;
    private int defense;
    //private int maxHP;

    //Buttons
    [SerializeField] private GameObject m_AttackBuffButtonGO;
    [SerializeField] private GameObject m_DefenceBuffButtonGO;
    [SerializeField] private GameObject m_HPBuffButtonGO;

    // Loot number
    [SerializeField] private int nbLootPerGluglu = 2;
    [SerializeField] private int nbLootPerPicpic = 2;
    [SerializeField] private int nbLootGlugluPerMimique = 1;
    [SerializeField] private int nbLootPicpicPerMimique = 1;
    [SerializeField] private int nbLootHeartPerMimique = 2;
    
    private Button m_AttackBuffButton;
    private Button m_DefenceBuffButton;
    private Button m_HPBuffButton;
    
    //Managers
    [SerializeField] private BuffMenu m_BuffMenu;

    // Start is called before the first frame update
    void Start()
    {
        gluGluBallCount = 0;
        spikeCount = 0;
        heartCount = 0;
        attack = 0;
        defense = 0;

        //Constants
        m_AttackBuffButton = m_AttackBuffButtonGO.GetComponent<Button>();
        m_DefenceBuffButton = m_DefenceBuffButtonGO.GetComponent<Button>();
        m_HPBuffButton = m_HPBuffButtonGO.GetComponent<Button>();

        
        UpdateButtons();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateButtons();
    }
    

    public void CollectLoot(string lootName)
    {
        switch (lootName)
        {
            case "GluGluBall":
                gluGluBallCount++;
                UIGluGluBallCount.text = gluGluBallCount.ToString();
                break;
            case "Spike":
                spikeCount++;
                UISpikeCount.text = spikeCount.ToString();
                break;
            case "Heart":
                heartCount++;
                UIHeartCount.text = heartCount.ToString();    
                break;
        }
    }

    public int GetGluGluBallCount()
    {
        return gluGluBallCount;
    }

    public int GetSpikeCount()
    {
        return spikeCount;
    }
    public int GetHeartCount()
    {
        return heartCount;
    }

    public void IncreaseAttack(int attackPointToAdd)
    {
        attack += attackPointToAdd;
        playerController.damageBuff += attackPointToAdd;
        UIAttack.text = attack.ToString();
    }

    public void IncreaseDefense(int defensePointToAdd)
    {
        defense += defensePointToAdd;
        playerController.defenseBuff += defensePointToAdd;
        UIDefense.text = defense.ToString();
    }

    public void IncreaseMaxHP(int hpToAdd)
    {
        playerHealth.IncreaseMaxHealth((float)hpToAdd);
    }

    public void SpendLoot(int gluGluBallSpent, int spikeSpent, int heartSpent)
    {
        gluGluBallCount -= gluGluBallSpent;
        spikeCount -= spikeSpent;
        heartCount -= heartSpent;
        UpdateUILootCounts();
    }

    public void InitializeLootsInstantiation(string ennemyTag, Vector3 position)
    {
        int nbGlugluLootToInstantiate = 0;
        int nbPicpicLootToInstantiate = 0;
        int nbHeartLootToInstantiate = 0;
        switch (ennemyTag)
        {
            case "Slime":
                nbGlugluLootToInstantiate = nbLootPerGluglu;
                break;
            case "pic-pic":
                nbPicpicLootToInstantiate = nbLootPerPicpic;
                break;
            case "Mimic":
                nbGlugluLootToInstantiate = nbLootGlugluPerMimique;
                nbPicpicLootToInstantiate = nbLootPicpicPerMimique;
                nbHeartLootToInstantiate = nbLootHeartPerMimique;
                break;
        }
        GameObject[] instantiationArray = new GameObject[nbGlugluLootToInstantiate + nbPicpicLootToInstantiate + nbHeartLootToInstantiate];
        for(int i = 0; i < nbGlugluLootToInstantiate; i++)
            instantiationArray[i] = gluGluLoot;
        for (int i = nbGlugluLootToInstantiate; i < nbGlugluLootToInstantiate + nbPicpicLootToInstantiate; i++)
            instantiationArray[i] = picPicLoot;
        for (int i = nbGlugluLootToInstantiate + nbPicpicLootToInstantiate; i < instantiationArray.Length; i++)
            instantiationArray[i] = heartLoot;
        InstantiateLoots(instantiationArray, position);
    }

    private void InstantiateLoots(GameObject[] instantiationArray, Vector3 position)
    {
        float spaceBetween = 0.2f;
        float zPosition = position.z - (instantiationArray.Length / 2) * spaceBetween;
        for(int i = 0; i < instantiationArray.Length; i++)
        {
            zPosition += spaceBetween;
            Quaternion rotation = instantiationArray[i] == heartLoot ? Quaternion.Euler(0.0f, 90.0f, 0.0f) : Quaternion.identity;
            Instantiate(instantiationArray[i], new Vector3(-3.0f, position.y + 0.3f, zPosition), rotation);
        }
    }

    private void UpdateUILootCounts()
    {
        UIGluGluBallCount.text = gluGluBallCount.ToString();
        UISpikeCount.text = spikeCount.ToString();
        UIHeartCount.text = heartCount.ToString();

    }
    
    void UpdateButtons()
    {
        //Buff Buttons
        if (gluGluBallCount < m_BuffMenu.glugluCostAttackBuff || spikeCount < m_BuffMenu.picpicCostAttackBuff)
        {
            m_AttackBuffButton.interactable = false;
        }
        else
        {
            m_AttackBuffButton.interactable = true;
        }
        
        if (gluGluBallCount < m_BuffMenu.glugluCostDefenseBuff || spikeCount < m_BuffMenu.picpicCostDefenseBuff)
        {
            m_DefenceBuffButton.interactable = false;
        }
        else
        {
            m_DefenceBuffButton.interactable = true;
        }
        
        if (gluGluBallCount < m_BuffMenu.glugluCostHpBuff || spikeCount < m_BuffMenu.picpicCostHpBuff || heartCount < m_BuffMenu.heartCostHpBuff)
        {
            m_HPBuffButton.interactable = false;
        }
        else
        {
            m_HPBuffButton.interactable = true;
        }
    }

}
