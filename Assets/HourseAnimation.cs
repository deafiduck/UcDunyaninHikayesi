using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HourseAnimation : MonoBehaviour
{
    public float speed = 5f; // Atýn ilerleme hýzý
    public Animator animator; // Atýn Animator bileþeni
    private Rigidbody rb; // Fiziksel hareket için Rigidbody

    void Start()
    {
        // Rigidbody bileþenini al
        rb = GetComponent<Rigidbody>();

        // Animator bileþenini al
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    void FixedUpdate()
    {
        // Eðer koþma animasyonu oynuyorsa fiziksel hareket uygula
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("horserig|horse runcycle new")) // "Run" animasyon ismi
        {
            Vector3 forwardMovement = transform.forward * speed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + forwardMovement);
        }
    }
}
