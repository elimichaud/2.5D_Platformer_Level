using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SimpleProjectile : MonoBehaviour
{
    [SerializeField]
    float cooldown = 850f;
    
    [SerializeField]
    GameObject projectileModel;
    
    [SerializeField]
    GameObject projectileSpawn;

    [SerializeField]
    float maxYRange = 5f;

    [SerializeField]
    float maxZRange = 24f;

    [SerializeField]
    float orientation = 1f;
    
    private float cooldownValue;
    private Rigidbody body;
    private Animator animator;
    
    private SoundManager m_SoundManager;
    private Transform m_PlayerTransform;

    void Start() {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody>();
        cooldownValue = cooldown;
        m_SoundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        m_PlayerTransform = GameObject.Find("MaleCharacterPBR").GetComponent<Transform>();

        if(cooldown < 850f) {
            cooldown = 850f;
        }
    }

    void Update()
    {
        float playerDistanceZ = Math.Abs(gameObject.transform.position.z - m_PlayerTransform.position.z);
        float playerDistanceY = Math.Abs(gameObject.transform.position.y - m_PlayerTransform.position.y);

        if (maxYRange > playerDistanceY && maxZRange > playerDistanceZ) {
            if(cooldownValue == 0f) {
                cooldownValue = cooldown;
                shootProjectile();
            } else {
                if(cooldownValue == 200) {
                    animator.ResetTrigger("Shoot");
                    animator.SetTrigger("Shoot");
                }
                cooldownValue--;
            }
        }
    }

    private void shootProjectile()
    {
        if (Time.timeScale != 0f)
        {
            m_SoundManager.PlayProjectileSound();
            GameObject projectile = MonoBehaviour.Instantiate(projectileModel, projectileSpawn.transform.position, new Quaternion(0, 0, 0, 1));
            Rigidbody projectileBody = projectile.GetComponent<Rigidbody>();
            projectileBody.velocity = new Vector3(0f, 0f, -10f * orientation);
            MonoBehaviour.Destroy(projectile, 5f);
        }
    }
}
