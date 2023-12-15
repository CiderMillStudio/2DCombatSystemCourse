using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    [SerializeField] float knockBackTime = 0.25f;
    Rigidbody2D rb;

    public bool GettingKnockedBack {get; private set;}
    
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    public void GetKnockedBack(Transform damageSource, float knockBackThrust)
    {
        GettingKnockedBack = true;
        Vector2 difference = (transform.position - damageSource.position).normalized * knockBackThrust * rb.mass;
        //What is .normalize? By multiplying the difference by the mass of the rb, wouldn't the distance become greater?
        rb.AddForce(difference, ForceMode2D.Impulse); //Impulse is like a punch in a specific direction
        StartCoroutine(DelayBeforeResetting());
    }

    IEnumerator DelayBeforeResetting()
    {
        yield return new WaitForSeconds(knockBackTime);
        GettingKnockedBack = false;
    }

}
