using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : Singleton<ActiveWeapon>
{
    public MonoBehaviour CurrentActiveWeapon { get; private set; } //we can drop the sword gameobject into this field in unity
    PlayerControls playerControls;


    bool attackButtonDown, isAttacking = false;

    private float timeBetweenAttacks;

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
        AttackCooldown();
        timeBetweenAttacks = (CurrentActiveWeapon as IWeapon).GetWeaponInfo().weaponCooldown;
    }

    public void WeaponNull()
    {
        CurrentActiveWeapon = null;
    }

    private void AttackCooldown()
    {
        isAttacking = true; 
        StopAllCoroutines();
        StartCoroutine(TimeBetweenAttacksRoutine());
    }

    private IEnumerator TimeBetweenAttacksRoutine()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);
        isAttacking = false;
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
        AttackCooldown();
        (CurrentActiveWeapon as IWeapon).Attack();
        
        }

    }
}
