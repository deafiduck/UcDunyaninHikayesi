using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HourseA : MonoBehaviour
{
    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void walk()
    {
        anim.SetBool("IsRunning", false);
        anim.SetBool("IsIdle", false);
        anim.SetBool("IsWalking", true);

    }

    public void Run()
    {
        anim.SetBool("IsRunning", true);
        anim.SetBool("IsIdle", false);
        anim.SetBool("IsWalking", false);
    }
    void Idle()
    {
        anim.SetBool("IsRunning", false);
        anim.SetBool("IsIdle", true);
        anim.SetBool("IsWalking", false);
    }
}
