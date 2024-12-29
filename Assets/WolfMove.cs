using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WolfMove : MonoBehaviour
{
    public Transform target;
    public float stopDistance = 5f; 
    GameObject Player;
    public float speed = 5f; 
    public Animator animator; 
    private Rigidbody rb;
    public float distance;
  

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();

        // Animator 
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    private void FixedUpdate()
    {
        

        if (target != null)
        {
            FollowPlayer();
        }
    }
    void FollowPlayer()
    {
         distance = Vector3.Distance(transform.position, target.position);
        if (Vector3.Distance(Player.transform.position, transform.position) < 10&& distance > stopDistance)
        {
            
            animator.SetBool("Idle", false);
            animator.SetBool("Run", true);
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

           
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);
        }
        else if (distance<3)
        {
           
            animator.SetBool("Run", false);
            animator.SetBool("Idle", true);
            rb.velocity = Vector3.zero;
            gameObject.SetActive(false);

        }
        else
        {
            animator.SetBool("Run",false);
            animator.SetBool("Idle", true);
        }
    }
}
