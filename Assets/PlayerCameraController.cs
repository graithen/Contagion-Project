using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    GameObject localPlayer;

    Vector2 playerPos;
    float horizontalOffset = 2;
    float verticalOffset = 4;

    // Start is called before the first frame update
    void Start()
    {
        localPlayer = GameObject.Find("LocalPlayer");
    }

    // Update is called once per frame
    void Update()
    {
        TrackPlayerPosition();
        AdjustToOffset();
    }

    void TrackPlayerPosition()
    {
        playerPos = localPlayer.transform.position;
    }

    void AdjustToOffset()
    {
        Vector2 currentPos = gameObject.transform.position;
        Vector2 nextPos = new Vector2(playerPos.x + horizontalOffset * Input.GetAxis("Horizontal"), playerPos.y + verticalOffset * Input.GetAxis("Vertical"));
        Vector2 lerpedPos = Vector2.Lerp(currentPos, nextPos, 0.25f);
        gameObject.transform.position = lerpedPos;
    }
}
