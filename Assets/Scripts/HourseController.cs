using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HourseController : MonoBehaviour
{
    public float speed = 5f; // At�n ilerleme h�z�
    public Animator animator; // At�n Animator bile�eni
    private Rigidbody rb; // Fiziksel hareket i�in Rigidbody
    public HourseA HAnimation;
    public CharController_Motor CController;
    public GameObject Player;

    private bool isRiding = false; // Oyuncunun ata binip binmedi�ini kontrol eder

    void Start()
    {
        HAnimation = GetComponent<HourseA>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

            CController = Player.GetComponent<CharController_Motor>();
          
      
    }

    void FixedUpdate()
    {
        WorkWithDistance();

    }

    public void WorkWithDistance()
    {
        if (DistanceManager.DistanceGameObject < 10 && Input.GetKeyDown(KeyCode.E))
        {
            if (CController != null && CController.walk == true)
            {
                HAnimation.walk();
                Debug.Log("E tu�una bas�ld�. Oyuncu ata biniyor.");
                Player.transform.position = transform.position;
                Player.transform.rotation = Quaternion.identity;
                Vector3 forwardMovement = transform.forward * speed * Time.fixedDeltaTime;
                rb.MovePosition(rb.position + forwardMovement);
            }
        }
    }

    private void MoveHorse()
    {

       

    }

    public void Dismount()
    {
        // Oyuncunun at� terk etmesi
        if (isRiding && Input.GetKeyDown(KeyCode.Q)) // Q tu�u at inme i�in
        {
            Debug.Log("Oyuncu attan indi.");
            isRiding = false;
            Player.transform.position = transform.position + transform.right * 2; // Oyuncuyu at�n yan�na yerle�tir
        }
    }
}
