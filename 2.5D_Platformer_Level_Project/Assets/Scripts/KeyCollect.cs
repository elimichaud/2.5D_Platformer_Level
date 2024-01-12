using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollect : MonoBehaviour
{
    private bool asKey = false;

    public bool GetAsKey()
    {
        return asKey;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            asKey = true;
            Destroy(other.gameObject);
        }
    }
}
