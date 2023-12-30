using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ActiveInventory : Singleton<ActiveInventory>
{
    private int activeSlotIndexNum = 0; //keeps track of which inventory slot we're active in

    private PlayerControls playerControls;

    protected override void Awake() {
        base.Awake();
        playerControls = new PlayerControls(); //activate player controls!
    }

    private void Start() {
        playerControls.Inventory.KeyboardKeys.performed += ctx => ToggleActiveSlot((int)ctx.ReadValue<float>());
    }


    public void ActivateInventoryToItemZero()
    {
         
        ToggleActiveHighlight(0); //at start of game, player has sword by default 
    }


    private void OnEnable() {
        playerControls.Enable(); //Don't forget to enable player controls!
    }

    // private void OnDisable()
    // {
    //     playerControls.Disable();
    // }

    void ToggleActiveSlot(int numValue) {
        ToggleActiveHighlight(numValue - 1);
    }

    void ToggleActiveHighlight(int indexNum) {
        if (PlayerController.Instance.PlayerIsAlive)
        {
            activeSlotIndexNum = indexNum;

            foreach (Transform inventorySlot in this.transform) {
                inventorySlot.GetChild(0).gameObject.SetActive(false);
            }

            this.transform.GetChild(activeSlotIndexNum).GetChild(0).gameObject.SetActive(true);

            ChangeActiveWeapon();
        }
    }

    void ChangeActiveWeapon() {
        if (PlayerController.Instance.PlayerIsAlive)
        {
            if (ActiveWeapon.Instance.CurrentActiveWeapon != null)
            {
                Destroy(ActiveWeapon.Instance.CurrentActiveWeapon.gameObject);
            }
            
            Transform childTransform = transform.GetChild(activeSlotIndexNum);
            InventorySlot inventorySlot = childTransform.GetComponentInChildren<InventorySlot>();
            WeaponInfo weaponInfo = inventorySlot.GetWeaponInfo();

            if (weaponInfo == null)
            {
                ActiveWeapon.Instance.WeaponNull();
                return;
            }

            GameObject weaponToSpawn = weaponInfo.weaponPrefab;
            


            GameObject newWeapon = Instantiate(weaponToSpawn, ActiveWeapon.Instance.transform.position, Quaternion.identity); //FindObjectOfType<PlayerController>().GetComponentInChildren<ActiveWeapon>().gameObject.transform);
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0,0,0); //it's important to reset the Parent's Euler rotation before assigning it to its new child.

            newWeapon.transform.parent = ActiveWeapon.Instance.transform;

            ActiveWeapon.Instance.NewWeapon(newWeapon.GetComponent<MonoBehaviour>());
        }
    }

    
}
