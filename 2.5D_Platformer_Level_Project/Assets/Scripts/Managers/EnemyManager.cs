using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private float enemyRespawnCounter = 0f;
    
    [SerializeField]
    private Vector3 slimeSpawnLocation = new Vector3(-7.76f,-1.926f,9.86f);
    
    [SerializeField]
    private Vector3 shellSpawnLocation = new Vector3(-7.76f,-1.926f,9.86f);
    
    [SerializeField]
    private Quaternion spawnRotation = new Quaternion(0, 1, 0, 0);
    
    [SerializeField]
    private  GameObject shellPrefab;
    
    [SerializeField]
    private  GameObject slimePrefab;

    [SerializeField]
    private  float respawnTimer = 90f;

    void FixedUpdate()
    {
        enemyRespawnCounter++;

        if(GameObject.FindGameObjectsWithTag("pic-pic").Length > 0 && shellPrefab != null) {
            enemyRespawnCounter = 0;
        }

        if(enemyRespawnCounter > respawnTimer) {
            if(shellPrefab != null) {
                Instantiate(shellPrefab, shellSpawnLocation, spawnRotation);
            }
            if(slimePrefab != null) {
                Instantiate(slimePrefab, slimeSpawnLocation, spawnRotation);
            }
        }
    }
}
