using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour {

    public GameObject menuFrame;
    public GameObject creditsFrame;
    private bool creditsShowing = false;

	// Use this for initialization
	void Start () {
        creditsFrame.SetActive(false);
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if(creditsShowing && Input.GetMouseButtonDown(0))
        {
            menuFrame.SetActive(true);
            creditsFrame.SetActive(false);
            creditsShowing = false;
        }
	}

    public void startGame()
    {
        print("hey123");
    }

    public void credits()
    {
        menuFrame.SetActive(false);
        creditsFrame.SetActive(true);
        creditsShowing = true;
    }
}
