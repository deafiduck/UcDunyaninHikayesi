using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    CharacterControllerWAnimation characterScript; //atak yapýp yapmadýðýna eriþilecek
    public float damage = 20f;//vereceðimiz hasar

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
