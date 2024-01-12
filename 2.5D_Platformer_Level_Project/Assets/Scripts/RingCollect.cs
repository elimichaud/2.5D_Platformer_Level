using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingCollect : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    private void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (gameObject.name)
            {
                case "BlueRing":
                    gameManager.CollectBlueRing();
                    Destroy(this.gameObject);
                    break;
                case "GreenRing":
                    gameManager.CollectGreenRing();
                    Destroy(this.gameObject);
                    break;
                case "RedRing":
                    gameManager.CollectRedRing();
                    Destroy(this.gameObject);
                    break;
            }

            
        }
    }
}
