using UnityEngine;
using System.Collections;

[System.Serializable]
public class StartOffests
{
    public float height = 35;
    public float width = 0;
    public float depth = -28;
}

public class CameraFollow : MonoBehaviour
{
    public StartOffests offsets;
    private float distance;
    public float yRotationSpeed = 300;
    public float yRotationSmoothness = 5;
    public GameObject midPoint;
    private Vector3 offset;
    private GameObject needPos;

    public float zoomAmount = 10;
    public float maxZoom = 100;
    public float startZoomProsent = 100;
    public float minZoom = 20;

    private float currentHeight;
    private float currentWidth;
    private float currentDepth;

    // Use this for initialization
    void Start()
    {
        offset = new Vector3(offsets.width * startZoomProsent / 100, offsets.height * startZoomProsent / 100, offsets.depth * startZoomProsent / 100);
        distance = Mathf.Sqrt(Mathf.Pow(offsets.height, 2) + Mathf.Pow(offsets.width, 2) + Mathf.Pow(offsets.depth, 2));

        // Starting location
        transform.position = midPoint.transform.position + offset;
        transform.LookAt(midPoint.transform);

        currentHeight = offsets.height;
        currentWidth = offsets.width;
		currentDepth = offsets.depth;

		needPos = Instantiate(new GameObject("Future Cam Location"),transform.position, transform.rotation) as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Mathf.Sqrt(Mathf.Pow(offset.x, 2) + Mathf.Pow(offset.y, 2) + Mathf.Pow(offset.z, 2));
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //  Smoothly follows mid point.
        Vector3 velocity = Vector3.zero;
        needPos.transform.position = midPoint.transform.position + offset;

        
        // if middle mouse down.. rotate around and watch at point
        if (Input.GetMouseButton(2)==true)
        {
            //transform.position = needPos;

            float mouseX = Input.GetAxis("Mouse X");

            //TODO: have camera orbit around needed position, use needPos
            // Keep us at orbitDistance from target

            needPos.transform.position = midPoint.transform.position + (needPos.transform.position - midPoint.transform.position).normalized * distance;
            needPos.transform.RotateAround(midPoint.transform.position + new Vector3(0, currentHeight, 0), Vector3.up, mouseX * yRotationSpeed * Time.deltaTime);
            
            offset = needPos.transform.position - midPoint.transform.position;
            currentWidth = needPos.transform.position.x - midPoint.transform.position.x;
            currentHeight = needPos.transform.position.y - midPoint.transform.position.y;
            currentDepth = needPos.transform.position.z - midPoint.transform.position.z;

            needPos.transform.LookAt(midPoint.transform);
        }

        // Zooming in and out by scrooling mouse wheel
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            float zoom = Input.GetAxis("Mouse ScrollWheel") * 10 * zoomAmount; // 10%

            float currentZoom = offset.y * 100 / offsets.height;
            Vector3 willBeOffset = offset - (new Vector3(currentWidth, currentHeight, currentDepth) * zoom / 100);

            if (willBeOffset.y * 100 / offsets.height >= minZoom && willBeOffset.y * 100 / offsets.height <= maxZoom) {
                offset = willBeOffset;
            }
        }

        transform.position = Vector3.SmoothDamp(transform.position, needPos.transform.position, ref velocity, 0.05f);
        if (Mathf.Abs(needPos.transform.eulerAngles.y - transform.eulerAngles.y) < 300)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, needPos.transform.rotation, (Mathf.Abs(needPos.transform.eulerAngles.y - transform.eulerAngles.y) * yRotationSmoothness * Time.deltaTime));
        else
        {
            if (transform.eulerAngles.y > 180)
                transform.rotation = Quaternion.RotateTowards(transform.rotation, needPos.transform.rotation, (Mathf.Abs(needPos.transform.eulerAngles.y - (transform.eulerAngles.y-360)) * yRotationSmoothness * Time.deltaTime));
            else
                transform.rotation = Quaternion.RotateTowards(transform.rotation, needPos.transform.rotation, (Mathf.Abs((needPos.transform.eulerAngles.y-360) - transform.eulerAngles.y) * yRotationSmoothness * Time.deltaTime));
        }
    }
}