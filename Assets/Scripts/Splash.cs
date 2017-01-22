using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour {

	void Start ()
    {
        StartCoroutine(splashTimer());

        GameObject gameController = new GameObject("GameController");
        gameController.AddComponent<Startup>();
        gameController.AddComponent<Stats>();
        gameController.AddComponent<State>();

        DontDestroyOnLoad(gameController);
	}
	
	IEnumerator splashTimer()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(1);
    }
}
