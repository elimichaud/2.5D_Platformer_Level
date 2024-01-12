using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    private List<GameObject> weaponArsenal = new List<GameObject>();
    private int equippedWeaponIndex = 0;
    private GameObject wpnInstance;
    private GameObject staffWeapon = null;
    public bool isStaffEquipped = false;

    [System.NonSerialized]
    public Transform StaffTransform;

    [SerializeField]
    GameObject hand;

    [SerializeField]
    GameObject startingWeapon;
    
    [SerializeField]
    Vector3 position;

    [SerializeField]
    Vector3 longWeaponPosition;

    [SerializeField]
    Quaternion rotation;

    void Start() {
        weaponArsenal.Add(startingWeapon);

        equipWeapon(equippedWeaponIndex);
    }

    void Update() {
        var previousWnpInput = Input.GetKeyDown(KeyCode.Q);
        var nextWpnInput = Input.GetKeyDown(KeyCode.E);

        if(!wpnInstance.GetComponent<MainWeapon>().isAttacking) {
            if(nextWpnInput) {
                equippedWeaponIndex++;

                if(equippedWeaponIndex >= weaponArsenal.Count) {
                    equippedWeaponIndex = 0;
                }

                equipWeapon(equippedWeaponIndex);
            } else if(previousWnpInput) {
                equippedWeaponIndex--;

                if(equippedWeaponIndex < 0) {
                    equippedWeaponIndex = weaponArsenal.Count - 1;
                }

                equipWeapon(equippedWeaponIndex);
            }
        }
        
        if(wpnInstance.GetComponent<MainWeapon>().name == "Staff")
        {
            StaffTransform = wpnInstance.GetComponent<Transform>();
        }
    }
    
    private void equipWeapon (int index) {  
        if(wpnInstance) {
            Destroy(wpnInstance);
        }

        wpnInstance = Instantiate(weaponArsenal[index]);
        wpnInstance.transform.parent = hand.transform;
        wpnInstance.transform.localPosition = (wpnInstance.GetComponent<MainWeapon>() is LanceWeapon) ? longWeaponPosition : position;
        wpnInstance.transform.localRotation = rotation;
    }

    public void addNewItemToArsenal(GameObject newWeapon) {
        if(newWeapon.tag == "Staff") {
            staffWeapon = newWeapon;
        } else {
            weaponArsenal.Add(newWeapon);
        }
    }

    public MainWeapon getCurrentWeapon() {
        return wpnInstance.GetComponent<MainWeapon>();
    }

    public void changeToStaff() {
        if(wpnInstance) {
            Destroy(wpnInstance);
        }

        wpnInstance = Instantiate(staffWeapon);
        wpnInstance.transform.parent = hand.transform;
        wpnInstance.transform.localPosition = position;
        wpnInstance.transform.localRotation = rotation;

        isStaffEquipped = true;
    }

    public void changeToEquippedWpn() {
        equipWeapon(equippedWeaponIndex);
    }

    public bool isStaffInArsenal() {
        return staffWeapon != null;
    }
}
