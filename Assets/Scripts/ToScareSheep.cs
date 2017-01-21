using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToScareSheep : MonoBehaviour {

    public Collider scareArea;

	// Use this for initialization
	void Start () {
		
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
