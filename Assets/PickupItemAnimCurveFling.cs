using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItemAnimCurveFling : MonoBehaviour
{
    [SerializeField] float duration = 1f;
    [SerializeField] AnimationCurve animCurve;
    [SerializeField] float heightY = 3f;
    [SerializeField] GameObject shadowOfProjectile;


    private void Start() {
        float randX = Random.Range(-2f, 2f);
        float randY = Random.Range(-1.5f, 1.5f);
        Vector2 randomEndPosition = new (transform.position.x + randX, transform.position.y + randY);
        StartCoroutine(ProjectileCurveRoutine(transform.position, randomEndPosition));
    }



    private IEnumerator ProjectileCurveRoutine(Vector3 startPosition, Vector3 endPosition)
    {
        float timePassed = 0f;
        GameObject shadowInstance = Instantiate(shadowOfProjectile, transform.position, Quaternion.identity, transform);


        while (timePassed < duration)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed/duration;
            float heightT = animCurve.Evaluate(linearT); //essentially Height over time
            float height = Mathf.Lerp(0f, heightY, heightT);

            transform.position = 
            Vector2.Lerp(startPosition, endPosition, linearT) + 
            new Vector2(0f, height); 

            shadowInstance.transform.position = 
            Vector2.Lerp(startPosition, endPosition, linearT);

            yield return null;
        }
    
    }

    public float GetPickupDuration()
    {
        return duration;
    }

}
