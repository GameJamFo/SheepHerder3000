using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepBoidCloseby : MonoBehaviour {

    private List<GameObject> nearbySheep = new List<GameObject>();

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
