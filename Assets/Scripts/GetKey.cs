using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetKey : MonoBehaviour
{
    public float theDistance; //player'�n anahtara olan uzakl��� 
    public bool keyTaken; //anahtar al�nd� m� al�nmad� m�
    public GameObject key;
    public GameObject keyImage;
    public GameObject pointLight;
    public GameObject getKeyText; //E'ye bas yaz�s�
    void Start()
    {
        keyTaken = false;
        getKeyText.SetActive(false);
    }

    
    void Update()
    {
        theDistance = PlayerRay.distanceFromTarget;

        //Debug.Log("Key distance: " + theDistance);
        if (theDistance <= 1.5f)
        {
            if (!keyTaken) //anahtar al�nmad�ysa �al��s�n 
            {
                getKeyText.SetActive(true); 
            }
           
            if (Input.GetKey(KeyCode.E))
            {
                KeyTaken();
            }
            
        }
        else
        {
            getKeyText.SetActive(false);
        }


        if (keyTaken)
        {
            keyImage.SetActive(true);
        }
        else
        {
            keyImage.SetActive(false);
        }
    }

    private void KeyTaken()
    {
        keyTaken = true;
        StartCoroutine(KeyTakenText());
        key.GetComponent<MeshRenderer>().enabled = false;
        pointLight.SetActive(false);
     
    }

    IEnumerator KeyTakenText()
    {
        yield return new WaitForSeconds(2f);
        getKeyText.SetActive(false);
    }

}
