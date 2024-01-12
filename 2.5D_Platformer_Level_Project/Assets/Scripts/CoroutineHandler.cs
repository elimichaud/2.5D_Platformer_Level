using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineHandler : MonoBehaviour
{
    public void createCoroutine(IEnumerator function) {
        StartCoroutine(function);
    }
}
