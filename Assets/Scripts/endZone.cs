using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endZone : MonoBehaviour {

    private Stats stats = null;
    private List<GameObject> sheepInsideZone = new List<GameObject>();

    void Awake ()
    {
        stats = GameObject.Find("GameController").GetComponent<Stats>();
	}

    private bool allSheepInZone()
    {
        foreach (GameObject sheep in stats.getAllSheep())
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
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "sheep" && sheepInsideZone.Contains(other.gameObject))
        {
            sheepInsideZone.Remove(other.gameObject);
        }
    }
}
