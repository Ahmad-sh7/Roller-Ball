using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject ball;

    void Update()
    {
        transform.position = new Vector3(ball.transform.position.x, transform.position.y, -8f + ball.transform.position.z);
    }
}
