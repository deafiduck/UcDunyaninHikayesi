using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterHealth : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth = 100f;
    public static CharacterHealth CH;
    public bool isDead;

    private void Awake()
    {
        CH = this;
    }

    void Start()
    {
        isDead = false;
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            if (!isDead)
            {
                Dead();
            }
        }
    }

    public void Damage(float damage)
    {
        if (!isDead && currentHealth > 0)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Dead();
            }
        }
    }

    void Dead()
    {
        currentHealth = 0;
        isDead = true;
        Debug.Log("Game Over!");
        // Oyun bittiðinde yapýlacaklar burada
    }
}
