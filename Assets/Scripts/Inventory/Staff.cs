using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour, IWeapon //requires the public Attack method
{
    public void Attack() //without this "public attack" function, IWeapon cannot be implemented!!! (Try commenting this out and watch the IWeapon implementation return an error!)
    {
        Debug.Log("Staff attack!");
        ActiveWeapon.Instance.ToggleIAttacking(false);
    }
}
