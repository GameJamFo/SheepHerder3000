using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endZone : MonoBehaviour {

    private Stats stats = null;
    private List<GameObject> sheepInsideZone = new List<GameObject>();
    private State state;

    void Awake ()
    {
        state = GameObject.Find("GameController").GetComponent<State>();
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

            if (allSheepInZone())
            {
                state.switchState(State.gameState.won);
            }
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
