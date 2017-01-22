using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSheep : MonoBehaviour {

    bool holdsSheep = false;
    public string pickupButton;
    public GameObject leftArm;
    public GameObject rightArm;
    private GameObject sheepPickedUp;
    public Transform sheepDropOfLocation;
    public GameObject fakeSheep;

    void Update()
    {
        if (holdsSheep == true && Input.GetButtonUp(pickupButton)) // Should also check for walls and stuff... It doesn't do that.
        {
            sheepPickedUp.SetActive(true);
            // position
            sheepPickedUp.transform.position = sheepDropOfLocation.position;
            sheepPickedUp.transform.eulerAngles = new Vector3(0, sheepDropOfLocation.eulerAngles.y, 0);
            fakeSheep.SetActive(false);

            leftArm.transform.localEulerAngles = Vector3.zero;
            rightArm.transform.localEulerAngles = Vector3.zero;

            holdsSheep = false;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.transform != other.gameObject.transform.root)
            return;
        
        if(Input.GetButtonDown(pickupButton))
        {
            if (holdsSheep == false && other.gameObject.tag == "sheep")
            {
                // Sheep should get PickedUp
                sheepPickedUp = other.gameObject;
                sheepPickedUp.SetActive(false);
                fakeSheep.SetActive(true);

                leftArm.transform.localEulerAngles = new Vector3(-80, 0, 0);
                rightArm.transform.localEulerAngles = new Vector3(-80, 0, 0);

                holdsSheep = true;
            }
        }
    }
}
