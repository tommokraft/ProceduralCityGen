using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveLook : MonoBehaviour
{
    public float cameraSens = 100;
    public float upSpeed = 4;
    public float moveSpeed = 10;
    public float slow = 0.25f;
    public float fast = 3;
    private float rotationX = 0.0f;
    private float rotationY = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        Screen.lockCursor = true;
    }

    // Update is called once per frame
    void Update()
    {
        rotationX += Input.GetAxis("Mouse X") * cameraSens * Time.deltaTime;
        rotationY += Input.GetAxis("Mouse Y") * cameraSens * Time.deltaTime;
        rotationY = Mathf.Clamp(rotationY, -90, 90);
        transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
        transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.left);
        if(Input.GetKey(KeyCode.LeftShift))
        {
            transform.position += transform.forward * (moveSpeed * fast) * Input.GetAxis("Vertical") * Time.deltaTime;
            transform.position += transform.right * (moveSpeed * fast) * Input.GetAxis("Horizontal") * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            transform.position += transform.forward * (moveSpeed * slow) * Input.GetAxis("Vertical") * Time.deltaTime;
            transform.position += transform.right * (moveSpeed * slow) * Input.GetAxis("Horizontal") * Time.deltaTime;
        }
        else
        {
            transform.position += transform.forward * moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
            transform.position += transform.right * moveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.position += transform.up * upSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.position -= transform.up * upSpeed * Time.deltaTime;
        }
    }
}
