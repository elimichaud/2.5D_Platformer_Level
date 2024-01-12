using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MimicController : MonoBehaviour
{
    private float m_Timer;
    private float m_TimeLimit;
    private Animator m_Animator;
    private bool m_IsAttacking;
    private Transform m_PlayerTransform;
    private SoundManager m_SoundManager;
    
    // Start is called before the first frame update
    void Start()
    {
        m_Timer = 0.0f;
        m_Animator = gameObject.GetComponent<Animator>();
        m_IsAttacking = false;
        m_TimeLimit = Random.Range(2.5f, 3.5f);
        m_PlayerTransform = GameObject.Find("MaleCharacterPBR").GetComponent<Transform>();
        m_SoundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float playerDistance = Vector3.Distance(gameObject.transform.position, m_PlayerTransform.position);
        m_Timer += Time.deltaTime;
        if (m_Timer >= m_TimeLimit && playerDistance <= 12f)
        {
            m_SoundManager.PlayMimicAttackSound();
            m_Animator.SetBool("Attack", true);
            m_IsAttacking = true;
            m_Timer = 0.0f;
            m_TimeLimit = Random.Range(2.5f, 3.5f);
        }

        if (m_IsAttacking && m_Timer >= 0.37f)
        {
            m_IsAttacking = false;
            m_Animator.SetBool("Attack", false);
            m_Timer = 0.0f;
        }
    }
}
