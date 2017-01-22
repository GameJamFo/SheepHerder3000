using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToScareSheep : MonoBehaviour {

    private SphereCollider scareArea;

	void Start () {
        scareArea = gameObject.AddComponent<SphereCollider>();
        scareArea.radius = 10f;
        scareArea.isTrigger = true;
    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.transform.tag == "sheep")
        {
            other.gameObject.GetComponent<BoidFlocking3D>().scareSheep(transform.position);
        }
    }
}
