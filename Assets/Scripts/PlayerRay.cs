using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRay : MonoBehaviour
{
    public static float distanceFromTarget; //ba�ka script'lerden de eri�ebilmek i�in static tan�mlad�k
    public float toTarget; //tavana,duvarlara yani her �eye olan uzakl��� hesaplar


    void Update()
    {
        RaycastHit hit;  //kullan�c�dan ���n demeti ��kmas�n� sa�lad�k ( bu ���n demetleri sayesinde uzakl�k hesab� yapaca��z)

        //���n bir nesneye �arparsa
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit)) //Physic ile ���n�n ��kaca�� fiziki bir alan olu�turduk. Poziyonunu, y�n�n� (ileri y�nl�) belirlerdik. Ve ��kt� olarak da hit'i olu�tursun dedik
        {
            toTarget = hit.distance;  //kafam� nereye �evirirsem o noktaya olan uzakl�k toTarget olsun
            distanceFromTarget = toTarget;
        }
    }
}
