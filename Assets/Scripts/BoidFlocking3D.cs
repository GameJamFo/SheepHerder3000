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
    public float nearbyCenterMultiple = 1f;
    public float nearbyVelocityMultiple = 1f;

    // Kristian's enum-----------
    private enum mood {
        SCARED,
        CALM
    };
    private mood currentMood = mood.CALM;
    // --------------------------

    private float timeLastScare = 0f;
    private float sheepScared = 3f;
    private Vector3 fleeDirection;
    private Vector3 evadeDirection;
    private bool jumping = false;

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

        Vector3 startVector = GetComponent<Rigidbody>().velocity.normalized;
        if (GetComponent<Rigidbody>().velocity.magnitude > .5f)
        {
            //gameObject.transform.rotation = Quaternion.LookRotation(Vector3.Lerp(-startVector, -new Vector3(GetComponent<Rigidbody>().velocity.normalized.x, 0f, GetComponent<Rigidbody>().velocity.normalized.z), .1f), Vector3.up); // Face movedirection
            gameObject.transform.rotation = Quaternion.LookRotation(-new Vector3(GetComponent<Rigidbody>().velocity.normalized.x, 0f, GetComponent<Rigidbody>().velocity.normalized.z), Vector3.up); // Face movedirection
        }
        //else
        //{
        //    gameObject.transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up); 
        //}

        RaycastHit hit;
        if (Physics.SphereCast(transform.position, 2, Vector3.forward, out hit, 10,LayerMask.NameToLayer("Fences")))
        {
            evadeDirection = hit.point - transform.position;
            evadeDirection.Set(evadeDirection.x, 0, evadeDirection.z);
            evadeDirection.Normalize();
            print("Aaaah, here's a fence!! at: " + hit.point);
        }
        else evadeDirection = Vector3.zero;

        if(!jumping)
        {
            //StartCoroutine(jump(1f));
        }
    }

    IEnumerator BoidSteering()
    {
        while (true)
        {
            float waitTime = Random.Range(0.3f, 0.5f);
            if (inited)
            {
                if(currentMood == mood.CALM)
                { 
                    GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity + FlockCalc() * Time.deltaTime;
                } else
                if (currentMood == mood.SCARED)
                { 
                    GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity + RunAwayCalc();
                }


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
            }

            yield return new WaitForSeconds(waitTime);
        }
    }

    IEnumerator jump(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Debug.Log("Waited " + waitTime + " seconds!");
    }

    private Vector3 FlockCalc()
    {
        Vector3 randomize = new Vector3((Random.value * 2) - 1, 0f, (Random.value * 2) - 1);

        randomize.Normalize();
        BoidController3D boidController = Controller.GetComponent<BoidController3D>();
        //Vector3 flockCenter = new Vector3(boidController.flockCenter.x, transform.position.y, boidController.flockCenter.z);
        Vector3 flockVelocity = boidController.flockVelocity;
        Vector3 follow = chasee.transform.localPosition;

        Vector3 flockCenter = -gameObject.GetComponentInChildren<SheepBoidCloseby>().getNearbyCenter() * nearbyCenterMultiple;
        flockVelocity = gameObject.GetComponentInChildren<SheepBoidCloseby>().getNearbyVelocity() * nearbyVelocityMultiple;
        follow = follow - transform.localPosition;
        follow = Vector3.zero;

        return (flockCenter + flockVelocity + follow * 2 + randomize * randomness + evadeDirection);
    }

    private Vector3 RunAwayCalc()
    {
        return fleeDirection + Vector3.up;
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
        fleeDirection = new Vector3(-positionScareFrom.x + transform.position.x, 0, -positionScareFrom.z + transform.position.z);
        fleeDirection.Normalize();

        if (currentMood == mood.CALM)
            GetComponent<Rigidbody>().AddForce(fleeDirection*10f + Vector3.up*5f, ForceMode.Impulse);
        currentMood = mood.SCARED;
        sheepScared = time;
        timeLastScare = 0f;

        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "ground")
        {
            jumping = false;
        }
    }
}