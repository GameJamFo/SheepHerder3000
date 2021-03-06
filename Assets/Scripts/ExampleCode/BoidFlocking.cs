﻿using UnityEngine;
using System.Collections;

public class BoidFlocking : MonoBehaviour
{
    private GameObject Controller;
    private bool inited = false;
    private float minVelocity;
    private float maxVelocity;
    private float randomness;
    private GameObject chasee;

    void Start()
    {
        StartCoroutine("BoidSteering");
    }

    IEnumerator BoidSteering()
    {
        while (true)
        {
            if (inited)
            {
                GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity + Calc() * Time.deltaTime;

                // enforce minimum and maximum speeds for the boids
                float speed = GetComponent<Rigidbody2D>().velocity.magnitude;
                if (speed > maxVelocity)
                {
                    GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity.normalized * maxVelocity;
                }
                else if (speed < minVelocity)
                {
                    GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity.normalized * minVelocity;
                }
            }

            float waitTime = Random.Range(0.02f, 0.04f);
            yield return new WaitForSeconds(waitTime);
        }
    }

    private Vector2 Calc()
    {
        Vector2 localPosition2D = new Vector2(transform.localPosition.x, transform.localPosition.y);
        Vector2 randomize = new Vector3((Random.value * 2) - 1, Random.value, (Random.value * 2) - 1);

        randomize.Normalize();
        BoidController boidController = Controller.GetComponent<BoidController>();
        Vector2 flockCenter = boidController.flockCenter;
        Vector2 flockVelocity = boidController.flockVelocity;
        Vector2 follow = chasee.transform.localPosition;

        flockCenter = flockCenter - localPosition2D;
        flockVelocity = flockVelocity - GetComponent<Rigidbody2D>().velocity;
        follow = follow - localPosition2D;

        return (flockCenter + flockVelocity + follow * 2 + randomize * randomness);
    }

    public void SetController(GameObject theController)
    {
        Controller = theController;
        BoidController boidController = Controller.GetComponent<BoidController>();
        minVelocity = boidController.minVelocity;
        maxVelocity = boidController.maxVelocity;
        randomness = boidController.randomness;
        chasee = boidController.chasee;
        inited = true;
    }
}