using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    PlayerContoler player;

    Vector3 startPosition;

    float lenght;


    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerContoler>();
        startPosition = transform.position;
        lenght = GetComponentInChildren<SpriteRenderer>().bounds.size.x;        
    }

    // Update is called once per frame
    void Update()
    {
        BackgroundMover();
    }

    void BackgroundMover()
    {
        if (player.transform.position.x > startPosition.x + lenght / 2)
        {
            startPosition.x += lenght;
            transform.position = startPosition;
        }
        else if (player.transform.position.x < startPosition.x - lenght / 2)
        {
            startPosition.x -= lenght;
            transform.position = startPosition;
        }
        if (player.transform.position.y > startPosition.y + lenght / 2)
        {
            startPosition.y += lenght;
            transform.position = startPosition;
        }
        else if (player.transform.position.y < startPosition.y - lenght / 2)
        {
            startPosition.y -= lenght;
            transform.position = startPosition;
        }
    }
    void BackgroundMover2()
    {
        if (player.transform.position.magnitude > startPosition.magnitude + lenght / 2)
        {
            startPosition = new Vector3(startPosition.x + lenght, startPosition.y + lenght, 0);
            transform.position = startPosition;
        }
        else if (player.transform.position.magnitude < startPosition.magnitude - lenght / 2)
        {
            startPosition = new Vector3(startPosition.x - lenght, startPosition.y - lenght, 0);
            transform.position = startPosition;
        }
    }
}
