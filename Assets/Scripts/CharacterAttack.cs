using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    CharacterControllerWAnimation characterScript; //atak yap�p yapmad���na eri�ilecek
    public float damage = 20f;//verece�imiz hasar

    void Start()
    {
        characterScript= GetComponent<CharacterControllerWAnimation>();
    }

    
    void Update()
    {
        if (Input.GetButton("Fire1")) 
        {
            
        }
    }


    
}
