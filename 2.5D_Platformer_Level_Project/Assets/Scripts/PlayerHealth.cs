using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class PlayerHealth : MonoBehaviour
{
    public Slider slider;
    private float currentHealth;

    [SerializeField]
    private PlayerController player;

    [SerializeField]
    private float shellDamage = 10f;

    [SerializeField]
    private float slimeDamage = 20f;

    [SerializeField]
    private float mimicDamage = 40f;

    [SerializeField]
    private float projectileDamage = 40f;

    [SerializeField]
    private float peachHealing = 20f;

    [SerializeField]
    private float berryHealing = 10f;

    [SerializeField]
    private float maxHealth = 100f;
    
    [SerializeField]
    private GameManager m_GameManager;


    private void Start()
    {
        slider.maxValue = maxHealth;
        currentHealth = maxHealth;
    }
    void FixedUpdate()
    {
        slider.value = currentHealth;
    }

    void OnCollisionEnter(Collision obj)
    { 
        if (obj.gameObject.CompareTag("pic-pic"))
        {
            takeDamage(shellDamage);
        } 
        else if (obj.gameObject.CompareTag("Slime"))
        {
            takeDamage(slimeDamage);
        } 
        else if (obj.gameObject.CompareTag("strawberry"))
        {
            this.currentHealth += berryHealing;// si le joueur a perdu des points de vie il peut les récupérer avec des fruits

            if(currentHealth > maxHealth) {
                currentHealth = maxHealth;
            }

            Destroy(obj.gameObject); // les fruits sont consommés (detruits) lorsqu'ils sont touchés 
        }
        else if (obj.gameObject.CompareTag("peach"))
        {
            this.currentHealth += peachHealing;

            if(currentHealth > maxHealth) {
                currentHealth = maxHealth;
            }

            Destroy(obj.gameObject);
        }
        else if (obj.gameObject.CompareTag("Mimic"))
        {
            takeDamage(mimicDamage);
        }
        else if (obj.gameObject.CompareTag("Spell"))
        {
            takeDamage(m_GameManager.GetSpellDamage());
        }

        if(currentHealth <= 0f) {
            SetDefeat();
        }
    }

    void OnTriggerEnter(Collider obj) {
        if (obj.CompareTag("Projectile"))
        {
            takeDamage(player.handleProjectileCollision(obj, projectileDamage));
        }
        
    }

    void SetDefeat()
    {
        gameObject.GetComponent<PlayerController>().StartDeathAnimation();
    }

    public void IncreaseMaxHealth(float healthToAdd)
    {
        maxHealth += healthToAdd;
        slider.maxValue = maxHealth;
        currentHealth = maxHealth;
    }

    private void takeDamage(float attack) {
        float damage = attack - player.defenseBuff;
        currentHealth -= (damage < 0 ? 0 : damage);

        if(currentHealth <= 0f) {
            SetDefeat();
        }
    }
}
