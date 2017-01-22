using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightBlink : MonoBehaviour {
    new private Light light;
    private float intensity;
    public float transitionTime = 5f;
    private float currentTransitionTime = 0f;
    private bool hasBeenOff = false;

	// Use this for initialization
	void Start () {
        light = GetComponent<Light>();
        intensity = light.intensity;
	}
	
	// Update is called once per frame
	void Update () {
        currentTransitionTime += Time.deltaTime;

		if(hasBeenOff)
        {
            light.intensity = (currentTransitionTime / transitionTime) * intensity;
        }
        else
        {
            light.intensity = (1-(currentTransitionTime / transitionTime)) * intensity;
        }

        if(currentTransitionTime >= transitionTime)
        {
            currentTransitionTime -= transitionTime;
            hasBeenOff = !hasBeenOff;
        }
	}
}
