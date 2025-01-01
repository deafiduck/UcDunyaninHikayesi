using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostAI : MonoBehaviour
{
    NavMeshAgent agent;
    Animator anim;
    Transform target; //ana karakterin pozisyonu
   public bool isDead = false;


    public float turnSpeed; //player'a doðru dönme hýzý
    public float damage = 25f;

    public bool canAttack; 
    [SerializeField]
    float attackTimer = 2f; // player'ýn 2 saniyede bir caný azalsýn.
    void Start()
    {
        canAttack = true;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    
    void Update()
    {
        float distance = Vector3.Distance(transform.position, target.position); //Player ile ghost arasındaki mesafe

        if (distance < 10 && distance > agent.stoppingDistance && !isDead)
        {
            ChasePlayer();
            Debug.Log("Chase");
        }

        else if (distance <= agent.stoppingDistance /*&& canAttack == true && PlayerHealth.PH.isDead == false*/) //player canlý ise, zombi ona saldýrabilsin
        {
            AttackPlayer();
            Debug.Log("Attack");
        }

        else if (distance > 10) 
        {
            StopChase();
            Debug.Log("Stop Chase");
        }

        Debug.Log("ENEMY DISTANCE: " + distance);
    }

    void ChasePlayer()
    {
        agent.updateRotation = true;
        agent.updatePosition = true; //pozisyonu güncellenecek
        agent.SetDestination(target.position);
        anim.SetBool("isRunning", true);
        anim.SetBool("isAttack", false); //koþarken atak yapamasýn
    }

    void AttackPlayer()
    {
        agent.updateRotation = false;
        Vector3 direction = target.position - transform.position;  //zombi atak yaparken yüzünün player'a dönük olmasý için rotasyonunu ayarladýk
        direction.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), turnSpeed * Time.deltaTime);
        agent.updatePosition = false; //durarak atacak yapacak
        anim.SetBool("isRunning", false);
        anim.SetBool("isAttack", true);
        anim.SetBool("isDead", false);
        // PlayerHealth.PH.Damage(damage);
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
