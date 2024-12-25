using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HourseAnimation : MonoBehaviour
{
    public float speed = 5f; // At�n ilerleme h�z�
    public Animator animator; // At�n Animator bile�eni
    private Rigidbody rb; // Fiziksel hareket i�in Rigidbody

    void Start()
    {
        // Rigidbody bile�enini al
        rb = GetComponent<Rigidbody>();

        // Animator bile�enini al
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    void FixedUpdate()
    {
        // E�er ko�ma animasyonu oynuyorsa fiziksel hareket uygula
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("horserig|horse runcycle new")) // "Run" animasyon ismi
        {
            Vector3 forwardMovement = transform.forward * speed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + forwardMovement);
        }
    }
}
