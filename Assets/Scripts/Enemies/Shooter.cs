using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class Shooter : MonoBehaviour, IEnemy
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletMoveSpeed = 6f;
    [Tooltip("A round is a collection of projectiles (1 or more projectiles) that fire either at the same time, or in a staggering fashion if stagger is enabled.")]
    [SerializeField] private int roundsPerBurst = 3; //how many bullets will fire in a given burst of shots?
    [Tooltip("A burst is a collection of rounds, and rounds are a collection of projectiles fired simultaneously or in staggering sequence")]
    [SerializeField] private float timeBetweenBursts = 2f;

    [Tooltip("Time between rounds within a burst. If oscillating, this defaults to zero, but it cannot be set to zero in the inspector.")]
    [SerializeField] private float timeBetweenRoundsWithinABurst = 0.3f;

    [SerializeField] int projectilesPerRound;
    [SerializeField] [Range(0f,359f)] float angleSpread = 30f;
    [SerializeField] float startingDistance = 0.1f; //distance that the bullet spawns away from the enemy transform, needs to be greater than 0
    
    [Tooltip("Enable 'stagger' if you want each round to fire bullets one at a time, as if 'spraying and praying'")]
    [SerializeField] bool stagger;
    [Tooltip("Stagger has to be enabled in order for oscillate to work properly")]
    [SerializeField] bool oscillate;
    private bool isShooting = false;

    private void OnValidate() {
        if (oscillate) {stagger = true;}
        if (!oscillate) {stagger = false;}
        if (projectilesPerRound < 1) {projectilesPerRound = 1;}
        if (roundsPerBurst < 1) {roundsPerBurst = 1;}
        if (timeBetweenRoundsWithinABurst < 0.1f) {timeBetweenRoundsWithinABurst = 0.1f;}
        if (timeBetweenBursts < 0.1f) {timeBetweenRoundsWithinABurst = 0.1f;}
        if (startingDistance < 0.1f) {startingDistance = 0.1f;}
        if (angleSpread == 0 ) {projectilesPerRound = 1;}
        if (bulletMoveSpeed <= 0) {bulletMoveSpeed = 0.1f;}
    }
    
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
        float startAngle, currentAngle, angleStep, endAngle;

        float timeBetweenProjectiles = 0f;
        TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);
        
        
        if (stagger) {timeBetweenProjectiles = timeBetweenRoundsWithinABurst / projectilesPerRound;}


        int roundsShot = 0;
        while (roundsShot <= roundsPerBurst - 1)
        {
            if (!oscillate) 
            {
                TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);
            }
            
            if (oscillate && roundsShot % 2 != 1)
            {
                TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);
            }
            else if (oscillate)
            {
                currentAngle = endAngle;
                endAngle = startAngle;
                startAngle = currentAngle;
                angleStep *= -1;
                
            }

            
            for (int j = 0; j < projectilesPerRound; j++)
            {

                
                Vector2 pos = FindBulletSpawnPos(currentAngle);

                GameObject newBullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
                newBullet.transform.right = newBullet.transform.position - transform.position;

                if (newBullet.TryGetComponent(out Projectile projectile)) //"out" allows us to call a function, pass a parameter, modify that parameter, and access it outside of the function. It also allows us to return multiple data types.
                {
                    projectile.UpdateProjectileSpeed(bulletMoveSpeed);
                }

                currentAngle += angleStep;
                
                if (stagger)
                {
                    yield return new WaitForSeconds(timeBetweenProjectiles);
                }

            }

            roundsShot++;
            currentAngle = startAngle;


            if (!stagger) {yield return new WaitForSeconds(timeBetweenRoundsWithinABurst);}

        }

        yield return new WaitForSeconds(timeBetweenBursts);
        
        isShooting = false;

    }

    private void TargetConeOfInfluence(out float startAngle, out float currentAngle, out float angleStep, out float endAngle)
    {
        Vector2 targetDirection = PlayerController.Instance.transform.position - transform.position;
        float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        startAngle = targetAngle;
        endAngle = targetAngle;
        currentAngle = targetAngle;
        float halfAngleSpread = 0;
        angleStep = 0;
        if (angleSpread != 0)
        {
            angleStep = angleSpread / (projectilesPerRound - 1f); // gives us the SPACE between bullets, in terms of degrees.
            halfAngleSpread = angleSpread / 2;
            startAngle = targetAngle - halfAngleSpread;
            endAngle = targetAngle + halfAngleSpread;
            currentAngle = startAngle; //allows the FIRST bullet to start at the START angle

        }
    }


    private Vector2 FindBulletSpawnPos(float currentAngle)
    {
        float x = transform.position.x + startingDistance * Mathf.Cos(currentAngle * Mathf.Deg2Rad);
        float y = transform.position.y + startingDistance * Mathf.Sin(currentAngle * Mathf.Deg2Rad);
        Vector2 pos = new Vector2 (x,y);

        return pos;
    }
}
