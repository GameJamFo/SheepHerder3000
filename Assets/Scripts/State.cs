using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour {

	void Start ()
    {
		
	}
	
	void Update ()
    {
		if(currentState != gameState.playing)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
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
                break;

            case gameState.playing:
                Debug.Log("Game started...");
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
