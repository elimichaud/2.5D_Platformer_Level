using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField]
    private float health = 20f;

    [SerializeField]
    private PlayerController player;
    
    private GameManager m_GameManager;
    private SoundManager m_SoundManager;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        m_GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        m_SoundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    void OnTriggerEnter(Collider coll) {
        if(coll.CompareTag("MainWeapon")) {
            MainWeapon wpn = coll.gameObject.GetComponent<MainWeapon>();
            
            if(wpn.isAttacking) {
                float damageModifier = 0f;

                if(gameObject.CompareTag("pic-pic")) {
                    damageModifier = wpn.shellEnemyMod;
                    m_SoundManager.PlayEnemyHurt();
                } 
                else if(gameObject.CompareTag("Slime")) {
                    damageModifier = wpn.slimeEnemyMod;
                    if (damageModifier > 0)
                    {
                        m_SoundManager.PlayEnemyHurt();
                    }
                } 
                else if(gameObject.CompareTag("Mimic")) {
                    damageModifier = wpn.mimicEnemyMod; 
                    m_SoundManager.PlayEnemyHurt();
                }

                health -= (wpn.attackValue + player.damageBuff) * damageModifier;

                if(health <= 0f) {
                    animator.ResetTrigger("Death");
                    animator.SetTrigger("Death");
                } else if (!(wpn.attackValue * damageModifier == 0)) {
                    animator.ResetTrigger("Hit");
                    animator.SetTrigger("Hit");
                }

                wpn.isAttacking = false;
            }
        }

        if (coll.CompareTag("Spell") && !gameObject.CompareTag("Slime"))
        {
            float spellDamage = m_GameManager.GetSpellDamage();
            health -= (spellDamage + player.damageBuff);

            if(health <= 0f) {
                animator.ResetTrigger("Death");
                animator.SetTrigger("Death");
            } 
        }
    }
}
