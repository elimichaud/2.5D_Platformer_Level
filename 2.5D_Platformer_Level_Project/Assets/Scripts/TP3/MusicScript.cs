using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    public static MusicScript instance;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if(instance == null){
            instance = this;
        }else {
            Destroy(gameObject);
        }
    }

    
}
