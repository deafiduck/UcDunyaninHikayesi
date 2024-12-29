using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerWAnimation : MonoBehaviour
{
    public Animator anim;
    public bool isAttack = false; //reis attack yapýyo mu kontrolü
    private void Start()
    {
        anim= GetComponent<Animator>();
    }
    public void walk()
    {
        anim.SetBool( "isWalk",true);
        anim.SetBool("isRun", false);
        anim.SetBool("isIdle", false);
        anim.SetBool("isJump", false);
        anim.SetBool("isAttack", false);
        isAttack = false;
    }

    public void Run()
    {
        anim.SetBool("isRun", true);
        anim.SetBool("isWalk", false);
        anim.SetBool("isIdle", false);
        anim.SetBool("isJump", false);
        anim.SetBool("isAttack", false); 
        isAttack = false;
    }

    public void Idle()
    {
        anim.SetBool("isRun", false);
        anim.SetBool("isWalk", false);
        anim.SetBool("isIdle", true);
        anim.SetBool("isJump", false);
        anim.SetBool("isAttack", false);
        isAttack = false;
    }

    public void Attack()
    {
        anim.SetBool("isRun", false);
        anim.SetBool("isWalk", false);
        anim.SetBool("isIdle", false);
        anim.SetBool("isJump", false);
        anim.SetBool("isAttack", true);
        isAttack = true ;
    }

    public void Jump()
    {
        anim.SetBool("isRun", false);
        anim.SetBool("isWalk", false);
        anim.SetBool("isIdle", false);
        anim.SetBool("isJump", true);
        anim.SetBool("isAttack", false);
        isAttack = false;
    }
}
