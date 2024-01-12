using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnvCollision : MonoBehaviour
{
    void OnTriggerEnter(Collider obj) {
        if(obj.tag != "InvisTrigger" && obj.tag != "Loot") {
            Destroy(gameObject);
        }
    }
}
