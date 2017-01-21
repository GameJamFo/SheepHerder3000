using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToScareSheep : MonoBehaviour {

    private SphereCollider scareArea;

	// Use this for initialization
	void Start () {
        scareArea = gameObject.AddComponent<SphereCollider>();
        scareArea.radius = 5f;
        scareArea.isTrigger = true;
    }
	
	// Update is called once per frame
	void Update () {	
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.transform.parent == null && other.gameObject.transform.tag == "sheep")
        {
            other.gameObject.GetComponent<BoidFlocking3D>().scareSheep(transform.position);
        }
    }

}
