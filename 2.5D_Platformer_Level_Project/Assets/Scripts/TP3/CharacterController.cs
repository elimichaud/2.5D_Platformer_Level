using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public ParticleSystem dustParticle;
    private Animator m_animator;
    private Transform m_transform;
    private bool isFacingRight;
    
    //Sounds
    private AudioSource stepingSound;
    private bool SprintingSoundIsPlaying;
    private bool WalkingSoundIsPlaying;
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_transform = GetComponent<Transform>();
        isFacingRight = true;

        SprintingSoundIsPlaying = false;
        WalkingSoundIsPlaying = false;
        stepingSound = GameObject.Find("SprintSound").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_animator != null)
        {
            if (Input.GetKey(KeyCode.D))
            {
                if (!isFacingRight)
                {
                    StartCoroutine(ChangeDirection());
                    isFacingRight = true;
                }
                    
                ManageMovement();
            }
            else if (Input.GetKey(KeyCode.A))
            {
                if (isFacingRight)
                {

                    StartCoroutine(ChangeDirection());
                    isFacingRight = false;
                }
                    
                ManageMovement();
            }
            else
            {
                m_animator.SetBool("isMoving", false);
                m_animator.SetBool("isSprinting", false);
            }
        }

        if (m_animator.GetBool("isMoving") == false)
        {
            stepingSound.Pause();
        }

    }

    void ManageMovement()
    {
        m_animator.SetBool("isMoving", true);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            MakeDust();
            if (!SprintingSoundIsPlaying)
            {
                StartCoroutine(PlaySprintingSound());
            }
            m_animator.SetBool("isSprinting", true);
        }
        else
        {
            if (!WalkingSoundIsPlaying)
            {
                StartCoroutine(PlayWalkingSound());
            }
            m_animator.SetBool("isSprinting", false);
            StopDust();
        }
    }

    IEnumerator ChangeDirection()
    {
        m_transform.transform.Rotate(0.0f, 10.0f, 0.0f);
        for(int i = 0; i < 17; i++)
        {
            yield return new WaitForSeconds(0.01f);
            m_transform.transform.Rotate(0.0f, 10.0f, 0.0f);
        }
    }
    
    IEnumerator PlaySprintingSound()
    {
        SprintingSoundIsPlaying = true;
        stepingSound.Play();
        yield return new WaitForSeconds(0.180f);
        SprintingSoundIsPlaying = false;
    }
    
    IEnumerator PlayWalkingSound()
    {
        WalkingSoundIsPlaying = true;
        stepingSound.Play();
        yield return new WaitForSeconds(0.260f);
        WalkingSoundIsPlaying = false;
    }

    void MakeDust()
    {
        if(!dustParticle.isPlaying)
            dustParticle.Play();
    }

    void StopDust()
    {
        if(dustParticle.isPlaying)
            dustParticle.Stop();
    }
}
