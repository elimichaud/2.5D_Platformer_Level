using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTrigger : MonoBehaviour
{
    [SerializeField] private GatesManager _gm;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _gm.CloseStartGate();
            Destroy(this.gameObject);
        }
    }
    
    
    //IEnumerator KillEnemies()
    //{
    //    yield return new WaitForSeconds(0.01f);
    //}
}
