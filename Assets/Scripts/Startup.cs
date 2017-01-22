using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startup : MonoBehaviour {

    private Stats stats = null;

    private void Awake()
    {
        Application.runInBackground = true;
        stats = gameObject.GetComponent<Stats>();
        setup();
    }

    private void OnLevelWasLoaded(int level)
    {
        setup();
    }

    private void setup()
    {
        stats.setCanvas(GameObject.Find("Canvas"));
        stats.setWinScreen(GameObject.Find("Canvas/WinScreen"));

        stats.getWinScreen().SetActive(false);
    }
}
