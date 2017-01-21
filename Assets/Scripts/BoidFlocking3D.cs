using UnityEngine;
using System.Collections;

public class BoidFlocking3D : MonoBehaviour
{
    private GameObject Controller;
    private bool inited = false;
    private float minVelocity;
    private float maxVelocity;
    private float randomness;
    private GameObject chasee;

    // Kristian's enum-----------
    private enum mood {
        SCARED,
        CALM
    };
    private mood currentMood = mood.CALM;
    // --------------------------

    private float timeLastScare = 0f;
    private float sheepScared = 3f;

    void Start()
    {
        StartCoroutine("BoidSteering");
    }

    void Update()
    {
        timeLastScare += Time.deltaTime;
        if (currentMood == mood.SCARED)
        {
            if (timeLastScare > sheepScared)
                currentMood = mood.CALM;
        }
    }

    IEnumerator BoidSteering()
    {
        while (true)
        {
            if (inited && currentMood == mood.CALM)
            {
                GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity + Calc() * Time.deltaTime;

                // enforce minimum and maximum speeds for the boids
                float speed = GetComponent<Rigidbody>().velocity.magnitude;
                if (speed > maxVelocity)
                {
                    GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * maxVelocity;
                }
                else if (speed < minVelocity)
                {
                    GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * minVelocity;
                }
            }else if(inited && currentMood == mood.SCARED)
            {

            }

            float waitTime = Random.Range(0.3f, 0.5f);
            yield return new WaitForSeconds(waitTime);
        }
    }

    private Vector3 Calc()
    {
        Vector3 randomize = new Vector3((Random.value * 2) - 1, (Random.value * 2) - 1, (Random.value * 2) - 1);

        randomize.Normalize();
        BoidController3D boidController = Controller.GetComponent<BoidController3D>();
        Vector3 flockCenter = boidController.flockCenter;
        Vector3 flockVelocity = boidController.flockVelocity;
        Vector3 follow = chasee.transform.localPosition;

        flockCenter = flockCenter - transform.localPosition;
        flockVelocity = flockVelocity - GetComponent<Rigidbody>().velocity;
        follow = follow - transform.localPosition;

        return (flockCenter + flockVelocity + follow * 2 + randomize * randomness);
    }

    public void SetController(GameObject theController)
    {
        Controller = theController;
        BoidController3D boidController = Controller.GetComponent<BoidController3D>();
        minVelocity = boidController.minVelocity;
        maxVelocity = boidController.maxVelocity;
        randomness = boidController.randomness;
        chasee = boidController.chasee;
        inited = true;
    }

    public void scareSheep(Vector3 positionScareFrom, float time=3f)
    {
        currentMood = mood.SCARED;
        sheepScared = time;
        timeLastScare = 0f;

        Vector3 fleeDirection = new Vector3(-positionScareFrom.x + transform.position.x, 0, -positionScareFrom.z + transform.position.z);
        fleeDirection.Normalize();
    }
}