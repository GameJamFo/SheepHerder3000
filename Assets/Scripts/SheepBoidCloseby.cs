using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepBoidCloseby : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.root != gameObject.transform.root && other.transform.root.tag == "sheep" && other.name == "follower")
        {
            Debug.Log("!same");
        }
    }
}
