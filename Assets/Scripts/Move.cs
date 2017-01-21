using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    private bool jumping = false;
	void Update ()
    {
        Vector3 moveDir = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        gameObject.GetComponent<Rigidbody>().velocity = moveDir * 3 + new Vector3(0f, GetComponent<Rigidbody>().velocity.y, 0f);
        Debug.Log(moveDir.magnitude);
        if (moveDir.magnitude > .05f)
        {
            gameObject.transform.rotation = Quaternion.LookRotation(moveDir, Vector3.up);
            if (!jumping)
            {
                StartCoroutine("jump");
            }
        }
    }

    IEnumerator jump()
    {
        jumping = true;
        gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 2, ForceMode.Impulse);
        yield return new WaitForSeconds(.5f);
        jumping = false;
    }
}
