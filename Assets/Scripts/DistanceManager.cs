using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceManager : MonoBehaviour
{
    //bu script player nesnesinde durur, hangi nesne ile player aras�ndaki mesafeyi bulmak istiyosan�z
    //o nesneden extends edin
    public void WorkWithDistance() { }//bu class� extends eden her nesne bu mesafe ile ne yapacaksa bu class� kullanacak
    

    public static float DistanceGameObject;
    GameObject Object;
    GameObject player;
    public string ObjectTag;
    void Start()
    {
        Object = GameObject.FindWithTag("Hourse");
        player = GameObject.FindWithTag("Player");
    }

    void DistanceCalc()
    {
        DistanceGameObject = Vector3.Distance(player.transform.position, Object.transform.position);
    }
    private void FixedUpdate()
    {
        DistanceCalc();
    }
}
