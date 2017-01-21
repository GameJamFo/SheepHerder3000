using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepBoidCloseby : MonoBehaviour {

    private List<GameObject> nearbySheep = new List<GameObject>();

    public Vector3 getNearbyCenter()
    {
        Vector3 center = Vector3.zero;

        foreach(GameObject neighbor in nearbySheep)
        {
            center += neighbor.transform.position;
        }

        if (nearbySheep.Count > 0)
        {
            center /= nearbySheep.Count;
        }

        return center;
    }

    public Vector3 getNearbyVelocity()
    {
        Vector3 vel = Vector3.zero;

        foreach(GameObject neighbor in nearbySheep)
        {
            vel += neighbor.GetComponent<Rigidbody>().velocity;
        }

        if(nearbySheep.Count > 0)
        {
            vel /= nearbySheep.Count;
        }

        return vel;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.root != gameObject.transform.root && other.transform.root.tag == "sheep" && other.gameObject.name == "follower")
        {
            nearbySheep.Add(other.transform.root.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (nearbySheep.Contains(other.transform.root.gameObject))
        {
            nearbySheep.Remove(other.transform.root.gameObject);
        }
    }
}
