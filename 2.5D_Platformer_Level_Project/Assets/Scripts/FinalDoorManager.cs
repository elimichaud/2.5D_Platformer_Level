using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Reflection;


public class FinalDoorManager : MonoBehaviour
{
    public GameObject woodDoor;
    public GameObject frontDoorHandle;
    public GameObject backDoorHandle;
    public GameObject player;
    public GameObject lockedMessage;
    public GameObject unlockableMessage;
    private bool isDoorOpened = false;

    void Start()
    {
        lockedMessage.SetActive(false);
        unlockableMessage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDoorOpened && Vector3.Distance(gameObject.transform.position, player.transform.position) < 3.0f)
        {
            if (player.GetComponent<KeyCollect>().GetAsKey())
            {
                unlockableMessage.SetActive(true);
                if(Input.GetKeyUp(KeyCode.F))
                    OpenDoor();
            }
            else
                lockedMessage.SetActive(true);
        }
        else
        {
            lockedMessage.SetActive(false);
            unlockableMessage.SetActive(false);
        }
    }
    void OpenDoor()
    {
        woodDoor.SetActive(false);
        frontDoorHandle.SetActive(false);
        backDoorHandle.SetActive(false);
        lockedMessage.SetActive(false);
        unlockableMessage.SetActive(false);
        isDoorOpened = true;
    }
}
