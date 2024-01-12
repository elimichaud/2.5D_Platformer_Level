using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gym10Enemies : MonoBehaviour
{
    private int m_SlimeHp;
    [SerializeField] private TextMeshPro HPSlimeText;
    private GameObject m_Slime;
    public bool m_SlimeIsDead;
    [SerializeField] private Animator slimeAnim;
 
    private int m_TurtleHp;
    [SerializeField] private TextMeshPro HPTurtleText;
    private GameObject m_Turtle;
    public bool m_TurtleIsDead;
    [SerializeField] private Animator turtleAnim;
    
    // Start is called before the first frame update
    void Start()
    {
        m_Slime = GameObject.Find("Slime");
        m_Turtle = GameObject.Find("TurtleShell");
        m_SlimeHp = 25;
        HPSlimeText.text = m_SlimeHp.ToString();
        m_TurtleHp = 25;
        HPTurtleText.text = m_TurtleHp.ToString();
        m_SlimeIsDead = false;
        m_TurtleIsDead = false;
    }

    private void Update()
    {
        if (m_SlimeHp <= 0 && !m_SlimeIsDead)
        {
            KillSlime();
            m_SlimeIsDead = true;
        }
        
        if (m_TurtleHp <= 0 && !m_TurtleIsDead)
        {
            KillTurtle();
            m_TurtleIsDead = true;
        }
    }

    public void UpdateSlimeHp(int hit)
    {
        m_SlimeHp -= hit;
        if (m_SlimeHp < 0)
        {
            HPSlimeText.color = Color.red;
            m_SlimeHp = 0;
        }
        HPSlimeText.text = m_SlimeHp.ToString();
    }
    
    public void UpdateTurtleHp(int hit)
    {
        m_TurtleHp -= hit;
        if (m_TurtleHp < 0)
        {
            HPTurtleText.color = Color.red;
            m_TurtleHp = 0;
        }
        HPTurtleText.text = m_TurtleHp.ToString();
    }

    private void KillSlime()
    {
        slimeAnim.SetBool("isDead", true);
        StartCoroutine(KillEnnemiTimer(m_Slime));
    }
    
    private void KillTurtle()
    {
        turtleAnim.SetBool("isDead", true);
        StartCoroutine(KillEnnemiTimer(m_Turtle));
    }
    
    
    IEnumerator KillEnnemiTimer(GameObject ennemy)
    {
        yield return new WaitForSeconds(1.50f);
        Destroy(ennemy);
    }
}
