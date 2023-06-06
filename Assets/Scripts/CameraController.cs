using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    PlayerContoler player;

    void Start()
    {
        player = FindObjectOfType<PlayerContoler>();
    }

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, 
                                            new Vector3(player.transform.position.x, player.transform.position.y, -10), 
                                            0.5f);
    }
}
