using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour {

    private List<GameObject> allSheep = new List<GameObject>();

    public List<GameObject> getAllSheep()
    {
        return allSheep;
    }

    public void addSheep(GameObject sheep)
    {
        if (!allSheep.Contains(sheep))
        {
            allSheep.Add(sheep);
        }
        return;
    }

    public void removeSheep(GameObject sheep)
    {
        if (!allSheep.Contains(sheep))
        {
            allSheep.Remove(sheep);
        }
        return;
    }
}
