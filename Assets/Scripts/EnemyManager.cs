using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public LayerMask layer;

    Rigidbody enemyRB;
    public static EnemyManager instance;

    PlayerContoler playerContoler;

    public float enemySpeed = 200f;

    bool isPatroling;
    float moveTime = 0f;
    Vector3 movePoint;

    bool isPursue;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        enemyRB = GetComponent<Rigidbody>();
        playerContoler = FindObjectOfType<PlayerContoler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPursue)
        {
            Patrolling();
        }
    }
    private void FixedUpdate()
    {
    }

    void DetectPlayer()
    {
        if(Physics.CheckSphere(transform.position, 10f, layer))
        {
            PursuePlayer();
        }
    }
    void PursuePlayer()
    {
        enemyRB.AddForce((playerContoler.transform.position - transform.position).normalized * enemySpeed * Time.fixedDeltaTime);
        transform.rotation = Quaternion.FromToRotation(Vector3.up, playerContoler.transform.position - transform.position);
    }

    void Patrolling()
    {
        float randomx = Random.Range(-5, +5);
        float randomy = Random.Range(-5, +5);

        float distanceToMovePoint = (movePoint - transform.position).magnitude;

        if (!isPatroling && moveTime <= Time.timeSinceLevelLoad)
        {
            movePoint = new Vector3(randomx, randomy, 0);
            isPatroling = true;
            
            moveTime = Random.Range(3, 5) + Time.timeSinceLevelLoad;
        }
        else if(!isPatroling && moveTime > Time.timeSinceLevelLoad)
        {
            enemyRB.velocity = Vector2.Lerp(enemyRB.velocity, Vector2.zero, 0.01f);
        }

        if (isPatroling)
        {
            enemyRB.AddForce(movePoint * enemySpeed/5 * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.FromToRotation(Vector3.up, movePoint), 0.5f);

            if (moveTime < Time.timeSinceLevelLoad)
            {
                moveTime = Random.Range(2, 3) + Time.timeSinceLevelLoad;
                isPatroling = false;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(other.GetType() == typeof(BoxCollider))
            {
                PursuePlayer();
                isPursue = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetType() == typeof(BoxCollider))
            {
                isPursue = false;
            }
        }
    }
}
