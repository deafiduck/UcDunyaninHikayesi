using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRay : MonoBehaviour
{
    public static float distanceFromTarget; //baþka script'lerden de eriþebilmek için static tanýmladýk
    public float toTarget; //tavana,duvarlara yani her þeye olan uzaklýðý hesaplar


    void Update()
    {
        RaycastHit hit;  //kullanýcýdan ýþýn demeti çýkmasýný saðladýk ( bu ýþýn demetleri sayesinde uzaklýk hesabý yapacaðýz)

        //ýþýn bir nesneye çarparsa
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit)) //Physic ile ýþýnýn çýkacaðý fiziki bir alan oluþturduk. Poziyonunu, yönünü (ileri yönlü) belirlerdik. Ve çýktý olarak da hit'i oluþtursun dedik
        {
            toTarget = hit.distance;  //kafamý nereye çevirirsem o noktaya olan uzaklýk toTarget olsun
            distanceFromTarget = toTarget;
        }
    }
}
