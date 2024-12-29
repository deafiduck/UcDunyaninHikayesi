using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostHealth : MonoBehaviour
{
    [SerializeField]
    float enemyHealth = 100f;

    GhostAI enemy;

    void Start()
    {
        enemy = GetComponent<GhostAI>();
    }

    
    void Update()
    {
        if (enemyHealth < 0)
        {
            enemyHealth = 0; 
        }
    }

    public void ReduceHealth(float reduceHealth)  //parametre olarak can�n ne kadar azalaca���n� al�yo
    {
        enemyHealth -= reduceHealth;

        if (enemyHealth <= 0)
        {
            enemy.isDead = true;
            enemy.DeadAnim();
        }

    }

   
}
