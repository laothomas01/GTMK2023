using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class ParticleController3D : MonoBehaviour
{
    private float lifeSpan = 0;
    public float lifeSpanThreshold;
    public float moveSpeed;
    float explosionForce = 50f;
    float explosionRadius = 5f;
    float upwardsModifier = 0.5f;
    float distance;
    float normalizedDistance;
    float force;
    Vector3 randomPosition;
    void Start()
    {

        randomPosition = Random.insideUnitSphere * explosionRadius;
        this.transform.position = transform.position + randomPosition;
        this.transform.rotation = Quaternion.identity;
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeSpan > lifeSpanThreshold)
        {
            this.gameObject.SetActive(false);

        }
        else
        {
            lifeSpan += Time.deltaTime;
        }

        if (this.tag == "Explosion")
        {
            explosiveMovement();
        }

    }
    public void explosiveMovement()
    {


        // // Calculate the force to apply based on the distance from the explosion center
        distance = randomPosition.magnitude;
        normalizedDistance = 1f - Mathf.Clamp01(distance / explosionRadius);
        // Apply explosion force to the sphere by directly manipulating its position
        force = explosionForce * normalizedDistance;

        this.transform.position += randomPosition.normalized * force * Time.deltaTime;
        this.transform.position += Vector3.up * upwardsModifier * force * Time.deltaTime;


    }
}
