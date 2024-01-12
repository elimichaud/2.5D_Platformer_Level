using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LancerUnSortManager : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI hptext;
    private int hpvalue;
    [SerializeField] private GameObject SpellHitVFX;
    public int m_SpellDamage;
    private bool isPoweredUp;
    
    private AudioSource spellAura;

    [SerializeField] private Animator playerAnim;
        
    private void Start()
    {
        m_SpellDamage = 10;
        hpvalue = 1000;
        isPoweredUp = false;
        
        spellAura = GameObject.Find("SpellAura").GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            if (!isPoweredUp)
            {
                spellAura.Play();
                StartCoroutine(PowerUpSpells());
            }
        }
    }

    public void SpellHit(Vector3 position, int damage)
    {
        GameObject hitObject = Instantiate(SpellHitVFX, position, new Quaternion(0, 0, 0, 1));
        hitObject.GetComponent<ParticleSystem>().Play();
        hpvalue -= damage;
        hptext.text = hpvalue.ToString();
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
}
