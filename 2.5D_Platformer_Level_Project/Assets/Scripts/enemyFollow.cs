using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class enemyFollow : MonoBehaviour
{
    public NavMeshAgent enemy;

    public Transform playerTarget;
    public Animator animator; // pour animer l'ennemi
    PlayerHealth playerHealth;
    
    float lookRadius = 10f;

    public bool canMove;

    void Start() {
        animator = GetComponent<Animator>();
        enemy = GetComponent<NavMeshAgent>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if(canMove) {
            float distance = Vector3.Distance(playerTarget.position, transform.position);

            if(distance <= lookRadius)
            {
                enemy.SetDestination(playerTarget.position); // pour suivre le joueur
            }

            animator.SetFloat("distanceFromPlayer",distance);

            //tourner l'ennemi lorsqu'il suit le joueur
            Vector3 movementDirection = new Vector3(playerTarget.position.z, 0, 0);

            if (movementDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 10f * Time.deltaTime);
            }
        }
    }
}