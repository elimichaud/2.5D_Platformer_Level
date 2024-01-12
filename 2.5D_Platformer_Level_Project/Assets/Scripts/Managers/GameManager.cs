using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Rings collected
    [SerializeField] private bool blueringCollected;
    [SerializeField] private bool greenringCollected;
    [SerializeField] private bool redringCollected ;
    
    //Door
    [SerializeField] private GameObject door;
    private bool m_DoorDestroyed;
    public bool destroyingDoor;
    
    //Spells
    public static float m_SpellDamage;
    [SerializeField] private GameObject SpellHitVFX;
    
    //Cameras
    [SerializeField] private GameObject m_MainCamera;
    [SerializeField] private GameObject m_DoorCamera;
    

    private void Start()
    {
        m_SpellDamage = 5;
        blueringCollected = false;
        greenringCollected = false;
        redringCollected = false;
        m_DoorDestroyed = false;
        destroyingDoor = false;
    }

    private void Update()
    {
        //Rings
        if (redringCollected && greenringCollected && blueringCollected && !m_DoorDestroyed)
        {
            m_DoorDestroyed = true;
            StartCoroutine(UnlockDoor());
        }
    }
    
    public void CollectBlueRing()
    {
        blueringCollected = true;
    }
    
    public void CollectGreenRing()
    {
        greenringCollected = true;
    }
    
    public void CollectRedRing()
    {
        redringCollected = true;
    }

    IEnumerator UnlockDoor()
    {
        destroyingDoor = true;
        m_MainCamera.SetActive(false);
        m_DoorCamera.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        Destroy(door);
        yield return new WaitForSeconds(1.0f);
        m_DoorCamera.SetActive(false);
        m_MainCamera.SetActive(true);
        destroyingDoor = false;
    }

    public void SpellHit(Vector3 position)
    {
        GameObject hitObject = Instantiate(SpellHitVFX, position, new Quaternion(0, 0, 0, 1));
        hitObject.GetComponent<ParticleSystem>().Play();
    }

    public float GetSpellDamage()
    {
        return m_SpellDamage;
    }
    
    public static void SetSpellDamage(float damage)
    {
        m_SpellDamage = damage;
    }

}
