using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour, IEnemy
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletMoveSpeed = 6f;
    [SerializeField] private int burstCount = 3; //how many bullets will fire in a given burst of shots?
    [SerializeField] private float timeBetweenBursts = 2f;

    [SerializeField] private float timeBetweenBullets = 0.3f;

    [SerializeField] int projectilesPerBurst;
    [SerializeField] [Range(0f,359f)] float angleSpread = 30f;
    [SerializeField] float startingDistance = 0.1f; //distance that the bullet spawns away from the enemy transform, needs to be greater than 0
    private bool isShooting = false;
    
    public void Attack()
    {
        if (!isShooting)
        {
            StartCoroutine(ShootRoutine());
        }
    }


    IEnumerator ShootRoutine()
    {
        
        isShooting = true;
        Vector2 targetDirection = PlayerController.Instance.transform.position - transform.position;
        float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        float startAngle = targetAngle;
        float endAngle = targetAngle;
        float currentAngle = targetAngle;
        float halfAngleSpread = 0;
        float angleStep = 0;

        if (angleSpread != 0)
        {
            angleStep = angleSpread / (projectilesPerBurst - 1f); // gives us the SPACE between bullets, in terms of degrees.
            halfAngleSpread = angleSpread / 2;
            startAngle = targetAngle - halfAngleSpread;
            endAngle = targetAngle + halfAngleSpread;
            currentAngle = startAngle; //allows the FIRST bullet to start at the START angle

        }

        int burstsShot = 0;
        while (burstsShot <= burstCount - 1)
        {
            
            for (int j = 0; j < projectilesPerBurst; j++)
            {
                
                Vector2 pos = FindBulletSpawnPos(currentAngle);
                
                GameObject newBullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
                newBullet.transform.right = newBullet.transform.position - transform.position;
                Debug.Log("newBullet.transform.position"+ newBullet.transform.position);
                Debug.Log("transform.position" + transform.position);
                Debug.Log(newBullet.transform.right);

                if (newBullet.TryGetComponent(out Projectile projectile)) //"out" allows us to call a function, pass a parameter, modify that parameter, and access it outside of the function. It also allows us to return multiple data types.
                {
                    projectile.UpdateProjectileSpeed(bulletMoveSpeed);
                }

                currentAngle += angleStep;

            }

            burstsShot++;
            currentAngle = startAngle;
            

            yield return new WaitForSeconds(timeBetweenBullets);

        }

        yield return new WaitForSeconds(timeBetweenBursts);
        isShooting = false;

    }

    // void UpdateAngleValues()
    // {
    //     targetDirection = PlayerController.Instance.transform.position - transform.position;
    //     targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
    //     startAngle = targetAngle;
    //     endAngle = targetAngle;
    //     currentAngle = targetAngle;
    //     halfAngleSpread = angleSpread / 2;
    //     angleStep = angleSpread / projectilesPerBurst - 1f;
    // }

    private Vector2 FindBulletSpawnPos(float currentAngle)
    {
        float x = transform.position.x + startingDistance * Mathf.Cos(currentAngle * Mathf.Deg2Rad);
        float y = transform.position.y + startingDistance * Mathf.Sin(currentAngle * Mathf.Deg2Rad);
        Vector2 pos = new Vector2 (x,y);

        return pos;
    }
}
