using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ActiveInventory : MonoBehaviour
{
    private int activeSlotIndexNum = 0; //keeps track of which inventory slot we're active in

    private PlayerControls playerControls;

    private void Awake() {
        playerControls = new PlayerControls(); //activate player controls!
    }

    private void Start() {
        playerControls.Inventory.KeyboardKeys.performed += ctx => ToggleActiveSlot((int)ctx.ReadValue<float>()); 

        ToggleActiveHighlight(0); //at start of game, player has sword by default 
    }


    private void OnEnable() {
        playerControls.Enable(); //Don't forget to enable player controls!

    }

    void ToggleActiveSlot(int numValue) {
        ToggleActiveHighlight(numValue - 1);
    }

    void ToggleActiveHighlight(int indexNum) {
        activeSlotIndexNum = indexNum;

        foreach (Transform inventorySlot in this.transform) {
            inventorySlot.GetChild(0).gameObject.SetActive(false);
        }

        this.transform.GetChild(activeSlotIndexNum).GetChild(0).gameObject.SetActive(true);

        ChangeActiveWeapon();
    }

    void ChangeActiveWeapon() {

        if (ActiveWeapon.Instance.CurrentActiveWeapon != null)
        {
            Destroy(ActiveWeapon.Instance.CurrentActiveWeapon.gameObject);
        }

        if (!transform.GetChild(activeSlotIndexNum).GetComponentInChildren<InventorySlot>())
        {
            ActiveWeapon.Instance.WeaponNull();
            return;
        }
        
        GameObject weaponToSpawn = transform.GetChild(activeSlotIndexNum).
        GetComponentInChildren<InventorySlot>().GetWeaponInfo().weaponPrefab;

        GameObject newWeapon = Instantiate(weaponToSpawn, ActiveWeapon.Instance.transform.position, Quaternion.identity); //FindObjectOfType<PlayerController>().GetComponentInChildren<ActiveWeapon>().gameObject.transform);
        ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0,0,0); //it's important to reset the Parent's Euler rotation before assigning it to its new child.

        newWeapon.transform.parent = ActiveWeapon.Instance.transform;

        ActiveWeapon.Instance.NewWeapon(newWeapon.GetComponent<MonoBehaviour>());

    }

    
}
