using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cherry : MonoBehaviour
{
    [SerializeField] private GameManager _gm;
    [SerializeField] private Gym10Manager _g10m; 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_g10m != null)
            {
                _g10m.RestartLevel();
            }
            else
            {
                //_gm.RestartLevel();    
            }
        }
    }
}
