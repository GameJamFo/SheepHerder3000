using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class State : MonoBehaviour {

    private Stats stats = null;

	void Start ()
    {
        stats = gameObject.GetComponent<Stats>();
	}

    private bool loadingNewScene = false;
	
	void Update ()
    {
        if((currentState == gameState.won || currentState == gameState.lost) && !loadingNewScene)
        {
            loadingNewScene = true;
            StartCoroutine(endMap());
        }
	}

    private IEnumerator endMap()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(1);
    }

    public gameState getState()
    {
        return currentState;
    }

    public void switchState(gameState inputState)
    {
        currentState = inputState;
        switch(currentState)
        {
            case gameState.lost:
                Debug.Log("You lose!");
                break;

            case gameState.won:
                Debug.Log("You've won!");
                stats.getWinScreen().SetActive(true);
                break;

            case gameState.playing:
                Debug.Log("Game started...");
                stats.getWinScreen().SetActive(false);
                break;

            case gameState.paused:
                Debug.Log("Game paused...");
                break;

            default:
                break;
        }
    }

    public enum gameState
    {
        won,
        lost,
        paused,
        playing
    }

    private gameState currentState = gameState.playing;
}
