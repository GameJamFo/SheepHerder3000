using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
    public Camera mainCamera;
    public float speed = 6f;
    private bool jumping = false;

	void Update ()
    {
        Vector3 leftAndRight = new Vector3(Mathf.Cos(mainCamera.transform.eulerAngles.y * Mathf.Deg2Rad), 0, -Mathf.Sin(mainCamera.transform.eulerAngles.y * Mathf.Deg2Rad));
        Vector3 upAndDown = new Vector3(Mathf.Sin(mainCamera.transform.eulerAngles.y * Mathf.Deg2Rad), 0, Mathf.Cos(mainCamera.transform.eulerAngles.y * Mathf.Deg2Rad));
        Vector3 moveDir = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        //gameObject.GetComponent<Rigidbody>().velocity = moveDir * 6 + new Vector3(0f, GetComponent<Rigidbody>().velocity.y, 0f);
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0f, GetComponent<Rigidbody>().velocity.y, 0f) + leftAndRight*Input.GetAxis("Horizontal") * speed + upAndDown * Input.GetAxis("Vertical") * speed;

        if (moveDir.magnitude > .50f)
        {
            Vector3 moveDirect = gameObject.GetComponent<Rigidbody>().velocity;
            moveDirect.Set(moveDirect.x, 0, moveDirect.z);
            gameObject.transform.rotation = Quaternion.LookRotation(/*moveDir*/ moveDirect, Vector3.up);
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
