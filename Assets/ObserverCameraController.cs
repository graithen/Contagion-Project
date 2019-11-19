using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserverCameraController : MonoBehaviour
{
    public float CameraSpeed = 10;

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
    }

    void MoveCamera()
    {
        transform.position = new Vector2(transform.position.x + (CameraSpeed * Input.GetAxis("Horizontal") * Time.deltaTime), transform.position.y + (CameraSpeed * Input.GetAxis("Vertical") * Time.deltaTime));
    }
}
