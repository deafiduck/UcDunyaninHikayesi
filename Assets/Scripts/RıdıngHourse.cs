using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RıdıngHourse : MonoBehaviour
{

    private Vector3 movement;
    [SerializeField] GameObject newPlayer, OldPlayer;
    bool changePlayer;
    private bool isWalking, isRunning,Idle= false;
   
    public Animator animator;
    public float walkSpeed = 2f;
    public float runSpeed = 5f;
    void distanceControl()
    {
        if (Vector3.Distance(OldPlayer.transform.position, newPlayer.transform.position)<10)
        {
            if(Input.GetKeyDown(KeyCode.E)){
                ChangePlayer();

            }
        }
    }

    void ChangePlayer()
    {
        newPlayer.SetActive(true);
        OldPlayer.SetActive(false);
        changePlayer = true;

    }

    void AnimationController()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        animator.SetBool("Idle", Idle);
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isRunning", isRunning);
        movement = new Vector3(horizontal, 0f, vertical).normalized;


        if (movement.magnitude > 0)
        {
            if (Input.GetKey(KeyCode.LeftShift)) // Koşma tuşu
            {
                Idle = false;
                isWalking = false;
                isRunning = true;
                transform.Translate(movement * runSpeed * Time.deltaTime, Space.World);
            }
            else
            {
                Idle = false;
                isWalking = true;
                isRunning = false;
                transform.Translate(movement * walkSpeed * Time.deltaTime, Space.World);
            }

            // Karakterin yönü hareket ettiği yöne dönsün
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 360 * Time.deltaTime);
        }
        else
        {
            Idle = true;
            isWalking = false;
            isRunning = false;
        }

    }

    void Update()
    {
        distanceControl();
        if (changePlayer)
        {
            OldPlayer.transform.position=newPlayer.transform.position;
            AnimationController();
        }
    }
}
