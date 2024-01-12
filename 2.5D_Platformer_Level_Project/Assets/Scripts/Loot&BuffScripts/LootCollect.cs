using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootCollect : MonoBehaviour
{
    private GameObject LootsManager;
    
    // Start is called before the first frame update
    void Start()
    {
        LootsManager = GameObject.Find("LootsManager");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && LootsManager != null)
        {
            LootsManager.GetComponent<LootsManager>().CollectLoot(gameObject.name);
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
