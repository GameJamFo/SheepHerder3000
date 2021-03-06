﻿using UnityEngine;
using System.Collections;

public class BoidController3D : MonoBehaviour
{
    public float minVelocity = 5;
    public float maxVelocity = 20;
    public float randomness = 1;
    public float nearbyCenterMultiple = 1f;
    public float nearbyVelocityMultiple = 1f;
    public int flockSize = 20;
    public GameObject prefab;
    public GameObject chasee;

    public Vector3 flockCenter;
    public Vector3 flockVelocity;

    public float spawnArea = 5f;

    private GameObject[] boids;
    new private Collider collider;
    private Stats stats = null;

    private void Awake()
    {
        stats = GameObject.Find("GameController").GetComponent<Stats>();
    }

    void Start()
    {
        collider = GetComponent<Collider>();
        boids = new GameObject[flockSize];
        for (var i = 0; i < flockSize; i++)
        {
            Vector3 random = new Vector3(Random.Range(-spawnArea, spawnArea), 0f, Random.Range(-spawnArea, spawnArea));
            GameObject boid = Instantiate(prefab, random, transform.rotation) as GameObject;
            boid.transform.position = transform.position + random;
            boid.GetComponent<BoidFlocking3D>().SetController(gameObject);
            boid.GetComponent<BoidFlocking3D>().nearbyCenterMultiple = nearbyCenterMultiple;
            boid.GetComponent<BoidFlocking3D>().nearbyVelocityMultiple = nearbyVelocityMultiple;
            boids[i] = boid;
            stats.addSheep(boid);

            GameObject boidFollower = new GameObject("follower");

            boidFollower.transform.parent = boid.transform;
            boidFollower.transform.localPosition = Vector3.zero;
            boidFollower.AddComponent<SheepBoidCloseby>();
            SphereCollider coll = boidFollower.AddComponent<SphereCollider>();
            coll.radius = 5f;
            coll.isTrigger = true;
        }
    }

    void Update()
    {
        Vector3 theCenter = Vector3.zero;
        Vector3 theVelocity = Vector3.zero;

        foreach (GameObject boid in stats.getAllSheep())
        {
            theCenter = theCenter + boid.transform.localPosition;
            theVelocity = theVelocity + boid.GetComponent<Rigidbody>().velocity;
        }

        flockCenter = theCenter / (flockSize);
        flockVelocity = theVelocity / (flockSize);
    }
}