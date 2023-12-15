using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    Rigidbody2D rb;

    
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    public void GetKnockedBack(Transform damageSource, float knockBackThrust)
    {
        Vector2 difference = (transform.position - damageSource.position).normalized * knockBackThrust * rb.mass;
        //What is .normalize? By multiplying the difference by the mass of the rb, wouldn't the distance become greater?
        rb.AddForce(difference, ForceMode2D.Impulse); //Impulse is like a punch in a specific direction
       

    }
}
