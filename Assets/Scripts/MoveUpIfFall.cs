using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpIfFall : MonoBehaviour {

    public float upAmount = 10;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform != other.gameObject.transform.root)
            return;

        if(other.gameObject.tag == "sheep")
        {
            other.gameObject.transform.Translate(Vector3.up*upAmount);
        }
    }
}
