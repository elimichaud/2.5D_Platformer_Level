using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnDeath : StateMachineBehaviour 
{
    [SerializeField]
    private float vfxSpawnOffset;

    [SerializeField]
    private GameObject deathVFX;

    [SerializeField]
    private float durationFraction = 3f;

    [SerializeField]
    private GameObject Key;

    private Vector3 spawnLocation;

    private LootsManager lootsManager;

    private string ennemyTag;

    private string ennemyName;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        Destroy(animator.gameObject, animatorStateInfo.length / 3);
        spawnLocation = animator.gameObject.transform.position;

        GameObject.FindGameObjectsWithTag("EnemyManager")[0].GetComponent<CoroutineHandler>().createCoroutine(waitForDuration(animatorStateInfo.length / durationFraction));
        lootsManager = GameObject.Find("LootsManager").GetComponent<LootsManager>();
        ennemyTag = animator.gameObject.tag;
        ennemyName = animator.gameObject.name;
    }

    private void startDeathEffect() {
        Vector3 effectPosition = spawnLocation;
        effectPosition.y += vfxSpawnOffset;

        GameObject deathEffect = Instantiate(deathVFX, effectPosition, new Quaternion(0, 0, 0, 1));
        deathEffect.GetComponent<ParticleSystem>().Play();
        Destroy(deathEffect, 0.6f);
        lootsManager.InitializeLootsInstantiation(ennemyTag, spawnLocation);
        if(ennemyName == "KeyMimique")
            Instantiate(Key, new Vector3(-3.0f, spawnLocation.y + 0.5f, spawnLocation.z), Quaternion.identity);
    }

    IEnumerator waitForDuration(float duration) {
        yield return new WaitForSeconds(duration);
        startDeathEffect();
    }
}
