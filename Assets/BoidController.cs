using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BoidController : MonoBehaviour {

    private float searchRadius = 5.0f;

    private Vector2 allignment = new Vector2(0, 0);
    private Vector2 cohesion = new Vector2(0, 0);
    private Vector2 seperation = new Vector2(0, 0);

    public float allignmentWeight = 0.0f;
    public float cohesionWeight = 10.0f;
    public float seperationWeight = 0.0f;

    public Collider2D[] nearbyBoids;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        allignment = Vector2.zero;
        cohesion = Vector2.zero;
        seperation = Vector2.zero;

        //get other boids in a radius to this one
        nearbyBoids = Physics2D.OverlapCircleAll(this.transform.position, searchRadius, 5);

        for (int i = 0; i < nearbyBoids.Length; i++)
        {
            if (nearbyBoids[i].gameObject != this.gameObject)
            {
                //allignment calculation
                allignment += nearbyBoids[i].GetComponent<Rigidbody2D>().velocity;

                //cohesion calculation
                cohesion.x += nearbyBoids[i].transform.position.x;
                cohesion.y += nearbyBoids[i].transform.position.y;

                //seperation calculation
                seperation.x += nearbyBoids[i].transform.position.x - this.transform.position.x;
                seperation.y += nearbyBoids[i].transform.position.y - this.transform.position.y;
            }
        }

        //divide to get the average and normalise
        allignment /= nearbyBoids.Length;
        allignment.Normalize();

        cohesion /= nearbyBoids.Length;
        cohesion = new Vector2(cohesion.x - this.transform.position.x, cohesion.y - this.transform.position.y);
        cohesion.Normalize();

        seperation /= nearbyBoids.Length;
        seperation *= -1;
        seperation.Normalize();

        //setting all the calculations
        rb.velocity += (allignment * allignmentWeight) + (cohesion * cohesionWeight) + (seperation * seperationWeight);
        rb.velocity.Normalize();

        rb.rotation = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
	}
}
