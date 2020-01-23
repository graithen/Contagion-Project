using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserverCameraController : MonoBehaviour
{
    public float CameraSpeed = 10;
    public float CameraZoom = 0.5f;

    Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
        ZoomCamera();
    }

    void MoveCamera()
    {
        transform.position = new Vector2(transform.position.x + (CameraSpeed * Input.GetAxis("Horizontal") * Time.deltaTime), transform.position.y + (CameraSpeed * Input.GetAxis("Vertical") * Time.deltaTime));
    }

    void ZoomCamera()
    {
        if(Input.GetKey(KeyCode.Q))
        {
            mainCam.orthographicSize -= CameraZoom;
        }
        if(Input.GetKey(KeyCode.E))
        {
            mainCam.orthographicSize += CameraZoom;
        }
    }
}
