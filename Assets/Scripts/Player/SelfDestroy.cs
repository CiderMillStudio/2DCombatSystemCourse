using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    [SerializeField] bool isShadowOfPickupItem = false;
    ParticleSystem ps;

    float shadowTimer = 0;
    float shadowLifetime = 1f;
    void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    private void Start() 
    {
        if (isShadowOfPickupItem) //solved a bug: assigns a lifetime to the spawned shadow instantiated from the PickupItemAnimCurveFling.cs class. This way, if the Pickup item is destroyed (obtained by player) before it reaches the ground, the shadow won't be stuck in existance, it will auto-self-destroy after the timer here runs out.
        {
            shadowTimer = 0;
            shadowLifetime = GetComponentInParent<PickupItemAnimCurveFling>().GetPickupDuration();
        }

    }
    private void Update() 
    {
        if (ps && !ps.IsAlive())
        {
            DestroySelfAnimEvent();
        }

        
        if (isShadowOfPickupItem)
        {
            PickupShadowDestroyTimer();
        } 
        
    }

    public void DestroySelfAnimEvent()
    {
        Destroy(gameObject);
    }

    void PickupShadowDestroyTimer() 
    {
        shadowTimer += Time.deltaTime;
            if (shadowTimer >= shadowLifetime)
            {
                Destroy(gameObject);
            }
    }




}

