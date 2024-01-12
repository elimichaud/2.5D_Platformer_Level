using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GatesManager : MonoBehaviour
{
    [SerializeField] private GameObject gateStart;
    [SerializeField] private GameObject gateEnd;
    private string slimeTag = "Slime";
    private string turtleTag = "pic-pic";


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (CountEnnemies() == 0)
        {
            OpenGates();
        }
    }

    public void CloseStartGate()
    {
        gateStart.SetActive(true);
    }

    public void OpenGates()
    {
        gateStart.SetActive(false);
        gateEnd.SetActive(false);
    }

    private int CountEnnemies()
    {
        int childCount = 0;
        Transform child;
        for(int i = 0; i < transform.childCount; i++)
        {
            child = transform.GetChild(i);
            if (child.tag == slimeTag || child.tag == turtleTag)
            {
                childCount++;
            }
        }
        return childCount;
    }
}
