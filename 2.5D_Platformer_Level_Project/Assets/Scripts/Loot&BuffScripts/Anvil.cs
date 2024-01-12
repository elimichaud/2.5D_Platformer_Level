using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Anvil : MonoBehaviour
{
    public GameObject player;
    public GameObject anvilMessage;
    public GameObject BuffMenu;
    private Transform anvilTransform;
    private Transform playerTransform;
    private bool isBuffMenuOpened;
    
    //Managers
    private SoundManager m_SoundManager;
    
    // Start is called before the first frame update
    void Start()
    {
        anvilTransform = gameObject.GetComponent<Transform>();
        playerTransform = player.GetComponent<Transform>();
        isBuffMenuOpened = false;
        
        m_SoundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = Vector3.Distance(anvilTransform.position, playerTransform.position);
        if(distanceFromPlayer <= 3.0f && !anvilMessage.activeSelf)
            anvilMessage.SetActive(true);
        else if (distanceFromPlayer > 3.0f && anvilMessage.activeSelf)
            anvilMessage.SetActive(false);
        if(Input.GetKey(KeyCode.F) && anvilMessage.activeSelf && !isBuffMenuOpened)
        {
            m_SoundManager.PlayAnvilSound();
            isBuffMenuOpened = true;
            BuffMenu.SetActive(true);
        }
        if(!BuffMenu.activeSelf && isBuffMenuOpened)
        {
            isBuffMenuOpened = false;
        }
    }
}
