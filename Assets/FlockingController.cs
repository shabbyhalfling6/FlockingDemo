using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingController : MonoBehaviour {

    public GameObject boidObject;

    public int boidCount = 50;
	// Use this for initialization
	void Start ()
    {
        //replace this with spawner that spawns a grid of boids on the screen and add that to the array
        for (int i = 0; i < boidCount; i++)
        {
            Vector2 randVector = new Vector2(Random.Range(-9, 9), Random.Range(-5, 5));
            Quaternion randRotation = new Quaternion(0.0f, 0.0f, Random.Range(0.0f, 360.0f), 0.0f);
            Instantiate(boidObject, randVector, randRotation);
        }
	}
}
