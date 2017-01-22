using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour {

	void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}

    public void winGame()
    {
        currentState = gameState.won;
        Debug.Log("Game has been won!");
    }

    public void loseGame()
    {
        currentState = gameState.lost;
        Debug.Log("Games has been lost!");
    }

    public gameState getState()
    {
        return currentState;
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
