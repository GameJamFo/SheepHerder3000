using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownClock : MonoBehaviour {

    private Text clockText;
    public float timeToComplete = 120f;
    private bool started = false;
    private State state;
	
    void Start()
    {
        state = GameObject.Find("GameController").GetComponent<State>();
        clockText = gameObject.GetComponent<Text>();
        drawText();
        clockText.color = Color.black;
        StartTimer();
    }

	// Update is called once per frame
	void Update () {
		if(started == true)
        {
            if (timeToComplete > 0)
            {
                timeToComplete -= Time.deltaTime;
                drawText();

                if (timeToComplete < 21 && clockText.color == Color.black)
                    clockText.color = Color.red;

            }
            else if (state.getState() == State.gameState.playing)
                timeOut();
        }
	}

    void drawText()
    {
        int minutes = 0, seconds = (int)timeToComplete;
        while(seconds >= 60)
        {
            ++minutes;
            seconds -= 60;
        }

        string result = ":";

        if (minutes.ToString().Length <= 1)
            result = "0" + minutes.ToString() + result;
        else
            result = minutes.ToString() + result;

        if (seconds.ToString().Length <= 1)
            result = result + "0" + seconds.ToString();
        else
            result = result + seconds.ToString();

        clockText.text = result;
    }

    public void StartTimer()
    {
        started = true;
    }

    public void StopTimer()
    {
        started = false;
    }

    void timeOut()
    {
        // You lose!
        // Return to menu screen...
        state.loseGame();
    }
}
