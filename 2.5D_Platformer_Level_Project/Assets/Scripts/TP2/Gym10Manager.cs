using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gym10Manager : MonoBehaviour
{
    [SerializeField] private GameObject gateStart;
    [SerializeField] private GameObject gateEnd;

    [SerializeField] private TextMeshProUGUI enemiesText;
    [SerializeField] private TextMeshProUGUI enemiesTextConst;
    private int m_EnemiesAmount;

    [SerializeField] private GameObject SpellHitVFX;

    [SerializeField] private Gym10Enemies g10e;
    
    
    private AudioSource spellAura;
    
    public int m_SpellDamage;
    private bool isPoweredUp;

    [SerializeField] private Animator playerAnim;
    
    private bool m_AlreadyCountedSlime;
    private bool m_AlreadyCountedTurtle;

    // Start is called before the first frame update
    void Start()
    {
        enemiesText.color = Color.red;
        enemiesTextConst.color = Color.red;
        m_EnemiesAmount = 2;
        m_SpellDamage = 10;
        isPoweredUp = false;
        m_AlreadyCountedSlime = false;
        m_AlreadyCountedTurtle = false;
        
        spellAura = GameObject.Find("SpellAura").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        enemiesText.text = m_EnemiesAmount.ToString();
        if (m_EnemiesAmount == 0)
        {
            enemiesText.color = Color.green;
            enemiesTextConst.color = Color.green;
            enemiesText.text = "";
            enemiesTextConst.text = "Aucun ennemi restant!";
            OpenGates();
        }
        
        if (m_EnemiesAmount == 1)
        {
            enemiesTextConst.text = " ennemi restant";
        }

        if (g10e.m_SlimeIsDead && !m_AlreadyCountedSlime)
        {
            m_EnemiesAmount--;
            m_AlreadyCountedSlime = true;
        }
        
        if (g10e.m_TurtleIsDead && !m_AlreadyCountedTurtle)
        {
            m_EnemiesAmount--;
            m_AlreadyCountedTurtle = true;
        }
        
        if (Input.GetMouseButtonDown(2))
        {
            if (!isPoweredUp)
            {
                spellAura.Play();
                StartCoroutine(PowerUpSpells());
            }
        }
    }


    public void CloseStartGate()
    {
        gateStart.SetActive(true);
    }
    
    public void OpenGates()
    {
        gateStart.SetActive(false);
        gateEnd.SetActive(false);
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene("Gym_10");
    }
    
    IEnumerator PowerUpSpells()
    {
        playerAnim.SetBool("IsPoweringUp", true);
        yield return new WaitForSeconds(0.2f);
        playerAnim.SetBool("IsPoweringUp", false);
        isPoweredUp = true;
        m_SpellDamage = 20;
        yield return new WaitForSeconds(5.00f);
        m_SpellDamage = 10;
        isPoweredUp = false;
    }
    
    public void SpellHit(Vector3 position, String tag, int damage)
    {
        GameObject hitObject = Instantiate(SpellHitVFX, position, new Quaternion(0, 0, 0, 1));
        hitObject.GetComponent<ParticleSystem>().Play();

        if (tag == "Slime")
        {
            g10e.UpdateSlimeHp(damage);
        }
        else if (tag == "pic-pic")
        {
            g10e.UpdateTurtleHp(damage);
        }
    }
}
