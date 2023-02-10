using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] private RectTransform playerTransform;
    [SerializeField] private float camSpeed;


    // Update is called once per frame
    void Update()
    {
        Vector3 cameraMove = new Vector3(playerTransform.position.x, playerTransform.position.y+2, -10);
        transform.position = Vector3.Lerp(transform.position, cameraMove, camSpeed);
    }
}
