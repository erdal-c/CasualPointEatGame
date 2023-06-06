using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool ýnstance;

    public Queue<GameObject> dotPoolQueue = new Queue<GameObject>();

    public Queue<GameObject> enemyPoolQueue = new Queue<GameObject>();

    public Queue<GameObject> mapPointPoolQueue = new Queue<GameObject>();

    void Awake()
    {
        if (ýnstance == null) 
        {
            ýnstance = this;
        }
    }

    public void AddQueue(string queueName,GameObject gameobject)
    {
        if(queueName == "dotpool")
        {
            dotPoolQueue.Enqueue(gameobject);
            gameobject.SetActive(false);
        }
        else if(queueName == "enemypool") 
        {
            enemyPoolQueue.Enqueue(gameobject);
            gameobject.SetActive(false);
        }
        else if(queueName == "mappointpool")
        {
            mapPointPoolQueue.Enqueue(gameobject);
            gameobject.SetActive(false);
        }

    }

    public GameObject RemoveQueue(string queueName)
    {
        if (queueName == "dotpool")
        {
            GameObject gameobject = dotPoolQueue.Dequeue();
            gameobject.SetActive(true);

            return gameobject;
        }
        else if (queueName == "enemypool")
        {
            GameObject gameobject = enemyPoolQueue.Dequeue();
            gameobject.SetActive(true);

            return gameobject;
        }
        else
        {
            GameObject gameobject = mapPointPoolQueue.Dequeue();
            gameobject.SetActive(true);

            return gameobject;
        }
    }
}
