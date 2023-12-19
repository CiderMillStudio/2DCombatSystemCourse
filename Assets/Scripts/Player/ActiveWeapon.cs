using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : Singleton<ActiveWeapon>
{
    public MonoBehaviour CurrentActiveWeapon { get; private set; } //we can drop the sword gameobject into this field in unity
    PlayerControls playerControls;

    bool attackButtonDown, isAttacking = false;

    protected override void Awake() {
        base.Awake();
        playerControls = new PlayerControls();
    }

    void OnEnable()
    {
        playerControls.Enable();
    }

    void Start()
    {
        playerControls.Combat.Attack.started += _ => StartAttacking();
        playerControls.Combat.Attack.canceled += _ => StopAttacking();
    }

    private void Update() {
        Attack();
    }

    public void NewWeapon(MonoBehaviour newWeapon) {
        CurrentActiveWeapon = newWeapon;
    }

    public void WeaponNull()
    {
        CurrentActiveWeapon = null;
    }

    public void SetIsAttackingFalse()
    {
        isAttacking = false;
    }

    public void ToggleIAttacking(bool value) {
        isAttacking = value;
    }
    void StartAttacking()
    {
        attackButtonDown = true;
    }

    void StopAttacking()
    {
        attackButtonDown = false;
    }

    void Attack() {
        
        if (attackButtonDown && !isAttacking) {
        
        isAttacking = true;
        (CurrentActiveWeapon as IWeapon).Attack();
        
        }

    }
}
