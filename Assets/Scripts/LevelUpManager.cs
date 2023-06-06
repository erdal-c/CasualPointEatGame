using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelUpManager : MonoBehaviour
{
    public static LevelUpManager instance;
    PlayerContoler playerContoler;
    EnemyManager enemyManager;

    GameObject enemyParent;
    void Start()
    {
        instance = this;

        playerContoler = FindObjectOfType<PlayerContoler>();
        enemyManager= FindObjectOfType<EnemyManager>();
        enemyParent = GameObject.Find("EnemyParent");
    }

    public void PlayerSpeedUp()
    {
        playerContoler.PlayerSpeedProperty += 100;
    }

    public void PlayerDashPowerUp() 
    {
        playerContoler.DashSpeedProperty += 50;
    }

    public void PlayerDashTime()
    {
        playerContoler.DashTimeProperty -= 0.2f;
    }

    public void PlayerHealthIncrease(int health)
    {
        playerContoler.PlayerHealthProperty = health;
    }

    public void EnemySpeedUp()
    {
        foreach (Transform enemy in enemyParent.transform)
        {
            enemy.GetComponent<EnemyManager>().enemySpeed += 50;
        }
    }
}
