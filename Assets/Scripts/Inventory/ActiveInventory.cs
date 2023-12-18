using System.Collections;
using System.Collections.Generic;
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

        ChangeActiveWeapon(); //just added this!!
    }

    void ChangeActiveWeapon() {
        Debug.Log(transform.GetChild(activeSlotIndexNum).GetComponent<InventorySlot>().GetWeaponInfo().weaponPrefab.name); //Since GetWeaponInfo is a public method that returns the weaponInfo scriptable object you can get the weaponPrefab name (string name of the weapon prefab)
    }
}
