using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject dot;
    public Transform dotParent;
    public GameObject enemy;
    public Transform enemyParent;

    Vector3 spawnArea;
    PlayerContoler playerContoler;

    int levelLimit = 1;
    int _level = 0;
    int _levelUpSkillPoint;

    public int LevelProperty { get { return _level; } set { _level = value; } }
    public int LevelSkillPointProperty { get { return _levelUpSkillPoint; } set { _levelUpSkillPoint = value; } }

    void Start()
    {
        playerContoler = FindObjectOfType<PlayerContoler>();
        for(int i=0; i<10; i++)
        {
            DotCreater();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //DotCreater();
        DotRepositioner();
        DistantDot();
        DistantEnemy();
        LevelUp();
    }

    void DotCreater()
    {
        float randomX = Random.Range(playerContoler.transform.position.x - 20, playerContoler.transform.position.x + 20);
        float randomY = Random.Range(playerContoler.transform.position.y - 20, playerContoler.transform.position.y + 20);
        spawnArea = new Vector3(randomX, randomY, 0);
        if (dotParent.childCount <10)
        {
            Instantiate(dot, spawnArea, Quaternion.identity, dotParent);
        }
    }

    void DotRepositioner()
    {
        float randomX = Random.Range(playerContoler.transform.position.x - 20, playerContoler.transform.position.x + 20);
        float randomY = Random.Range(playerContoler.transform.position.y - 20, playerContoler.transform.position.y + 20);
        spawnArea = new Vector3(randomX, randomY, 0);
        
        if(ObjectPool.ýnstance.dotPoolQueue.Count > 0) 
        {
            ObjectPool.ýnstance.RemoveQueue("dotpool").transform.position = spawnArea;  //RemoveQueue Gameobject return ediyor. bu yüzden doðrudan .tranform.position kullanabiliyoruz. 
        }
    }

    void DistantDot()
    {
        foreach(Transform d in dotParent)
        {
            if((d.position - playerContoler.transform.position).magnitude > 40)
            {
                //Destroy(d.gameObject);
                ObjectPool.ýnstance.AddQueue("dotpool", d.gameObject);
            }
        }
    }

    void DistantEnemy()
    {
        foreach(Transform e in enemyParent) 
        {
            if ((e.position - playerContoler.transform.position).magnitude > 30)
            {
                ObjectPool.ýnstance.AddQueue("enemypool",e.gameObject);
                EnemyReposioner();
            }
        }
    }
    float sliderMoveAccelerator = 1.0f;
    void LevelUp()
    {
        
        UIManager.Instance.SliderControl(levelLimit, playerContoler.ReturnDot(), sliderMoveAccelerator);

        if (playerContoler.ReturnDot() == levelLimit)
        {
            _level ++;
            _levelUpSkillPoint ++;
            sliderMoveAccelerator += 0.5f;

            UIManager.Instance.RandomizedProperty = true;
            UIManager.Instance.button1.interactable = true;
            UIManager.Instance.button2.interactable = true;

            float templevelLimit = (float)levelLimit;
            templevelLimit *= 1.5f;
            levelLimit = Mathf.RoundToInt(templevelLimit);
            playerContoler.ResetDot();
            for (int i = 0; i < levelLimit/2; i++)
            {
                EnemySpawner2();
            }
        }
    }

    void EnemySpawner()
    {
        //float radius = Random.Range(10f, 20f);
        float radius = 10f;
        Vector2 spawnPoint = Random.insideUnitCircle * radius;
        print(spawnPoint);
        Instantiate(enemy, spawnPoint, Quaternion.identity, enemyParent);
    }

    void EnemySpawner2()
    {
        float randomx = Random.Range(-20f, 20f);
        float randomy = Random.Range(-20f, 20f);
        Vector3 spawnPoint = playerContoler.transform.position + new Vector3(randomx, randomy,0);
        if((spawnPoint - playerContoler.transform.position).magnitude > 10f)
        {
            Instantiate(enemy, spawnPoint, Quaternion.identity, enemyParent);
        }
        else
        {
            EnemySpawner2();
        }
    }

    void EnemyReposioner()
    {
        float randomx = Random.Range(-20f, 20f);
        float randomy = Random.Range(-20f, 20f);
        Vector3 spawnPoint = playerContoler.transform.position + new Vector3(randomx, randomy, 0);
        
        if ((spawnPoint - playerContoler.transform.position).magnitude > 10f)
        {
            ObjectPool.ýnstance.RemoveQueue("enemypool").transform.position = spawnPoint;
        }
        else
        {
            EnemyReposioner();
        }

    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(playerContoler.transform.position,)
    }
}
