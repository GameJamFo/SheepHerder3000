using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endZone : MonoBehaviour {

    private List<GameObject> allSheep = new List<GameObject>();
    private List<GameObject> sheepInsideZone = new List<GameObject>();

    void Start ()
    {
        foreach(GameObject sheep in GameObject.FindGameObjectsWithTag("sheep"))
        {
            allSheep.Add(sheep);
        }
	}

    private bool allSheepInZone()
    {
        foreach (GameObject sheep in allSheep)
        {
            if (!sheepInsideZone.Contains(sheep))
            {
                return false;
            }
        }
        return true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "sheep" && !sheepInsideZone.Contains(other.gameObject))
        {
            sheepInsideZone.Add(other.gameObject);
            Debug.Log("Sheep entered!");
            Debug.Log(allSheepInZone());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "sheep" && sheepInsideZone.Contains(other.gameObject))
        {
            sheepInsideZone.Remove(other.gameObject);
            Debug.Log("Sheep left!");
        }
    }
}
