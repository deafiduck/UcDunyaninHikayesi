using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WolfMove : MonoBehaviour
{
    public Transform target;
    public float stopDistance = 15f;
    GameObject Player;
    public float speed = 5f;
    public Animator animator;
    private Rigidbody rb;
    public float distance;
    public GameObject Text;
    public bool wolfTalk;
    private bool wolfDisapare;
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
        StartCoroutine(Talk());
        if (target != null && wolfTalk)
        {
            FollowPlayer();
        }

    }

    private IEnumerator Talk()
    {
        distance = Vector3.Distance(transform.position, target.position);
        if (Vector3.Distance(Player.transform.position, transform.position) < 10)
        {
            Text.SetActive(true);

            yield return new WaitForSeconds(10);
            wolfTalk = true;
        }



    }


    void FollowPlayer()
    {
        Text.SetActive(false);
        distance = Vector3.Distance(transform.position, target.position);
        if (Vector3.Distance(Player.transform.position, transform.position) < 10 && distance > stopDistance)
        {

            animator.SetBool("Idle", false);
            animator.SetBool("Run", true);
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;


            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);
        }
        else if (distance < 15)
        {

            animator.SetBool("Run", false);
            animator.SetBool("Idle", true);
            rb.velocity = Vector3.zero;
          

        }
        else
        {
            animator.SetBool("Run", false);
            animator.SetBool("Idle", true);
            gameObject.SetActive(false);
            
        }
    }
}