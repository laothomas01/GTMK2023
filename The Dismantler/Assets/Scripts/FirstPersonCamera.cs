using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
     public Transform player;

    void FixedUpdate()
    {
        transform.position = player.position;
        transform.rotation = player.rotation;
    }
}
