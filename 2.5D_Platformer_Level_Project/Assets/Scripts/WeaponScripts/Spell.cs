using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spell : MonoBehaviour
{
    private LancerUnSortManager m_Lsm;
    private Gym10Manager m_G10m;
    private GameManager m_Gm;

    private AudioSource spellSound;

    private void Start()
    {
        spellSound = GameObject.Find("SpellHit").GetComponent<AudioSource>();
        m_Gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mimic") || other.CompareTag("pic-pic") )
        {
            spellSound.Play();
            m_Gm.SpellHit(gameObject.transform.position);
            Destroy(gameObject);
        }

        else if (other.CompareTag("Slime"))
        {
            Rigidbody projectileBody = GetComponent<Rigidbody>();
            float spellDirection = 0;
            if (transform.rotation.y > 0)
            {
                spellDirection = 1;
            }
            else
            {
                spellDirection = -1;
            }
            projectileBody.velocity = new Vector3(0f, 0f, 5f * spellDirection);
        }

        else if (other.CompareTag("Player"))
        {
            spellSound.Play();
            m_Gm.SpellHit(gameObject.transform.position);
            Destroy(gameObject);
        }

        else if (!other.CompareTag("Staff"))
        {
            m_Gm.SpellHit(gameObject.transform.position);
            Destroy(gameObject);
        }
    }
    


}
