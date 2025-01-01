using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostAI : MonoBehaviour
{
    NavMeshAgent agent;
    Animator anim;
    Transform target; // ana karakterin pozisyonu
    public bool isDead = false;

    public float turnSpeed;
    public float damage = 10f; 

    public bool canAttack;
    [SerializeField]
    float attackCooldown = 2f; // player'ın 2 saniyede bir canı azalsın.
    private float lastAttackTime;

    void Start()
    {
        canAttack = true;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        lastAttackTime = -attackCooldown; // Başlangıçta hemen saldırı yapabilmesi için
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, target.position); // Player ile ghost arasındaki mesafe

        if (distance < 10 && distance > agent.stoppingDistance && !isDead)
        {
            ChasePlayer();
            Debug.Log("Chase");
        }
        else if (distance <= agent.stoppingDistance /*&& canAttack == true && PlayerHealth.PH.isDead == false*/) // player canlı ise, zombi ona saldırabilsin
        {
            AttackPlayer();
            Debug.Log("Attack");
        }
        else if (distance > 10)
        {
            StopChase();
            Debug.Log("Stop Chase");
        }
    }

    void ChasePlayer()
    {
        agent.updateRotation = true;
        agent.updatePosition = true; // pozisyonu güncellenecek
        agent.SetDestination(target.position);
        anim.SetBool("isRunning", true);
        anim.SetBool("isAttack", false); // koşarken atak yapamasın
    }

    void AttackPlayer()
    {
        agent.updateRotation = false;
        Vector3 direction = target.position - transform.position; // zombi atak yaparken yüzünün player'a dönük olması için rotasyonunu ayarladık
        direction.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), turnSpeed * Time.deltaTime);
        agent.updatePosition = false; // durarak atak yapacak
        anim.SetBool("isRunning", false);
        anim.SetBool("isAttack", true);
        anim.SetBool("isDead", false);

        if (Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            CharacterHealth.CH.Damage(damage);
        }
    }

    void StopChase()
    {
        agent.updatePosition = false;
        anim.SetBool("isRunning", false);
        anim.SetBool("isAttack", false);
        anim.SetBool("isDead", false);
    }

    public void DeadAnim()
    {
        canAttack = false;
        anim.SetBool("isDead", true);
        anim.SetBool("isRunning", false);
        anim.SetBool("isAttack", false);
        Destroy(gameObject, 2f);
    }
}
