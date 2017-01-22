using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour {

    private List<GameObject> allSheep = new List<GameObject>();
    private GameObject canvas = null;
    private GameObject winScreen = null;
    private GameObject loseScreen = null;

    private void OnLevelWasLoaded(int level)
    {
        allSheep = new List<GameObject>();
        canvas = null;
        winScreen = null;
        loseScreen = null;
    }

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

    public GameObject getCanvas()
    {
        return canvas;
    }

    public void setCanvas(GameObject input)
    {
        canvas = input;
    }

    public GameObject getWinScreen()
    {
        return winScreen;
    }

    public void setWinScreen(GameObject input)
    {
        winScreen = input;
    }

    public GameObject getLoseScreen()
    {
        return loseScreen;
    }

    public void setLoseScreen(GameObject input)
    {
        loseScreen = input;
    }
}
